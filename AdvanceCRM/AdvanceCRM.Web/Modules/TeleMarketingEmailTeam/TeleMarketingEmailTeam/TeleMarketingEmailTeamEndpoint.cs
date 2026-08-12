using AdvanceCRM.Web.Modules.Common.AppServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Serenity.Data;
using Serenity.Reporting;
using Serenity.Services;
using Serenity.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using MyRow = AdvanceCRM.TeleMarketingEmailTeam.TeleMarketingEmailTeamRow;

namespace AdvanceCRM.TeleMarketingEmailTeam.Endpoints
{
    [Route("Services/TeleMarketingEmailTeam/TeleMarketingEmailTeam/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class TeleMarketingEmailTeamController : ServiceEndpoint
    {
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] ITeleMarketingEmailTeamSaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] ITeleMarketingEmailTeamSaveHandler handler)
        {
            return handler.Update(uow, request);
        }

        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] ITeleMarketingEmailTeamDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] ITeleMarketingEmailTeamRetrieveHandler handler)
        {
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] ITeleMarketingEmailTeamListHandler handler)
        {
            return handler.List(connection, request);
        }

        [HttpPost, IgnoreAntiforgeryToken, AuthorizeList(typeof(MyRow))]
        public FileContentResult ListExcel(
            IDbConnection connection,
            [FromForm] ListRequest request,
            [FromForm] string Ids,
            [FromServices] ITeleMarketingEmailTeamListHandler handler)
        {
            request ??= new ListRequest { Take = 0 };

            // Account Number, Campaign ID and the owner's username live on joins, which a bare
            // export request does not select - ask for them or those columns come back empty.
            request.IncludeColumns ??= new HashSet<string>();
            request.IncludeColumns.Add(MyRow.Fields.MasterAccountNumber.PropertyName);
            request.IncludeColumns.Add(MyRow.Fields.CampaignCode.PropertyName);
            request.IncludeColumns.Add(MyRow.Fields.OwnerUsername.PropertyName);

            var data = List(connection, request, handler).Entities.ToList();
            if (!string.IsNullOrWhiteSpace(Ids))
            {
                var idList = Ids.Split(',').Select(x =>
                {
                    int v; return int.TryParse(x.Trim(), out v) ? (int?)v : null;
                }).Where(x => x.HasValue).Select(x => x!.Value).ToHashSet();
                if (idList.Count > 0)
                    data = data.Where(x => x.Id.HasValue && idList.Contains(x.Id.Value)).ToList();
            }

            var bytes = TeleMarketingEmailTeamExcelExporter.ExportToExcel(data);
            return ExcelContentResult.Create(bytes, "TeleMarketingEmailTeamList_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
        }

        /// <summary>
        /// An empty sheet carrying exactly the columns ImportExcel understands - one per field the
        /// TM Email Team form offers. Built here rather than shipped as a file so it can never
        /// drift from what the import actually reads. The Id column is left in so an exported
        /// sheet and the template are the same shape; rows that carry an Id are skipped on import.
        /// </summary>
        [HttpPost, IgnoreAntiforgeryToken, AuthorizeList(typeof(MyRow))]
        public FileContentResult DownloadTemplate()
        {
            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("TMEmailTeam");

            for (int i = 0; i < TeleMarketingEmailTeamExcelExporter.Headers.Length; i++)
            {
                var cell = ws.Cells[1, i + 1];
                cell.Value = TeleMarketingEmailTeamExcelExporter.Headers[i];
                cell.Style.Font.Bold = true;
                cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
            }

            // Spell the accepted Status values out on the sheet - the import rejects anything else.
            ws.Cells[1, TeleMarketingEmailTeamExcelExporter.Headers.Length - 1].AddComment(
                "Allowed values: " + ExcelEnumHelper.ValueList<TeleMarketingEmailTeamStatus>() +
                ". Leave empty to start the record as Pending.", "Import");

            ws.Cells[1, 1, 1, TeleMarketingEmailTeamExcelExporter.Headers.Length].AutoFitColumns(12, 30);
            ws.View.FreezePanes(2, 1);

            return ExcelContentResult.Create(package.GetAsByteArray(), "TMEmailTeam_Template.xlsx");
        }

        [HttpPost, IgnoreAntiforgeryToken]
        [RequestSizeLimit(52428800)]
        [RequestFormLimits(MultipartBodyLengthLimit = 52428800)]
        public IActionResult ImportExcel([FromServices] IUnitOfWork uow, IFormFile file,
            [FromServices] ITeleMarketingEmailTeamSaveHandler saveHandler)
        {
            try
            {
                if (file == null || !file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                    return Content("Please upload a valid .xlsx file.", "text/plain");

                int imported = 0, skipped = 0, failed = 0;
                var errors = new List<string>();

                using (var package = new ExcelPackage(file.OpenReadStream()))
                {
                    var ws = package.Workbook.Worksheets[0];
                    int rowCount = ws.Dimension.End.Row;
                    var map = ExcelImportHelper.BuildHeaderMap(ws);

                    // Account Number -> id and (account, campaign code) -> id, read once so the
                    // per-row lookups below cost nothing.
                    var accounts = ExcelImportHelper.LoadMasterAccountMap(uow.Connection);
                    var campaigns = ExcelImportHelper.LoadCampaignMap(uow.Connection);

                    for (int row = 2; row <= rowCount; row++)
                    {
                        try
                        {
                            // A Status the enum does not know would silently become "no status"
                            // and then be defaulted to Pending on create, quietly losing what the
                            // sheet said - so the row is rejected with the accepted values instead.
                            var statusRaw = ExcelImportHelper.GetText(ws, row, map, "Status");
                            if (!ExcelEnumHelper.TryParse<TeleMarketingEmailTeamStatus>(statusRaw, out var status))
                            {
                                failed++;
                                errors.Add($"Row {row}: Status '{statusRaw}' is not valid. " +
                                    "Allowed: " + ExcelEnumHelper.ValueList<TeleMarketingEmailTeamStatus>() + ".");
                                continue;
                            }

                            // The campaign code only identifies a campaign inside its account, so
                            // the account is resolved first and the campaign looked up within it.
                            var masterAccountId = ExcelImportHelper.GetMasterAccountId(ws, row, map, accounts,
                                    "Master Account No", "Master Account", "Account Number", "Account No")
                                ?? ExcelImportHelper.GetInt(ws, row, map, "MasterAccountId", "Master Account Id");

                            var emailTeam = new MyRow
                            {
                                Id = ExcelImportHelper.GetInt(ws, row, map, "Id"),
                                MasterAccountId = masterAccountId,
                                CampaignId = ExcelImportHelper.GetCampaignId(ws, row, map, campaigns, masterAccountId,
                                    "CampaignId", "Campaign Id", "Campaign"),
                                FirstName = ExcelImportHelper.GetText(ws, row, map, "FirstName", "First Name"),
                                LastName = ExcelImportHelper.GetText(ws, row, map, "LastName", "Last Name"),
                                Email = ExcelImportHelper.GetText(ws, row, map, "Email"),
                                OwnerId = ExcelImportHelper.GetUserId(ws, row, map, uow.Connection,
                                    "OwnerId", "Owner", "Owner Name", "OwnerName", "Created By", "CreatedBy"),
                            };

                            // Left unassigned when the sheet says nothing, so the save handler
                            // applies its own default (Pending) rather than this overriding it.
                            if (status != null)
                                emailTeam.Status = status;

                            if (emailTeam.Id.HasValue && emailTeam.Id.Value > 0)
                            {
                                skipped++; continue;
                            }

                            ExcelImportHelper.ClampStringFields(emailTeam);
                            saveHandler.Create(uow, new SaveRequest<MyRow> { Entity = emailTeam });
                            imported++;
                        }
                        catch (Exception ex)
                        {
                            failed++; errors.Add($"Row {row}: {ex.Message}");
                        }
                    }
                }

                if (imported == 0 && failed > 0)
                    return Content("All rows failed to import.\n" + string.Join("\n", errors), "text/plain");
                return Content($"Added: {imported}, Skipped (existing IDs): {skipped}, Failed: {failed}\n" +
                    (errors.Count > 0 ? string.Join("\n", errors) : ""), "text/plain");
            }
            catch (Exception ex)
            {
                return Content("Import failed: " + ex.Message + "\n" + ex.StackTrace, "text/plain");
            }
        }
    }
}
