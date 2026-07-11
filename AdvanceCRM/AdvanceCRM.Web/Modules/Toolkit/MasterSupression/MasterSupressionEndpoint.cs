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
            string[] headers = { "Account Number", "Company Name", "First Name", "Last Name", "Email", "Domain", "Date" };

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
                .Where(a => !string.IsNullOrWhiteSpace(a.AccountNumber))
                .GroupBy(a => a.AccountNumber.Trim(), StringComparer.OrdinalIgnoreCase)
                .ToDictionary(g => g.Key, g => g.First().Id, StringComparer.OrdinalIgnoreCase);

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

            // SrNo (column 1) is the upsert key: an existing SrNo is updated, not duplicated.
            var idBySrNo = uow.Connection.List<MyRow>(q => q
                    .Select(MyRow.Fields.Id).Select(MyRow.Fields.SrNo)
                    .Where(new Criteria(MyRow.Fields.SrNo).IsNotNull()))
                .Where(r => r.SrNo.HasValue)
                .GroupBy(r => r.SrNo.Value)
                .ToDictionary(g => g.Key, g => g.First().Id.Value);
            int maxSrNo = idBySrNo.Count == 0 ? 0 : idBySrNo.Keys.Max();

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
                    var masterAccountId = defaultAccount.Id;
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

                    if (!srNo.HasValue)
                        srNo = ++maxSrNo;
                    else if (srNo.Value > maxSrNo)
                        maxSrNo = srNo.Value;

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

                    if (idBySrNo.TryGetValue(srNo.Value, out var existingId))
                    {
                        data.Id = existingId;
                        uow.Connection.UpdateById(data);
                        response.Updated++;
                    }
                    else
                    {
                        data.OwnerId = ownerId;
                        var newId = (int)uow.Connection.InsertAndGetID(data);
                        idBySrNo[srNo.Value] = newId;
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