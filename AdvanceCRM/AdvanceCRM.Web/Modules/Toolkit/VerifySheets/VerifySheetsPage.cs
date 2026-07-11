using Serenity.Web;
using Serenity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AdvanceCRM.Administration;
using AdvanceCRM.Masters;
using AdvanceCRM.Web.Modules.Common.AppServices;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
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
            // Master Suppression is account-wise, so it is pulled by the campaign's parent account.
            var masterAccountId = connection.TryById<DemandayCampaignIdRow>(campaignId)?.DemandayMasterAccountId ?? 0;
            var masterSupp = connection.List<MasterSupressionRow>(q => q.SelectTableFields()
                .Where(MasterSupressionRow.Fields.MasterAccountId == masterAccountId));
            var openCampaign = connection.List<OpenCampaignRow>(q => q.SelectTableFields()
                .Where(OpenCampaignRow.Fields.CampaignId == campaignId));

            // Resolve TAL "Agent" (a user id) to a display name.
            var userNames = connection.List<UserRow>()
                .Where(u => u.UserId != null)
                .GroupBy(u => u.UserId.Value)
                .ToDictionary(g => g.Key, g => g.First().DisplayName ?? g.First().Username);

            using var package = new ExcelPackage();

            WriteSheet(package, "Specification",
                new[] { "ID", "Sr No", "Order ID", "Job Title", "Job Level", "Job Function", "Industry", "City", "Country" },
                specs.Select(r => new object[] { r.Id, r.SrNo, r.OrderId, r.JobTitle, r.JobLevel, r.JobFunction, r.Industry, r.City, r.Country }));

            WriteSheet(package, "Email Suppression",
                new[] { "ID", "Sr No", "Company Name", "First Name", "Last Name", "Email", "Domain" },
                emailSupp.Select(r => new object[] { r.Id, r.SrNo, r.CompanyName, r.FirstName, r.LastName, r.Email, r.Domain }));

            WriteSheet(package, "Competitor List",
                new[] { "ID", "Sr No", "Company Name", "Domain", "Email", "CPC" },
                competitors.Select(r => new object[] { r.Id, r.SrNo, r.CompanyName, r.Domain, r.Email, r.Cpc }));

            WriteSheet(package, "TAL List",
                new[] { "ID", "Sr No", "Company Name", "Domain", "Agent" },
                tal.Select(r => new object[] { r.Id, r.SrNo, r.CompanyName, r.Domain,
                    r.AgentsName != null && userNames.ContainsKey(r.AgentsName.Value) ? userNames[r.AgentsName.Value] : null }));

            WriteSheet(package, "Master Suppression",
                new[] { "ID", "Sr No", "Company Name", "First Name", "Last Name", "Email", "Domain" },
                masterSupp.Select(r => new object[] { r.Id, r.SrNo, r.CompanyName, r.FirstName, r.LastName, r.Email, r.Domain }));

            WriteSheet(package, "Open Campaign",
                new[] { "ID", "Sr No", "Domain", "Demanday User", "Time Stamp" },
                openCampaign.Select(r => new object[] { r.Id, r.SrNo, r.Domain,
                    r.DemandayUserId != null && userNames.ContainsKey(r.DemandayUserId.Value) ? userNames[r.DemandayUserId.Value] : null,
                    r.TimeStamp }));

            byte[] bytes = package.GetAsByteArray();
            string fileName = "VerifySheets_Campaign_" + campaignId + "_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx";
            return File(bytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName);
        }

        // Uploads a sheet into the chosen Tool Kit sub-module. Most sheets are tagged with the
        // selected Campaign; Master Suppression is account-wise, so it takes a Master Account
        // instead and leaves CampaignId null. Duplicates (by the module's natural key within
        // that scope) are skipped.
        [HttpPost, IgnoreAntiforgeryToken]
        [Route("Toolkit/VerifySheets/ImportExcel")]
        [RequestSizeLimit(104857600)]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        public IActionResult ImportExcel([FromServices] ISqlConnections connections, IFormFile file,
            int campaignId, string sheet, int masterAccountId = 0)
        {
            if (file == null || !file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                return Content("Please upload a valid .xlsx file.", "text/plain");

            bool accountScoped = sheet == "MasterSuppression";
            if (accountScoped)
            {
                if (masterAccountId <= 0)
                    return Content("Please select a Master Account first.", "text/plain");
            }
            else if (campaignId <= 0)
            {
                return Content("Please select a Campaign first.", "text/plain");
            }

            int imported = 0, updated = 0;
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
                    var idBySrNo = LoadIdBySrNo<DemandaySpecsRow>(connection, f.Id, f.SrNo);
                    int maxSrNo = NextSeed(idBySrNo);
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var orderId = ParseLong(ExcelImportHelper.GetText(ws, row, map, "OrderId", "Order ID"));
                        var jobTitle = ExcelImportHelper.GetText(ws, row, map, "JobTitle", "Job Title");
                        if (orderId == null && string.IsNullOrWhiteSpace(jobTitle)) continue;
                        var srNo = ResolveSrNo(ws, row, map, ref maxSrNo);
                        var data = new DemandaySpecsRow
                        {
                            SrNo = srNo,
                            CampaignId = campaignId,
                            OrderId = orderId,
                            JobTitle = jobTitle,
                            JobLevel = ExcelImportHelper.GetText(ws, row, map, "JobLevel", "Job Level"),
                            JobFunction = ExcelImportHelper.GetText(ws, row, map, "JobFunction", "Job Function"),
                            Industry = ExcelImportHelper.GetText(ws, row, map, "Industry"),
                            City = ExcelImportHelper.GetText(ws, row, map, "City"),
                            Country = ExcelImportHelper.GetText(ws, row, map, "Country")
                        };
                        UpsertRow(connection, idBySrNo, srNo.Value, data, r => data.Id = r, ref imported, ref updated);
                    }
                }
                else if (sheet == "EmailSuppression")
                {
                    var f = ClientSupressionRow.Fields;
                    var idBySrNo = LoadIdBySrNo<ClientSupressionRow>(connection, f.Id, f.SrNo);
                    int maxSrNo = NextSeed(idBySrNo);
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var email = ExcelImportHelper.GetText(ws, row, map, "Email");
                        var company = ExcelImportHelper.GetText(ws, row, map, "CompanyName", "Company Name");
                        var domain = ExcelImportHelper.GetText(ws, row, map, "Domain");
                        if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(company) && string.IsNullOrWhiteSpace(domain)) continue;
                        var srNo = ResolveSrNo(ws, row, map, ref maxSrNo);
                        var data = new ClientSupressionRow
                        {
                            SrNo = srNo,
                            CampaignId = campaignId,
                            CompanyName = company,
                            FirstName = ExcelImportHelper.GetText(ws, row, map, "FirstName", "First Name"),
                            LastName = ExcelImportHelper.GetText(ws, row, map, "LastName", "Last Name"),
                            Email = email,
                            Domain = domain
                        };
                        UpsertRow(connection, idBySrNo, srNo.Value, data, r => data.Id = r, ref imported, ref updated);
                    }
                }
                else if (sheet == "CompetitorList")
                {
                    var f = DemandayCompetitorRow.Fields;
                    var idBySrNo = LoadIdBySrNo<DemandayCompetitorRow>(connection, f.Id, f.SrNo);
                    int maxSrNo = NextSeed(idBySrNo);
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var domain = ExcelImportHelper.GetText(ws, row, map, "Domain");
                        var company = ExcelImportHelper.GetText(ws, row, map, "CompanyName", "Company Name");
                        if (string.IsNullOrWhiteSpace(domain) && string.IsNullOrWhiteSpace(company)) continue;
                        var srNo = ResolveSrNo(ws, row, map, ref maxSrNo);
                        var data = new DemandayCompetitorRow
                        {
                            SrNo = srNo,
                            CampaignId = campaignId,
                            CompanyName = company,
                            Domain = domain,
                            Email = ExcelImportHelper.GetText(ws, row, map, "Email"),
                            // CPC is free text ("02 cpc", "$0.75"), so it is stored verbatim.
                            Cpc = ExcelImportHelper.GetText(ws, row, map, "Cpc", "CPC")?.Trim()
                        };
                        UpsertRow(connection, idBySrNo, srNo.Value, data, r => data.Id = r, ref imported, ref updated);
                    }
                }
                else if (sheet == "TALList")
                {
                    var f = TalCampaignRow.Fields;
                    var idBySrNo = LoadIdBySrNo<TalCampaignRow>(connection, f.Id, f.SrNo);
                    int maxSrNo = NextSeed(idBySrNo);
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var domain = ExcelImportHelper.GetText(ws, row, map, "Domain");
                        var company = ExcelImportHelper.GetText(ws, row, map, "CompanyName", "Company Name");
                        if (string.IsNullOrWhiteSpace(domain) && string.IsNullOrWhiteSpace(company)) continue;
                        var srNo = ResolveSrNo(ws, row, map, ref maxSrNo);
                        var data = new TalCampaignRow
                        {
                            SrNo = srNo,
                            CampaignId = campaignId,
                            CompanyName = company,
                            Domain = domain
                        };
                        UpsertRow(connection, idBySrNo, srNo.Value, data, r => data.Id = r, ref imported, ref updated);
                    }
                }
                else if (sheet == "MasterSuppression")
                {
                    // Account-wise: no campaign is involved.
                    var f = MasterSupressionRow.Fields;
                    var idBySrNo = LoadIdBySrNo<MasterSupressionRow>(connection, f.Id, f.SrNo);
                    int maxSrNo = NextSeed(idBySrNo);
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var email = ExcelImportHelper.GetText(ws, row, map, "Email");
                        var company = ExcelImportHelper.GetText(ws, row, map, "CompanyName", "Company Name");
                        var domain = ExcelImportHelper.GetText(ws, row, map, "Domain");
                        if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(company) && string.IsNullOrWhiteSpace(domain)) continue;
                        var srNo = ResolveSrNo(ws, row, map, ref maxSrNo);
                        var data = new MasterSupressionRow
                        {
                            SrNo = srNo,
                            MasterAccountId = masterAccountId,
                            CompanyName = company,
                            FirstName = ExcelImportHelper.GetText(ws, row, map, "FirstName", "First Name"),
                            LastName = ExcelImportHelper.GetText(ws, row, map, "LastName", "Last Name"),
                            Email = email,
                            Domain = domain
                        };
                        UpsertRow(connection, idBySrNo, srNo.Value, data, r => data.Id = r, ref imported, ref updated);
                    }
                }
                else if (sheet == "OpenCampaign")
                {
                    var f = OpenCampaignRow.Fields;
                    var idBySrNo = LoadIdBySrNo<OpenCampaignRow>(connection, f.Id, f.SrNo);
                    int maxSrNo = NextSeed(idBySrNo);
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var domain = ExcelImportHelper.GetText(ws, row, map, "Domain");
                        if (string.IsNullOrWhiteSpace(domain)) continue;
                        var srNo = ResolveSrNo(ws, row, map, ref maxSrNo);
                        var data = new OpenCampaignRow
                        {
                            SrNo = srNo,
                            CampaignId = campaignId,
                            Domain = domain
                        };
                        UpsertRow(connection, idBySrNo, srNo.Value, data, r => data.Id = r, ref imported, ref updated);
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

            return Content($"Imported {imported}, updated {updated} (by Sr No) into '{sheet}'.", "text/plain");
        }

        // SrNo -> Id for every existing row that has one. SrNo is the globally-unique upsert key.
        private static Dictionary<int, int> LoadIdBySrNo<TRow>(IDbConnection connection,
            Serenity.Data.Int32Field idField, Serenity.Data.Int32Field srNoField)
            where TRow : class, IIdRow, new()
        {
            return connection.List<TRow>(q => q
                    .Select(idField).Select(srNoField)
                    .Where(new Criteria(srNoField).IsNotNull()))
                .Select(r => new { Id = (int?)idField[r], SrNo = (int?)srNoField[r] })
                .Where(x => x.SrNo.HasValue && x.Id.HasValue)
                .GroupBy(x => x.SrNo.Value)
                .ToDictionary(g => g.Key, g => g.First().Id.Value);
        }

        private static int NextSeed(Dictionary<int, int> idBySrNo)
            => idBySrNo.Count == 0 ? 0 : idBySrNo.Keys.Max();

        // Reads the SrNo cell; a blank one is assigned the next free number so the row still gets one.
        private static int? ResolveSrNo(ExcelWorksheet ws, int row, Dictionary<string, int> map, ref int maxSrNo)
        {
            var srNo = ExcelImportHelper.GetInt(ws, row, map, "SrNo", "Sr No");
            if (!srNo.HasValue)
                srNo = ++maxSrNo;
            else if (srNo.Value > maxSrNo)
                maxSrNo = srNo.Value;
            return srNo;
        }

        // Update the row whose SrNo already exists, otherwise insert. setId assigns Id on the row
        // for the update path. UpdateById writes only assigned fields, so OwnerId is preserved.
        private static void UpsertRow<TRow>(IDbConnection connection, Dictionary<int, int> idBySrNo,
            int srNo, TRow data, Action<int> setId, ref int imported, ref int updated)
            where TRow : class, IIdRow
        {
            if (idBySrNo.TryGetValue(srNo, out var existingId))
            {
                setId(existingId);
                connection.UpdateById(data);
                updated++;
            }
            else
            {
                var newId = (int)connection.InsertAndGetID(data);
                idBySrNo[srNo] = newId;
                imported++;
            }
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
