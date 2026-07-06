using Serenity.Web;
using Serenity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AdvanceCRM.Administration;
using AdvanceCRM.Web.Modules.Common.AppServices;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AdvanceCRM.Toolkit.Pages
{
    [PageAuthorize("Toolkit:VerifySheets")]
    public class VerifySheetsController : Controller
    {
        [Route("Toolkit/VerifySheets")]
        public ActionResult Index()
        {
            return View("~/Modules/Toolkit/VerifySheets/VerifySheetsIndex.cshtml");
        }

        // One workbook, one worksheet per Tool Kit sub-module, all filtered by the selected Campaign.
        [Route("Toolkit/VerifySheets/ExportExcel")]
        public FileContentResult ExportExcel([FromServices] ISqlConnections connections, int campaignId)
        {
            using var connection = connections.NewByKey("Default");

            var specs = connection.List<DemandaySpecsRow>(q => q.SelectTableFields()
                .Where(DemandaySpecsRow.Fields.CampaignId == campaignId));
            var emailSupp = connection.List<ClientSupressionRow>(q => q.SelectTableFields()
                .Where(ClientSupressionRow.Fields.CampaignId == campaignId));
            var competitors = connection.List<DemandayCompetitorRow>(q => q.SelectTableFields()
                .Where(DemandayCompetitorRow.Fields.CampaignId == campaignId));
            var tal = connection.List<TalCampaignRow>(q => q.SelectTableFields()
                .Where(TalCampaignRow.Fields.CampaignId == campaignId));
            var masterSupp = connection.List<MasterSupressionRow>(q => q.SelectTableFields()
                .Where(MasterSupressionRow.Fields.CampaignId == campaignId));

            // Resolve TAL "Agent" (a user id) to a display name.
            var userNames = connection.List<UserRow>()
                .Where(u => u.UserId != null)
                .GroupBy(u => u.UserId.Value)
                .ToDictionary(g => g.Key, g => g.First().DisplayName ?? g.First().Username);

            using var package = new ExcelPackage();

            WriteSheet(package, "Specification",
                new[] { "ID", "Order ID", "Job Title", "Job Level", "Job Function", "Industry", "City", "Country" },
                specs.Select(r => new object[] { r.Id, r.OrderId, r.JobTitle, r.JobLevel, r.JobFunction, r.Industry, r.City, r.Country }));

            WriteSheet(package, "Email Suppression",
                new[] { "ID", "Company Name", "First Name", "Last Name", "Email", "Domain" },
                emailSupp.Select(r => new object[] { r.Id, r.CompanyName, r.FirstName, r.LastName, r.Email, r.Domain }));

            WriteSheet(package, "Competitor List",
                new[] { "ID", "Company Name", "Domain", "Email", "CPC" },
                competitors.Select(r => new object[] { r.Id, r.CompanyName, r.Domain, r.Email, r.Cpc }));

            WriteSheet(package, "TAL List",
                new[] { "ID", "Company Name", "Domain", "Agent" },
                tal.Select(r => new object[] { r.Id, r.CompanyName, r.Domain,
                    r.AgentsName != null && userNames.ContainsKey(r.AgentsName.Value) ? userNames[r.AgentsName.Value] : null }));

            WriteSheet(package, "Master Suppression",
                new[] { "ID", "Company Name", "First Name", "Last Name", "Email", "Domain" },
                masterSupp.Select(r => new object[] { r.Id, r.CompanyName, r.FirstName, r.LastName, r.Email, r.Domain }));

            byte[] bytes = package.GetAsByteArray();
            string fileName = "VerifySheets_Campaign_" + campaignId + "_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx";
            return File(bytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName);
        }

        // Uploads a sheet into the chosen Tool Kit sub-module, tagged with the selected Campaign.
        // Duplicates (by the module's natural key within the campaign) are skipped.
        [HttpPost, IgnoreAntiforgeryToken]
        [Route("Toolkit/VerifySheets/ImportExcel")]
        [RequestSizeLimit(104857600)]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        public IActionResult ImportExcel([FromServices] ISqlConnections connections, IFormFile file, int campaignId, string sheet)
        {
            if (file == null || !file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                return Content("Please upload a valid .xlsx file.", "text/plain");
            if (campaignId <= 0)
                return Content("Please select a Campaign first.", "text/plain");

            int imported = 0, skipped = 0;
            try
            {
                using var connection = connections.NewByKey("Default");
                using var package = new ExcelPackage(file.OpenReadStream());
                var ws = package.Workbook.Worksheets.Count > 0 ? package.Workbook.Worksheets[0] : null;
                if (ws?.Dimension == null)
                    return Content("The uploaded file has no data.", "text/plain");
                int rowCount = ws.Dimension.End.Row;
                var map = ExcelImportHelper.BuildHeaderMap(ws);

                if (sheet == "Specification")
                {
                    var f = DemandaySpecsRow.Fields;
                    var existing = new HashSet<long>(connection.List<DemandaySpecsRow>(q => q.Select(f.OrderId)
                        .Where(f.CampaignId == campaignId)).Where(r => r.OrderId.HasValue).Select(r => r.OrderId.Value));
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var orderId = ParseLong(ExcelImportHelper.GetText(ws, row, map, "OrderId", "Order ID"));
                        var jobTitle = ExcelImportHelper.GetText(ws, row, map, "JobTitle", "Job Title");
                        if (orderId == null && string.IsNullOrWhiteSpace(jobTitle)) continue;
                        if (orderId.HasValue && existing.Contains(orderId.Value)) { skipped++; continue; }
                        connection.Insert(new DemandaySpecsRow
                        {
                            CampaignId = campaignId,
                            OrderId = orderId,
                            JobTitle = jobTitle,
                            JobLevel = ExcelImportHelper.GetText(ws, row, map, "JobLevel", "Job Level"),
                            JobFunction = ExcelImportHelper.GetText(ws, row, map, "JobFunction", "Job Function"),
                            Industry = ExcelImportHelper.GetText(ws, row, map, "Industry"),
                            City = ExcelImportHelper.GetText(ws, row, map, "City"),
                            Country = ExcelImportHelper.GetText(ws, row, map, "Country")
                        });
                        if (orderId.HasValue) existing.Add(orderId.Value);
                        imported++;
                    }
                }
                else if (sheet == "EmailSuppression")
                {
                    var f = ClientSupressionRow.Fields;
                    var existing = new HashSet<string>(connection.List<ClientSupressionRow>(q => q.Select(f.Email)
                        .Where(f.CampaignId == campaignId)).Where(r => r.Email != null).Select(r => r.Email.Trim()),
                        StringComparer.OrdinalIgnoreCase);
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var email = ExcelImportHelper.GetText(ws, row, map, "Email");
                        var company = ExcelImportHelper.GetText(ws, row, map, "CompanyName", "Company Name");
                        var domain = ExcelImportHelper.GetText(ws, row, map, "Domain");
                        if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(company) && string.IsNullOrWhiteSpace(domain)) continue;
                        if (!string.IsNullOrWhiteSpace(email) && existing.Contains(email.Trim())) { skipped++; continue; }
                        connection.Insert(new ClientSupressionRow
                        {
                            CampaignId = campaignId,
                            CompanyName = company,
                            FirstName = ExcelImportHelper.GetText(ws, row, map, "FirstName", "First Name"),
                            LastName = ExcelImportHelper.GetText(ws, row, map, "LastName", "Last Name"),
                            Email = email,
                            Domain = domain
                        });
                        if (!string.IsNullOrWhiteSpace(email)) existing.Add(email.Trim());
                        imported++;
                    }
                }
                else if (sheet == "CompetitorList")
                {
                    var f = DemandayCompetitorRow.Fields;
                    var existing = new HashSet<string>(connection.List<DemandayCompetitorRow>(q => q.Select(f.Domain)
                        .Where(f.CampaignId == campaignId)).Where(r => r.Domain != null).Select(r => r.Domain.Trim()),
                        StringComparer.OrdinalIgnoreCase);
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var domain = ExcelImportHelper.GetText(ws, row, map, "Domain");
                        var company = ExcelImportHelper.GetText(ws, row, map, "CompanyName", "Company Name");
                        if (string.IsNullOrWhiteSpace(domain) && string.IsNullOrWhiteSpace(company)) continue;
                        if (!string.IsNullOrWhiteSpace(domain) && existing.Contains(domain.Trim())) { skipped++; continue; }
                        connection.Insert(new DemandayCompetitorRow
                        {
                            CampaignId = campaignId,
                            CompanyName = company,
                            Domain = domain,
                            Email = ExcelImportHelper.GetText(ws, row, map, "Email"),
                            Cpc = ParseLong(ExcelImportHelper.GetText(ws, row, map, "Cpc", "CPC"))
                        });
                        if (!string.IsNullOrWhiteSpace(domain)) existing.Add(domain.Trim());
                        imported++;
                    }
                }
                else if (sheet == "TALList")
                {
                    var f = TalCampaignRow.Fields;
                    var existing = new HashSet<string>(connection.List<TalCampaignRow>(q => q.Select(f.Domain)
                        .Where(f.CampaignId == campaignId)).Where(r => r.Domain != null).Select(r => r.Domain.Trim()),
                        StringComparer.OrdinalIgnoreCase);
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var domain = ExcelImportHelper.GetText(ws, row, map, "Domain");
                        var company = ExcelImportHelper.GetText(ws, row, map, "CompanyName", "Company Name");
                        if (string.IsNullOrWhiteSpace(domain) && string.IsNullOrWhiteSpace(company)) continue;
                        if (!string.IsNullOrWhiteSpace(domain) && existing.Contains(domain.Trim())) { skipped++; continue; }
                        connection.Insert(new TalCampaignRow
                        {
                            CampaignId = campaignId,
                            CompanyName = company,
                            Domain = domain
                        });
                        if (!string.IsNullOrWhiteSpace(domain)) existing.Add(domain.Trim());
                        imported++;
                    }
                }
                else if (sheet == "MasterSuppression")
                {
                    var f = MasterSupressionRow.Fields;
                    var existing = new HashSet<string>(connection.List<MasterSupressionRow>(q => q.Select(f.Email)
                        .Where(f.CampaignId == campaignId)).Where(r => r.Email != null).Select(r => r.Email.Trim()),
                        StringComparer.OrdinalIgnoreCase);
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var email = ExcelImportHelper.GetText(ws, row, map, "Email");
                        var company = ExcelImportHelper.GetText(ws, row, map, "CompanyName", "Company Name");
                        var domain = ExcelImportHelper.GetText(ws, row, map, "Domain");
                        if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(company) && string.IsNullOrWhiteSpace(domain)) continue;
                        if (!string.IsNullOrWhiteSpace(email) && existing.Contains(email.Trim())) { skipped++; continue; }
                        connection.Insert(new MasterSupressionRow
                        {
                            CampaignId = campaignId,
                            CompanyName = company,
                            FirstName = ExcelImportHelper.GetText(ws, row, map, "FirstName", "First Name"),
                            LastName = ExcelImportHelper.GetText(ws, row, map, "LastName", "Last Name"),
                            Email = email,
                            Domain = domain
                        });
                        if (!string.IsNullOrWhiteSpace(email)) existing.Add(email.Trim());
                        imported++;
                    }
                }
                else
                {
                    return Content("Unknown sheet type: " + sheet, "text/plain");
                }
            }
            catch (Exception ex)
            {
                return Content("Import failed: " + ex.Message, "text/plain");
            }

            return Content($"Imported {imported}, skipped {skipped} duplicate/blank into '{sheet}'.", "text/plain");
        }

        private static long? ParseLong(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;
            return long.TryParse(s.Trim(), out var v) ? v : (long?)null;
        }

        private static void WriteSheet(ExcelPackage package, string sheetName, string[] headers, IEnumerable<object[]> rows)
        {
            var ws = package.Workbook.Worksheets.Add(sheetName);

            for (int c = 0; c < headers.Length; c++)
            {
                var cell = ws.Cells[1, c + 1];
                cell.Value = headers[c];
                cell.Style.Font.Bold = true;
                ws.Column(c + 1).Width = 22;
            }

            int r = 2;
            foreach (var row in rows)
            {
                for (int c = 0; c < row.Length; c++)
                    ws.Cells[r, c + 1].Value = row[c];
                r++;
            }
        }
    }
}
