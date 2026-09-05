using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serenity;
using Serenity.Data;
using Serenity.Reporting;
using Serenity.Services;
using Serenity.Web;
using AdvanceCRM.Web.Helpers;
using AdvanceCRM.Toolkit;
using AdvanceCRM.Masters;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using MyRow = AdvanceCRM.Toolkit.DemandaySpecsRow;

namespace AdvanceCRM.Toolkit.Endpoints
{
    [Route("Services/Toolkit/DemandaySpecs/[action]")]
    [ConnectionKey(typeof(MyRow))]
    public class DemandaySpecsController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public DemandaySpecsController(
            ISqlConnections connections,
            IConfiguration configuration,
            IWebHostEnvironment env)
        {
            _connections = connections;
            _configuration = configuration;
            _env = env;
            UploadHelper.Configure(configuration, env);

            // Ensure template file exists and is valid
            string templatePath = Path.Combine(env.ContentRootPath, "Templates", "DemandaySpecs_Template.xlsx");
            DemandaySpecsTemplateInitializer.EnsureTemplateExists(templatePath);
        }

        private void CheckReadPermission()
        {
            if (!Authorization.HasPermission("DemandaySpecs:Read") &&
                !Authorization.HasPermission("Toolkit:VerifySheets") &&
                !Authorization.HasPermission("Toolkit:VerifySheets:Specification"))
                throw new ValidationError("Access denied");
        }

        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandaySpecsSaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandaySpecsSaveHandler handler)
        {
            return handler.Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] IDemandaySpecsDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] IDemandaySpecsRetrieveHandler handler)
        {
            CheckReadPermission();
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] IDemandaySpecsListHandler handler)
        {
            CheckReadPermission();
            return handler.List(connection, request);
        }

        [HttpPost]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
            [FromServices] IDemandaySpecsListHandler handler,
            [FromServices] IExcelExporter exporter)
        {
            CheckReadPermission();
            var data = List(connection, request, handler).Entities;
            var bytes = exporter.Export(data, typeof(Columns.DemandaySpecsColumns), request.ExportColumns);
            return ExcelContentResult.Create(bytes, "DemandaySpecsList_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
        }

        [HttpPost, ServiceAuthorize("DemandaySpecs:Import")]
        public ActionResult DownloadTemplate(IDbConnection connection, RetrieveRequest request)
        {
            string templateFile = Path.Combine(_env.ContentRootPath, "Templates", "DemandaySpecs_Template.xlsx");
            byte[] bytes = System.IO.File.ReadAllBytes(templateFile);

            var Output = File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, "DemandaySpecs_Template.xlsx");
            return Output;
        }

        [HttpPost, ServiceAuthorize("DemandaySpecs:Import")]
        public ExcelImportResponse ExcelImport(IUnitOfWork uow, DemandaySpecsExcelImportRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrWhiteSpace(request.FileName))
                throw new ArgumentNullException(nameof(request.FileName));

            UploadHelper.CheckFileNameSecurity(request.FileName);

            if (!request.FileName.StartsWith("temporary/", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentOutOfRangeException("filename");

            // The Campaign in the dialog is optional: when picked, it is the default every row
            // without its own Master Account Id / Campaign Id column falls back to; a row that
            // has neither is rejected below instead of crashing the whole import.
            DemandayCampaignIdRow campaign = null;
            if (request.CampaignId != null)
            {
                campaign = uow.Connection.TryById<DemandayCampaignIdRow>(request.CampaignId.Value);
                if (campaign == null)
                    throw new ValidationError("Selected campaign was not found");
            }

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

            // A row may name a different campaign / account than the one picked in the dialog;
            // neither has to already exist - a name that doesn't resolve is created on the fly
            // (Campaign cascades under its resolved Master Account).
            var accountLookup = uow.Connection.List<DemandayMasterAccountRow>()
                .Where(a => !string.IsNullOrWhiteSpace(a.AccountNumber) && a.Id.HasValue)
                .GroupBy(a => a.AccountNumber.Trim(), StringComparer.OrdinalIgnoreCase)
                .ToDictionary(g => g.Key, g => g.First().Id.Value, StringComparer.OrdinalIgnoreCase);

            var campaignLookup = uow.Connection.List<DemandayCampaignIdRow>()
                .Where(c => c.Id.HasValue && c.DemandayMasterAccountId.HasValue && !string.IsNullOrWhiteSpace(c.CampaignId))
                .GroupBy(c => (AccountId: c.DemandayMasterAccountId.Value, CampaignId: c.CampaignId.Trim().ToUpperInvariant()))
                .ToDictionary(g => g.Key, g => g.First().Id.Value);

            int ResolveOrCreateAccount(string accountNumber)
            {
                var key = accountNumber.Trim();
                if (accountLookup.TryGetValue(key, out var existingId))
                    return existingId;
                var newId = (int)uow.Connection.InsertAndGetID(new DemandayMasterAccountRow { AccountNumber = key });
                accountLookup[key] = newId;
                return newId;
            }

            int ResolveOrCreateCampaign(int accountId, string campaignIdText)
            {
                var key = (AccountId: accountId, CampaignId: campaignIdText.Trim().ToUpperInvariant());
                if (campaignLookup.TryGetValue(key, out var existingId))
                    return existingId;
                var newId = (int)uow.Connection.InsertAndGetID(new DemandayCampaignIdRow
                {
                    CampaignId = campaignIdText.Trim(),
                    DemandayMasterAccountId = accountId
                });
                campaignLookup[key] = newId;
                return newId;
            }

            // SrNo (column 1) is the upsert key, but only within the same Master Account +
            // Campaign: two campaigns may legitimately reuse the same SrNo, so the key is
            // (MasterAccountId, CampaignId, SrNo), not SrNo alone. That also lets every
            // campaign restart its own numbering at 1.
            var existingRows = uow.Connection.List<MyRow>(q => q
                    .Select(MyRow.Fields.Id).Select(MyRow.Fields.SrNo)
                    .Select(MyRow.Fields.MasterAccountId).Select(MyRow.Fields.CampaignId)
                    .Where(new Criteria(MyRow.Fields.SrNo).IsNotNull()))
                .Where(r => r.SrNo.HasValue && r.MasterAccountId.HasValue && r.CampaignId.HasValue)
                .ToList();

            var idByScopeAndSrNo = existingRows
                .GroupBy(r => (AccountId: r.MasterAccountId.Value, CampaignId: r.CampaignId.Value, SrNo: r.SrNo.Value))
                .ToDictionary(g => g.Key, g => g.First().Id.Value);

            // Highest SrNo per campaign, so a blank SrNo cell continues that campaign's own run.
            var maxSrNoByScope = existingRows
                .GroupBy(r => (AccountId: r.MasterAccountId.Value, CampaignId: r.CampaignId.Value))
                .ToDictionary(g => g.Key, g => g.Max(r => r.SrNo.Value));

            for (var row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                try
                {
                    var srNoStr = Convert.ToString(worksheet.Cells[row, 1].Value ?? "").Trim();
                    var masterAccountIdStr = Convert.ToString(worksheet.Cells[row, 2].Value ?? "").Trim();
                    var campaignIdStr = Convert.ToString(worksheet.Cells[row, 3].Value ?? "").Trim();
                    var orderId = Convert.ToString(worksheet.Cells[row, 4].Value ?? "").Trim();
                    var jobTitle = Convert.ToString(worksheet.Cells[row, 5].Value ?? "").Trim();
                    var jobLevel = Convert.ToString(worksheet.Cells[row, 6].Value ?? "").Trim();
                    var jobFunction = Convert.ToString(worksheet.Cells[row, 7].Value ?? "").Trim();
                    var industry = Convert.ToString(worksheet.Cells[row, 8].Value ?? "").Trim();
                    var companyEmployeeSize = Convert.ToString(worksheet.Cells[row, 9].Value ?? "").Trim();
                    var annualRevenue = Convert.ToString(worksheet.Cells[row, 10].Value ?? "").Trim();
                    var excludeCompany = Convert.ToString(worksheet.Cells[row, 11].Value ?? "").Trim();
                    var address = Convert.ToString(worksheet.Cells[row, 12].Value ?? "").Trim();
                    var city = Convert.ToString(worksheet.Cells[row, 13].Value ?? "").Trim();
                    var state = Convert.ToString(worksheet.Cells[row, 14].Value ?? "").Trim();
                    var zipCode = Convert.ToString(worksheet.Cells[row, 15].Value ?? "").Trim();
                    var country = Convert.ToString(worksheet.Cells[row, 16].Value ?? "").Trim();
                    var comments = Convert.ToString(worksheet.Cells[row, 17].Value ?? "").Trim();
                    var additionalNotes = Convert.ToString(worksheet.Cells[row, 18].Value ?? "").Trim();

                    // Skip empty rows (at least JobTitle or OrderId should have a value)
                    if (string.IsNullOrEmpty(jobTitle) && string.IsNullOrEmpty(orderId))
                        continue;

                    // Blank Campaign Id falls back to the dialog's Campaign (if one was picked); a
                    // named one that doesn't exist yet is created automatically under its Master
                    // Account (also created automatically when the Master Account Id cell names a
                    // new one).
                    var rowCampaignId = campaign?.Id;
                    var rowMasterAccountId = campaign?.DemandayMasterAccountId;
                    if (!string.IsNullOrEmpty(campaignIdStr))
                    {
                        int? scopeAccountId = string.IsNullOrEmpty(masterAccountIdStr)
                            ? rowMasterAccountId
                            : ResolveOrCreateAccount(masterAccountIdStr);
                        if (!scopeAccountId.HasValue)
                        {
                            response.ErrorList.Add($"Row {row}: Campaign ID '{campaignIdStr}' needs a Master Account ID (no Campaign was selected in the dialog either)");
                            continue;
                        }
                        rowMasterAccountId = scopeAccountId;
                        rowCampaignId = ResolveOrCreateCampaign(scopeAccountId.Value, campaignIdStr);
                    }
                    else if (!string.IsNullOrEmpty(masterAccountIdStr))
                    {
                        rowMasterAccountId = ResolveOrCreateAccount(masterAccountIdStr);
                    }

                    if (!rowCampaignId.HasValue || !rowMasterAccountId.HasValue)
                    {
                        response.ErrorList.Add($"Row {row}: Select a Campaign in the dialog, or fill in the Master Account ID / Campaign ID columns");
                        continue;
                    }

                    var scopeKey = (AccountId: rowMasterAccountId.Value, CampaignId: rowCampaignId.Value);

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

                    // Numbering is tracked per campaign, so a different campaign's blank cells
                    // start at 1 even when another campaign already runs into the thousands.
                    maxSrNoByScope.TryGetValue(scopeKey, out var scopeMaxSrNo);

                    if (!srNo.HasValue)
                        srNo = scopeMaxSrNo + 1;

                    if (srNo.Value > scopeMaxSrNo)
                        maxSrNoByScope[scopeKey] = srNo.Value;

                    var data = new MyRow
                    {
                        SrNo = srNo,
                        OrderId = string.IsNullOrEmpty(orderId) ? (long?)null : Convert.ToInt64(orderId),
                        JobTitle = jobTitle,
                        JobLevel = jobLevel,
                        JobFunction = jobFunction,
                        Industry = industry,
                        CompanyEmployeeSize = companyEmployeeSize,
                        AnnualRevenue = annualRevenue,
                        ExcludeCompany = excludeCompany,
                        Address = address,
                        City = city,
                        State = state,
                        ZipCode = zipCode,
                        Country = country,
                        Comments = comments,
                        AdditionalNotes = additionalNotes,
                        CampaignId = rowCampaignId,
                        MasterAccountId = rowMasterAccountId
                    };

                    var upsertKey = (AccountId: scopeKey.AccountId, CampaignId: scopeKey.CampaignId, SrNo: srNo.Value);

                    if (idByScopeAndSrNo.TryGetValue(upsertKey, out var existingId))
                    {
                        // UpdateById only writes assigned fields, so OwnerId (creator) is preserved.
                        data.Id = existingId;
                        uow.Connection.UpdateById(data);
                        response.Updated++;
                    }
                    else
                    {
                        data.OwnerId = ownerId;
                        var newId = (int)uow.Connection.InsertAndGetID(data);
                        idByScopeAndSrNo[upsertKey] = newId;
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