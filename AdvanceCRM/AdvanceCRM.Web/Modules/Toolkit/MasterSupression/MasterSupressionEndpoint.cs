using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serenity;
using Serenity.Data;
using Serenity.Reporting;
using Serenity.Services;
using Serenity.Web;
using AdvanceCRM.Web.Helpers;
using AdvanceCRM.Masters;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using MyRow = AdvanceCRM.Toolkit.MasterSupressionRow;

namespace AdvanceCRM.Toolkit.Endpoints
{
    [Route("Services/Toolkit/MasterSupression/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class MasterSupressionController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public MasterSupressionController(
            ISqlConnections connections,
            IConfiguration configuration,
            IWebHostEnvironment env)
        {
            _connections = connections;
            _configuration = configuration;
            _env = env;
            UploadHelper.Configure(configuration, env);

            // Rewrites the template whenever its header row no longer matches.
            SupressionTemplateInitializer.EnsureTemplateExists(
                Path.Combine(env.ContentRootPath, "Templates", "MasterSupression_Template.xlsx"),
                "MasterSupression",
                SupressionTemplateInitializer.MasterSupressionHeaders);
        }

        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IMasterSupressionSaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IMasterSupressionSaveHandler handler)
        {
            return handler.Update(uow, request);
        }

        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] IMasterSupressionDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] IMasterSupressionRetrieveHandler handler)
        {
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] IMasterSupressionListHandler handler)
        {
            return handler.List(connection, request);
        }

        public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
            [FromServices] IMasterSupressionListHandler handler,
            [FromServices] IExcelExporter exporter)
        {
            var data = List(connection, request, handler).Entities;
            var bytes = exporter.Export(data, typeof(Columns.MasterSupressionColumns), request.ExportColumns);
            return ExcelContentResult.Create(bytes, "MasterSupressionList_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
        }

        [HttpPost, ServiceAuthorize("MasterSupression:Import")]
        public ActionResult DownloadTemplate(IDbConnection connection, RetrieveRequest request)
        {
            // Shared with the on-disk template so the download can never drift from the column
            // order ExcelImport reads - it used to omit "Sr No" and shift every column by one.
            string[] headers = SupressionTemplateInitializer.MasterSupressionHeaders;

            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("MasterSupression");
                for (int i = 0; i < headers.Length; i++)
                {
                    var cell = ws.Cells[1, i + 1];
                    cell.Value = headers[i];
                    cell.Style.Font.Bold = true;
                    ws.Column(i + 1).Width = 20;
                }

                byte[] bytes = package.GetAsByteArray();
                return File(bytes,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "MasterSupression_Template.xlsx");
            }
        }

        [HttpPost, ServiceAuthorize("MasterSupression:Import")]
        public ExcelImportResponse ExcelImport(IUnitOfWork uow, MasterSupressionExcelImportRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrWhiteSpace(request.FileName))
                throw new ArgumentNullException(nameof(request.FileName));
            if (request.MasterAccountId == null)
                throw new ValidationError("Please select a Master Account before importing");

            UploadHelper.CheckFileNameSecurity(request.FileName);

            if (!request.FileName.StartsWith("temporary/", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentOutOfRangeException("filename");

            // The Master Account picked in the dialog is the fallback for rows that leave the
            // Account Number cell blank.
            var defaultAccount = uow.Connection.TryById<DemandayMasterAccountRow>(request.MasterAccountId.Value);
            if (defaultAccount == null)
                throw new ValidationError("Selected master account was not found");

            // A row may name a different account, so one file can span several of them.
            var accountLookup = uow.Connection.List<DemandayMasterAccountRow>()
                .Where(a => !string.IsNullOrWhiteSpace(a.AccountNumber) && a.Id.HasValue)
                .GroupBy(a => a.AccountNumber.Trim(), StringComparer.OrdinalIgnoreCase)
                .ToDictionary(g => g.Key, g => g.First().Id.Value, StringComparer.OrdinalIgnoreCase);

            string physicalPath = UploadHelper.DbFilePath(request.FileName);

            ExcelPackage ep = new ExcelPackage();
            using (var fs = new FileStream(physicalPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                ep.Load(fs);
            }

            var response = new ExcelImportResponse();
            response.Inserted = 0;
            response.Updated = 0;
            response.ErrorList = new List<string>();

            var worksheet = ep.Workbook.Worksheets.FirstOrDefault();
            if (worksheet == null)
                throw new ValidationError("Uploaded excel file does not contain any worksheet");

            int ownerId = Convert.ToInt32(Context.User.GetIdentifier());

            // Master Suppression is imported account-wise, so the upsert key is the Account Number
            // (column 2) plus SrNo (column 1), not SrNo alone: the same SrNo under the same
            // account updates that row, while the same SrNo under a different account is a
            // separate record. That lets every account restart its numbering at 1.
            var existingRows = uow.Connection.List<MyRow>(q => q
                    .Select(MyRow.Fields.Id).Select(MyRow.Fields.SrNo).Select(MyRow.Fields.MasterAccountId)
                    .Where(new Criteria(MyRow.Fields.SrNo).IsNotNull()))
                .Where(r => r.SrNo.HasValue && r.MasterAccountId.HasValue)
                .ToList();

            var idByAccountAndSrNo = existingRows
                .GroupBy(r => (AccountId: r.MasterAccountId.Value, SrNo: r.SrNo.Value))
                .ToDictionary(g => g.Key, g => g.First().Id.Value);

            // Highest SrNo per account, so a blank SrNo cell continues that account's own run.
            var maxSrNoByAccount = existingRows
                .GroupBy(r => r.MasterAccountId.Value)
                .ToDictionary(g => g.Key, g => g.Max(r => r.SrNo.Value));

            for (var row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                try
                {
                    var srNoStr = Convert.ToString(worksheet.Cells[row, 1].Value ?? "").Trim();
                    var accountNumber = Convert.ToString(worksheet.Cells[row, 2].Value ?? "").Trim();
                    var companyName = Convert.ToString(worksheet.Cells[row, 3].Value ?? "").Trim();
                    var firstName = Convert.ToString(worksheet.Cells[row, 4].Value ?? "").Trim();
                    var lastName = Convert.ToString(worksheet.Cells[row, 5].Value ?? "").Trim();
                    var email = Convert.ToString(worksheet.Cells[row, 6].Value ?? "").Trim();
                    var domain = Convert.ToString(worksheet.Cells[row, 7].Value ?? "").Trim();
                    var dateStr = Convert.ToString(worksheet.Cells[row, 8].Value ?? "").Trim();

                    if (string.IsNullOrEmpty(accountNumber) && string.IsNullOrEmpty(companyName) &&
                        string.IsNullOrEmpty(email))
                        continue;

                    // Blank Account Number falls back to the dialog's Master Account; a named one
                    // must resolve, otherwise the row would silently land under the wrong account.
                    var masterAccountId = defaultAccount.Id.Value;
                    if (!string.IsNullOrEmpty(accountNumber))
                    {
                        if (!accountLookup.TryGetValue(accountNumber, out var resolved))
                        {
                            response.ErrorList.Add($"Row {row}: Account Number '{accountNumber}' not found");
                            continue;
                        }
                        masterAccountId = resolved;
                    }

                    int? srNo = null;
                    if (!string.IsNullOrEmpty(srNoStr))
                    {
                        if (!int.TryParse(srNoStr, out var parsedSrNo))
                        {
                            response.ErrorList.Add($"Row {row}: Sr No '{srNoStr}' is not a valid number");
                            continue;
                        }
                        srNo = parsedSrNo;
                    }

                    // Numbering is tracked per account, so account B's blank cells start at 1 even
                    // when account A already runs into the thousands.
                    maxSrNoByAccount.TryGetValue(masterAccountId, out var accountMaxSrNo);

                    if (!srNo.HasValue)
                        srNo = accountMaxSrNo + 1;

                    if (srNo.Value > accountMaxSrNo)
                        maxSrNoByAccount[masterAccountId] = srNo.Value;

                    DateTime? date = null;
                    if (!string.IsNullOrEmpty(dateStr))
                    {
                        if (DateTime.TryParse(dateStr, out DateTime parsedDate))
                            date = parsedDate;
                    }

                    var data = new MyRow
                    {
                        // Account-wise import: no campaign is involved.
                        SrNo = srNo,
                        MasterAccountId = masterAccountId,
                        CompanyName = companyName,
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        Domain = domain,
                        Date = date
                    };

                    var upsertKey = (AccountId: masterAccountId, SrNo: srNo.Value);

                    if (idByAccountAndSrNo.TryGetValue(upsertKey, out var existingId))
                    {
                        data.Id = existingId;
                        uow.Connection.UpdateById(data);
                        response.Updated++;
                    }
                    else
                    {
                        data.OwnerId = ownerId;
                        var newId = (int)uow.Connection.InsertAndGetID(data);
                        idByAccountAndSrNo[upsertKey] = newId;
                        response.Inserted++;
                    }
                }
                catch (Exception ex)
                {
                    response.ErrorList.Add($"Row {row}: {ex.Message}");
                }
            }

            return response;
        }
    }
}