using Serenity.Web;
using Serenity.Data;
using Microsoft.AspNetCore.Mvc;
using AdvanceCRM.Administration;
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
