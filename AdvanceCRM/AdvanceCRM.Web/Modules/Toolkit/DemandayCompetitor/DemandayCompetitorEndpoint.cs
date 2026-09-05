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
using MyRow = AdvanceCRM.Toolkit.DemandayCompetitorRow;

namespace AdvanceCRM.Toolkit.Endpoints
{
    [Route("Services/Toolkit/DemandayCompetitor/[action]")]
    [ConnectionKey(typeof(MyRow))]
    public class DemandayCompetitorController : ServiceEndpoint
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public DemandayCompetitorController(
            IConfiguration configuration,
            IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
            UploadHelper.Configure(configuration, env);
        }

        private void CheckReadPermission()
        {
            if (!Authorization.HasPermission("DemandayCompetitor:Read") &&
                !Authorization.HasPermission("Toolkit:VerifySheets") &&
                !Authorization.HasPermission("Toolkit:VerifySheets:Competitor") &&
                !Authorization.HasPermission("Toolkit:VerifySheets:DemandayCompetitor"))
                throw new ValidationError("Access denied");
        }

        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayCompetitorSaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayCompetitorSaveHandler handler)
        {
            return handler.Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] IDemandayCompetitorDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] IDemandayCompetitorRetrieveHandler handler)
        {
            CheckReadPermission();
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] IDemandayCompetitorListHandler handler)
        {
            CheckReadPermission();
            return handler.List(connection, request);
        }

        [HttpPost]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
            [FromServices] IDemandayCompetitorListHandler handler,
            [FromServices] IExcelExporter exporter)
        {
            CheckReadPermission();
            var data = List(connection, request, handler).Entities;
            var bytes = exporter.Export(data, typeof(Columns.DemandayCompetitorColumns), request.ExportColumns);
            return ExcelContentResult.Create(bytes, "DemandayCompetitorList_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
        }

        [HttpPost, ServiceAuthorize("DemandayCompetitor:Import")]
        public ActionResult DownloadTemplate(IDbConnection connection, RetrieveRequest request)
        {
            string[] headers = { "Sr No", "Master Account ID", "Campaign ID", "Company Name", "Domain", "Email", "CPC" };

            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("DemandayCompetitor");
                for (int i = 0; i < headers.Length; i++)
                {
                    var cell = ws.Cells[1, i + 1];
                    cell.Value = headers[i];
                    cell.Style.Font.Bold = true;
                    ws.Column(i + 1).Width = 25;
                }

                byte[] bytes = package.GetAsByteArray();
                return File(bytes,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "DemandayCompetitor_Template.xlsx");
            }
        }

        [HttpPost, ServiceAuthorize("DemandayCompetitor:Import")]
        public ExcelImportResponse ExcelImport(IUnitOfWork uow, DemandayCompetitorExcelImportRequest request)
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

            // A row may name a different campaign than the one picked in the dialog, so one file
            // can span several campaigns / accounts; neither has to already exist - a name that
            // doesn't resolve is created on the fly (Campaign cascades under its Master Account).
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
                    var companyName = Convert.ToString(worksheet.Cells[row, 4].Value ?? "").Trim();
                    var domain = Convert.ToString(worksheet.Cells[row, 5].Value ?? "").Trim();
                    var email = Convert.ToString(worksheet.Cells[row, 6].Value ?? "").Trim();
                    // CPC is free text – read the displayed value so "02 cpc" and "$0.75" survive.
                    var cpc = (worksheet.Cells[row, 7].Text ?? "").Trim();

                    if (string.IsNullOrEmpty(companyName) && string.IsNullOrEmpty(domain) && string.IsNullOrEmpty(email))
                        continue;

                    if (cpc.Length > 50)
                    {
                        response.ErrorList.Add($"Row {row}: CPC '{cpc}' is longer than 50 characters");
                        continue;
                    }

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
                        CompanyName = companyName,
                        Domain = domain,
                        Email = email,
                        Cpc = string.IsNullOrEmpty(cpc) ? null : cpc,
                        CampaignId = rowCampaignId,
                        MasterAccountId = rowMasterAccountId
                    };

                    var upsertKey = (AccountId: scopeKey.AccountId, CampaignId: scopeKey.CampaignId, SrNo: srNo.Value);

                    if (idByScopeAndSrNo.TryGetValue(upsertKey, out var existingId))
                    {
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