using Serenity.Web;
using Serenity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AdvanceCRM.Administration;
using AdvanceCRM.Masters;
using AdvanceCRM.Web.Modules.Common.AppServices;
using Microsoft.Data.SqlClient;
using Serenity;
using Serenity.Abstractions;
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

        // Exports each Tool Kit sub-module as its own .xlsx (named {accountId}_{campaignId}_{module}.xlsx),
        // all filtered by the selected Campaign, bundled together into a single .zip download.
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

            // Master Suppression stores a numeric campaign key; export the human-facing Campaign ID
            // so the sheet round-trips back through the import.
            var campaignTexts = connection.List<DemandayCampaignIdRow>(q => q
                    .Select(DemandayCampaignIdRow.Fields.Id)
                    .Select(DemandayCampaignIdRow.Fields.CampaignId))
                .Where(c => c.Id != null)
                .GroupBy(c => c.Id.Value)
                .ToDictionary(g => g.Key, g => g.First().CampaignId);

            // One separate .xlsx per module, named {accountId}_{campaignId}_{module}.xlsx.
            // A single HTTP response can carry one file, so they are bundled into a .zip.
            var modules = new List<(string Name, string[] Headers, IEnumerable<object[]> Rows)>
            {
                ("Specification",
                    new[] { "Sr No", "Order ID", "Job Title", "Job Level", "Job Function", "Industry",
                        "Company Employee Size", "Annual Revenue", "Exclude Company", "Address", "City", "State",
                        "Zip Code", "Country", "Comments", "Additional Notes" },
                    specs.Select(r => new object[] { r.SrNo, r.OrderId, r.JobTitle, r.JobLevel, r.JobFunction,
                        r.Industry, r.CompanyEmployeeSize, r.AnnualRevenue, r.ExcludeCompany, r.Address, r.City, r.State,
                        r.ZipCode, r.Country, r.Comments, r.AdditionalNotes })),

                ("EmailSuppression",
                    new[] { "Sr No", "Company Name", "First Name", "Last Name", "Email", "Domain" },
                    emailSupp.Select(r => new object[] { r.SrNo, r.CompanyName, r.FirstName, r.LastName, r.Email, r.Domain })),

                ("CompetitorList",
                    new[] { "Sr No", "Company Name", "Domain", "Email", "CPC" },
                    competitors.Select(r => new object[] { r.SrNo, r.CompanyName, r.Domain, r.Email, r.Cpc })),

                ("TALList",
                    new[] { "Sr No", "Company Name", "Domain", "Agent", "Reason", "CPC" },
                    tal.Select(r => new object[] { r.SrNo, r.CompanyName, r.Domain,
                        r.AgentsName != null && userNames.ContainsKey(r.AgentsName.Value) ? userNames[r.AgentsName.Value] : null,
                        r.Reason, r.Cpc })),

                ("MasterSuppression",
                    new[] { "Sr No", "Campaign ID", "Company Name", "First Name", "Last Name", "Email", "Domain", "Date" },
                    masterSupp.Select(r => new object[] { r.SrNo,
                        r.CampaignId != null && campaignTexts.ContainsKey(r.CampaignId.Value) ? campaignTexts[r.CampaignId.Value] : null,
                        r.CompanyName, r.FirstName, r.LastName, r.Email, r.Domain,
                        r.Date?.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) })),

                ("OpenCampaign",
                    new[] { "Sr No", "Domain", "Demanday User", "Time Stamp" },
                    openCampaign.Select(r => new object[] { r.SrNo, r.Domain,
                        r.DemandayUserId != null && userNames.ContainsKey(r.DemandayUserId.Value) ? userNames[r.DemandayUserId.Value] : null,
                        r.TimeStamp })),
            };

            using var zipStream = new System.IO.MemoryStream();
            using (var zip = new System.IO.Compression.ZipArchive(zipStream, System.IO.Compression.ZipArchiveMode.Create, true))
            {
                foreach (var m in modules)
                {
                    var fileBytes = BuildSingleSheetWorkbook(m.Name, m.Headers, m.Rows);
                    var entry = zip.CreateEntry(masterAccountId + "_" + campaignId + "_" + m.Name + ".xlsx",
                        System.IO.Compression.CompressionLevel.Fastest);
                    using var es = entry.Open();
                    es.Write(fileBytes, 0, fileBytes.Length);
                }
            }

            return File(zipStream.ToArray(), "application/zip",
                masterAccountId + "_" + campaignId + "_VerifySheets.zip");
        }

        // Builds a one-worksheet .xlsx (module export) and returns its bytes.
        private static byte[] BuildSingleSheetWorkbook(string sheetName, string[] headers, IEnumerable<object[]> rows)
        {
            using var package = new ExcelPackage();
            WriteSheet(package, sheetName, headers, rows);
            return package.GetAsByteArray();
        }

        // ---- Bulk import plumbing ----
        //
        // A suppression list can hold millions of rows, so the import must never do work per row on
        // the server. Instead the parsed rows are streamed into a session temp table in batches with
        // SqlBulkCopy, then merged into the target in two set-based statements keyed on Sr No.
        // Nothing about the existing table is loaded into memory.

        // 0 = no timeout: a multi-million row merge legitimately runs for minutes.
        private const int BulkCommandTimeout = 0;
        private const int BulkBatchSize = 50000;
        private const string StagingTable = "#VsImportStaging";

        // Serenity hands back a wrapped IDbConnection; SqlBulkCopy needs the real SqlConnection.
        private static SqlConnection UnwrapSqlConnection(IDbConnection connection)
        {
            var current = connection;
            for (int i = 0; i < 6 && current != null; i++)
            {
                if (current is SqlConnection sql)
                    return sql;
                var t = current.GetType();
                var prop = t.GetProperty("ActualConnection")
                        ?? t.GetProperty("InnerConnection")
                        ?? t.GetProperty("Connection");
                current = prop?.GetValue(current) as IDbConnection;
            }
            return null;
        }

        // Trims a cell to its destination column width — SqlBulkCopy has no per-row error recovery,
        // so an over-length value would abort the whole batch.
        private static object Clip(string value, int size)
        {
            if (value == null)
                return DBNull.Value;
            return value.Length > size ? value.Substring(0, size) : value;
        }

        private static SqlCommand NewCommand(SqlConnection sqlConn, string sql)
        {
            var cmd = sqlConn.CreateCommand();
            cmd.CommandTimeout = BulkCommandTimeout;
            cmd.CommandText = sql;
            return cmd;
        }

        // Maps the human-facing Campaign ID from a sheet (e.g. "79580") to the numeric key that
        // MasterSupression.CampaignId actually stores. Restricted to the selected Master Account so
        // a campaign belonging to someone else can never be attached by accident. The numeric key
        // is accepted too, but only where it does not shadow a real Campaign ID.
        private static Dictionary<string, int> LoadCampaignMap(SqlConnection sqlConn, int masterAccountId)
        {
            var rows = new List<(int Id, string Text)>();
            using (var cmd = NewCommand(sqlConn,
                "SELECT [Id], [CampaignId] FROM [dbo].[DemandayCampaignId] WHERE [DemandayMasterAccountId] = @acc"))
            {
                cmd.Parameters.AddWithValue("@acc", masterAccountId);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    rows.Add((reader.GetInt32(0), reader.IsDBNull(1) ? null : reader.GetString(1)));
            }

            var map = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (var r in rows)
            {
                if (string.IsNullOrWhiteSpace(r.Text))
                    continue;
                map[r.Text.Trim()] = r.Id;
                // Also index the cleaned form, so a stored "79,580" still matches a sheet's "79580".
                var cleaned = CleanCampaignText(r.Text);
                if (!string.IsNullOrEmpty(cleaned) && !map.ContainsKey(cleaned))
                    map[cleaned] = r.Id;
            }
            foreach (var r in rows)
            {
                var key = r.Id.ToString(CultureInfo.InvariantCulture);
                if (!map.ContainsKey(key))
                    map[key] = r.Id;
            }
            return map;
        }

        // DemandayCampaignId.CampaignId is nvarchar(15).
        private const int CampaignIdSize = 15;

        // Mirrors ExcelImportHelper's header normalisation so we can tell whether a column is
        // present at all (its own matcher is private).
        private static bool HasHeader(Dictionary<string, int> map, params string[] names)
        {
            foreach (var n in names)
            {
                var key = new string((n ?? "").Where(char.IsLetterOrDigit)
                    .Select(char.ToLowerInvariant).ToArray());
                if (key.Length > 0 && map.ContainsKey(key))
                    return true;
            }
            return false;
        }

        // Excel renders a numeric Campaign ID as "79,580" or "79580.00" depending on the cell
        // format, while the master list stores "79580" — reduce both to the same key.
        private static string CleanCampaignText(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return null;
            var s = raw.Trim();
            var compact = s.Replace(",", "").Replace(" ", "");
            if (decimal.TryParse(compact, NumberStyles.Any, CultureInfo.InvariantCulture, out var dec) &&
                dec == decimal.Truncate(dec))
                return decimal.Truncate(dec).ToString(CultureInfo.InvariantCulture);
            return s;
        }

        // Registers a Campaign ID that the sheet references but the Master Account does not have yet,
        // and returns its new key so the imported rows can point at it.
        private static int CreateCampaign(SqlConnection sqlConn, int masterAccountId, string campaignText)
        {
            using var cmd = NewCommand(sqlConn,
                "INSERT INTO [dbo].[DemandayCampaignId] ([CampaignId], [DemandayMasterAccountId]) " +
                "VALUES (@cid, @acc); SELECT CAST(SCOPE_IDENTITY() AS INT);");
            cmd.Parameters.AddWithValue("@cid", campaignText);
            cmd.Parameters.AddWithValue("@acc", masterAccountId);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        // Highest Sr No currently in the table. New rows are numbered above it, so a freshly
        // allocated Sr No can never collide with an existing row (no lookup table needed).
        private static int GetMaxSrNo(SqlConnection sqlConn, string tableName)
        {
            using var cmd = NewCommand(sqlConn, $"SELECT ISNULL(MAX([SrNo]), 0) FROM {tableName}");
            var v = cmd.ExecuteScalar();
            return (v == null || v == DBNull.Value) ? 0 : Convert.ToInt32(v);
        }

        // Empty staging table shaped exactly like the columns we import.
        private static void CreateStaging(SqlConnection sqlConn, string tableName, DataTable shape)
        {
            var colList = ColumnList(shape);
            using var cmd = NewCommand(sqlConn,
                $"IF OBJECT_ID('tempdb..{StagingTable}') IS NOT NULL DROP TABLE [{StagingTable}]; " +
                $"SELECT TOP 0 {colList} INTO [{StagingTable}] FROM {tableName};");
            cmd.ExecuteNonQuery();
        }

        private static string ColumnList(DataTable table)
        {
            return string.Join(", ", table.Columns.Cast<DataColumn>().Select(c => "[" + c.ColumnName + "]"));
        }

        // Streams one in-memory batch into staging, then clears it so memory stays bounded.
        private static void FlushBatch(SqlConnection sqlConn, DataTable batch)
        {
            if (batch.Rows.Count == 0)
                return;
            using (var bulk = new SqlBulkCopy(sqlConn))
            {
                bulk.DestinationTableName = StagingTable;
                bulk.BulkCopyTimeout = BulkCommandTimeout;
                bulk.BatchSize = BulkBatchSize;
                foreach (DataColumn c in batch.Columns)
                    bulk.ColumnMappings.Add(c.ColumnName, c.ColumnName);
                bulk.WriteToServer(batch);
            }
            batch.Clear();
        }

        // Merges staging into the target: update rows whose Sr No already exists, insert the rest.
        private static void MergeStaging(SqlConnection sqlConn, string tableName, DataTable shape,
            ref int imported, ref int updated)
        {
            var colList = ColumnList(shape);
            var names = shape.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();

            using (var cmd = NewCommand(sqlConn, $"CREATE INDEX IX_Staging_SrNo ON [{StagingTable}] ([SrNo]);"))
                cmd.ExecuteNonQuery();

            var setList = string.Join(", ", names
                .Where(c => !string.Equals(c, "SrNo", StringComparison.OrdinalIgnoreCase))
                .Select(c => $"t.[{c}] = s.[{c}]"));

            if (!string.IsNullOrEmpty(setList))
            {
                using var cmd = NewCommand(sqlConn,
                    $"UPDATE t SET {setList} FROM {tableName} t " +
                    $"INNER JOIN [{StagingTable}] s ON t.[SrNo] = s.[SrNo]; SELECT @@ROWCOUNT;");
                updated += Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
            }

            using (var cmd = NewCommand(sqlConn,
                $"INSERT INTO {tableName} ({colList}) SELECT {colList} FROM [{StagingTable}] s " +
                $"WHERE NOT EXISTS (SELECT 1 FROM {tableName} t WHERE t.[SrNo] = s.[SrNo]); SELECT @@ROWCOUNT;"))
                imported += Convert.ToInt32(cmd.ExecuteScalar() ?? 0);

            using (var cmd = NewCommand(sqlConn, $"DROP TABLE [{StagingTable}];"))
                cmd.ExecuteNonQuery();
        }

        // Sr No for a bulk row: keeps the file's value the first time it appears, otherwise (blank or
        // duplicated within the file) allocates the next number above the table's current maximum.
        private static int BulkSrNo(ExcelWorksheet ws, int row, Dictionary<string, int> map,
            ref int maxSrNo, HashSet<int> seen)
        {
            var parsed = ExcelImportHelper.GetInt(ws, row, map, "SrNo", "Sr No");
            int srNo;
            if (parsed.HasValue && !seen.Contains(parsed.Value))
            {
                srNo = parsed.Value;
                if (srNo > maxSrNo)
                    maxSrNo = srNo;
            }
            else
            {
                do { srNo = ++maxSrNo; } while (seen.Contains(srNo));
            }
            seen.Add(srNo);
            return srNo;
        }

        // Uploads a sheet into the chosen Tool Kit sub-module. Most sheets are tagged with the
        // selected Campaign; Master Suppression is account-wise, so it takes a Master Account
        // instead and leaves CampaignId null. Rows are upserted by Sr No.
        [HttpPost, IgnoreAntiforgeryToken]
        [Route("Toolkit/VerifySheets/ImportExcel")]
        [RequestSizeLimit(524288000)]
        [RequestFormLimits(MultipartBodyLengthLimit = 524288000)]
        public IActionResult ImportExcel([FromServices] ISqlConnections connections,
            [FromServices] ITwoLevelCache cache, IFormFile file,
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

            int imported = 0, updated = 0, skipped = 0, campaignsCreated = 0, campaignsMatched = 0;
            bool campaignHeaderFound = false, dateHeaderFound = false;
            try
            {
                using var connection = connections.NewByKey("Default");
                var sqlConn = UnwrapSqlConnection(connection);
                if (sqlConn == null)
                    return Content("Import failed: could not obtain a SQL Server connection.", "text/plain");
                if (sqlConn.State != ConnectionState.Open)
                    sqlConn.Open();

                using var package = new ExcelPackage(file.OpenReadStream());
                var ws = package.Workbook.Worksheets.Count > 0 ? package.Workbook.Worksheets[0] : null;
                if (ws?.Dimension == null)
                    return Content("The uploaded file has no data.", "text/plain");
                int rowCount = ws.Dimension.End.Row;
                var map = ExcelImportHelper.BuildHeaderMap(ws);
                var seen = new HashSet<int>();
                var batch = new DataTable();

                if (sheet == "Specification")
                {
                    const string table = "[dbo].[DemandaySpecs]";
                    batch.Columns.Add("SrNo", typeof(int));
                    batch.Columns.Add("CampaignId", typeof(int));
                    batch.Columns.Add("OrderId", typeof(long));
                    batch.Columns.Add("JobTitle", typeof(string));
                    batch.Columns.Add("JobLevel", typeof(string));
                    batch.Columns.Add("JobFunction", typeof(string));
                    batch.Columns.Add("Industry", typeof(string));
                    batch.Columns.Add("CompanyEmployeeSize", typeof(string));
                    batch.Columns.Add("AnnualRevenue", typeof(string));
                    batch.Columns.Add("ExcludeCompany", typeof(string));
                    batch.Columns.Add("Address", typeof(string));
                    batch.Columns.Add("City", typeof(string));
                    batch.Columns.Add("State", typeof(string));
                    batch.Columns.Add("ZipCode", typeof(string));
                    batch.Columns.Add("Country", typeof(string));
                    batch.Columns.Add("Comments", typeof(string));
                    batch.Columns.Add("AdditionalNotes", typeof(string));

                    int maxSrNo = GetMaxSrNo(sqlConn, table);
                    CreateStaging(sqlConn, table, batch);
                    for (int row = 2; row <= rowCount; row++)
                    {
                        // A row carrying a Sr No is always imported, even when every other column is
                        // blank; only rows with neither a Sr No nor any data are treated as empty.
                        var hasSrNo = ExcelImportHelper.GetInt(ws, row, map, "SrNo", "Sr No") != null;
                        var orderId = ParseLong(ExcelImportHelper.GetText(ws, row, map, "OrderId", "Order ID"));
                        var jobTitle = ExcelImportHelper.GetText(ws, row, map, "JobTitle", "Job Title");
                        if (!hasSrNo && orderId == null && string.IsNullOrWhiteSpace(jobTitle)) { skipped++; continue; }
                        batch.Rows.Add(
                            BulkSrNo(ws, row, map, ref maxSrNo, seen),
                            campaignId,
                            (object)orderId ?? DBNull.Value,
                            Clip(jobTitle, 500),
                            Clip(ExcelImportHelper.GetText(ws, row, map, "JobLevel", "Job Level"), 500),
                            Clip(ExcelImportHelper.GetText(ws, row, map, "JobFunction", "Job Function"), 200),
                            Clip(ExcelImportHelper.GetText(ws, row, map, "Industry"), 500),
                            Clip(ExcelImportHelper.GetText(ws, row, map, "CompanyEmployeeSize", "Company Employee Size"), 200),
                            Clip(ExcelImportHelper.GetText(ws, row, map, "AnnualRevenue", "Annual Revenue"), 200),
                            Clip(ExcelImportHelper.GetText(ws, row, map, "ExcludeCompany", "Exclude Company"), 4000),
                            Clip(ExcelImportHelper.GetText(ws, row, map, "Address"), 200),
                            Clip(ExcelImportHelper.GetText(ws, row, map, "City"), 100),
                            Clip(ExcelImportHelper.GetText(ws, row, map, "State"), 100),
                            Clip(ExcelImportHelper.GetText(ws, row, map, "ZipCode", "Zip Code"), 20),
                            Clip(ExcelImportHelper.GetText(ws, row, map, "Country"), 100),
                            Clip(ExcelImportHelper.GetText(ws, row, map, "Comments"), 10000),
                            Clip(ExcelImportHelper.GetText(ws, row, map, "AdditionalNotes", "Additional Notes"), 10000));
                        if (batch.Rows.Count >= BulkBatchSize) FlushBatch(sqlConn, batch);
                    }
                    FlushBatch(sqlConn, batch);
                    MergeStaging(sqlConn, table, batch, ref imported, ref updated);
                }
                else if (sheet == "EmailSuppression")
                {
                    const string table = "[dbo].[ClientSupression]";
                    batch.Columns.Add("SrNo", typeof(int));
                    batch.Columns.Add("CampaignId", typeof(int));
                    batch.Columns.Add("CompanyName", typeof(string));
                    batch.Columns.Add("FirstName", typeof(string));
                    batch.Columns.Add("LastName", typeof(string));
                    batch.Columns.Add("Email", typeof(string));
                    batch.Columns.Add("Domain", typeof(string));

                    int maxSrNo = GetMaxSrNo(sqlConn, table);
                    CreateStaging(sqlConn, table, batch);
                    for (int row = 2; row <= rowCount; row++)
                    {
                        // A row carrying a Sr No is always imported, even when every other column is blank.
                        var hasSrNo = ExcelImportHelper.GetInt(ws, row, map, "SrNo", "Sr No") != null;
                        var email = ExcelImportHelper.GetText(ws, row, map, "Email");
                        var company = ExcelImportHelper.GetText(ws, row, map, "CompanyName", "Company Name");
                        var domain = ExcelImportHelper.GetText(ws, row, map, "Domain");
                        if (!hasSrNo && string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(company) && string.IsNullOrWhiteSpace(domain)) { skipped++; continue; }
                        batch.Rows.Add(
                            BulkSrNo(ws, row, map, ref maxSrNo, seen),
                            campaignId,
                            Clip(company, 200),
                            Clip(ExcelImportHelper.GetText(ws, row, map, "FirstName", "First Name"), 100),
                            Clip(ExcelImportHelper.GetText(ws, row, map, "LastName", "Last Name"), 100),
                            Clip(email, 200),
                            Clip(domain, 50));
                        if (batch.Rows.Count >= BulkBatchSize) FlushBatch(sqlConn, batch);
                    }
                    FlushBatch(sqlConn, batch);
                    MergeStaging(sqlConn, table, batch, ref imported, ref updated);
                }
                else if (sheet == "CompetitorList")
                {
                    const string table = "[dbo].[DemandayCompetitor]";
                    batch.Columns.Add("SrNo", typeof(int));
                    batch.Columns.Add("CampaignId", typeof(int));
                    batch.Columns.Add("CompanyName", typeof(string));
                    batch.Columns.Add("Domain", typeof(string));
                    batch.Columns.Add("Email", typeof(string));
                    batch.Columns.Add("Cpc", typeof(string));

                    int maxSrNo = GetMaxSrNo(sqlConn, table);
                    CreateStaging(sqlConn, table, batch);
                    for (int row = 2; row <= rowCount; row++)
                    {
                        // A row carrying a Sr No is always imported, even when every other column is blank.
                        var hasSrNo = ExcelImportHelper.GetInt(ws, row, map, "SrNo", "Sr No") != null;
                        var domain = ExcelImportHelper.GetText(ws, row, map, "Domain");
                        var company = ExcelImportHelper.GetText(ws, row, map, "CompanyName", "Company Name");
                        if (!hasSrNo && string.IsNullOrWhiteSpace(domain) && string.IsNullOrWhiteSpace(company)) { skipped++; continue; }
                        batch.Rows.Add(
                            BulkSrNo(ws, row, map, ref maxSrNo, seen),
                            campaignId,
                            Clip(company, 200),
                            Clip(domain, 50),
                            Clip(ExcelImportHelper.GetText(ws, row, map, "Email"), 200),
                            // CPC is free text ("CPC 1225358", "02 cpc", "$0.75"), stored verbatim.
                            Clip(ExcelImportHelper.GetText(ws, row, map, "Cpc", "CPC")?.Trim(), 200));
                        if (batch.Rows.Count >= BulkBatchSize) FlushBatch(sqlConn, batch);
                    }
                    FlushBatch(sqlConn, batch);
                    MergeStaging(sqlConn, table, batch, ref imported, ref updated);
                }
                else if (sheet == "TALList")
                {
                    const string table = "[dbo].[TalCampaign]";
                    batch.Columns.Add("SrNo", typeof(int));
                    batch.Columns.Add("CampaignId", typeof(int));
                    batch.Columns.Add("CompanyName", typeof(string));
                    batch.Columns.Add("Domain", typeof(string));
                    batch.Columns.Add("AgentsName", typeof(int));
                    batch.Columns.Add("Reason", typeof(string));
                    batch.Columns.Add("CPC", typeof(string));

                    int maxSrNo = GetMaxSrNo(sqlConn, table);
                    CreateStaging(sqlConn, table, batch);
                    for (int row = 2; row <= rowCount; row++)
                    {
                        // A row carrying a Sr No is always imported, even when every other column is blank.
                        var hasSrNo = ExcelImportHelper.GetInt(ws, row, map, "SrNo", "Sr No") != null;
                        var domain = ExcelImportHelper.GetText(ws, row, map, "Domain");
                        var company = ExcelImportHelper.GetText(ws, row, map, "CompanyName", "Company Name");
                        if (!hasSrNo && string.IsNullOrWhiteSpace(domain) && string.IsNullOrWhiteSpace(company)) { skipped++; continue; }
                        // Agent cell may hold a user id or a typed name (Username / Display Name).
                        var agent = ExcelImportHelper.GetUserId(ws, row, map, connection, "Agent", "AgentsName", "Agent Name");
                        batch.Rows.Add(
                            BulkSrNo(ws, row, map, ref maxSrNo, seen),
                            campaignId,
                            Clip(company, 200),
                            Clip(domain, 100),
                            (object)agent ?? DBNull.Value,
                            Clip(ExcelImportHelper.GetText(ws, row, map, "Reason"), 100),
                            // CPC is free text ("CPC 1225358", "02 cpc", "$0.75"), stored verbatim.
                            Clip(ExcelImportHelper.GetText(ws, row, map, "Cpc", "CPC")?.Trim(), 200));
                        if (batch.Rows.Count >= BulkBatchSize) FlushBatch(sqlConn, batch);
                    }
                    FlushBatch(sqlConn, batch);
                    MergeStaging(sqlConn, table, batch, ref imported, ref updated);
                }
                else if (sheet == "MasterSuppression")
                {
                    // Account-wise: no campaign is involved. This is the sheet that reaches millions
                    // of rows, so it goes through the same bulk path as the rest.
                    const string table = "[dbo].[MasterSupression]";
                    batch.Columns.Add("SrNo", typeof(int));
                    batch.Columns.Add("MasterAccountId", typeof(int));
                    batch.Columns.Add("CampaignId", typeof(int));
                    batch.Columns.Add("CompanyName", typeof(string));
                    batch.Columns.Add("FirstName", typeof(string));
                    batch.Columns.Add("LastName", typeof(string));
                    batch.Columns.Add("Email", typeof(string));
                    batch.Columns.Add("Domain", typeof(string));
                    batch.Columns.Add("Date", typeof(DateTime));

                    // The sheet carries a Campaign ID per row (this sheet is account-wise, so it is
                    // not fixed by the toolbar). Resolve it once up front; it drives the section's
                    // Campaign filter, and the Date column drives the Date filter.
                    var campaignMap = LoadCampaignMap(sqlConn, masterAccountId);
                    campaignHeaderFound = HasHeader(map, "CampaignId", "Campaign ID", "Campaign", "CampaignID", "Camp ID");
                    dateHeaderFound = HasHeader(map, "Date");

                    int maxSrNo = GetMaxSrNo(sqlConn, table);
                    CreateStaging(sqlConn, table, batch);
                    for (int row = 2; row <= rowCount; row++)
                    {
                        // A row carrying a Sr No is always imported, even when every other column is blank.
                        var hasSrNo = ExcelImportHelper.GetInt(ws, row, map, "SrNo", "Sr No") != null;
                        var email = ExcelImportHelper.GetText(ws, row, map, "Email");
                        var company = ExcelImportHelper.GetText(ws, row, map, "CompanyName", "Company Name");
                        var domain = ExcelImportHelper.GetText(ws, row, map, "Domain");
                        if (!hasSrNo && string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(company) && string.IsNullOrWhiteSpace(domain)) { skipped++; continue; }

                        var campText = CleanCampaignText(ExcelImportHelper.GetText(
                            ws, row, map, "CampaignId", "Campaign ID", "Campaign", "CampaignID", "Camp ID"));
                        object campVal = DBNull.Value;
                        if (!string.IsNullOrEmpty(campText))
                        {
                            // Clip before the lookup so the same sheet value always maps to one key.
                            if (campText.Length > CampaignIdSize)
                                campText = campText.Substring(0, CampaignIdSize);
                            if (!campaignMap.TryGetValue(campText, out var cid))
                            {
                                // Campaign ID is new for this Master Account — create it, then use it.
                                cid = CreateCampaign(sqlConn, masterAccountId, campText);
                                campaignMap[campText] = cid;
                                campaignsCreated++;
                            }
                            campVal = cid;
                            campaignsMatched++;
                        }

                        var date = ExcelImportHelper.GetDate(ws, row, map, "Date");

                        batch.Rows.Add(
                            BulkSrNo(ws, row, map, ref maxSrNo, seen),
                            masterAccountId,
                            campVal,
                            Clip(company, 200),
                            Clip(ExcelImportHelper.GetText(ws, row, map, "FirstName", "First Name"), 100),
                            Clip(ExcelImportHelper.GetText(ws, row, map, "LastName", "Last Name"), 100),
                            Clip(email, 200),
                            Clip(domain, 50),
                            (object)date ?? DBNull.Value);
                        if (batch.Rows.Count >= BulkBatchSize) FlushBatch(sqlConn, batch);
                    }
                    FlushBatch(sqlConn, batch);
                    MergeStaging(sqlConn, table, batch, ref imported, ref updated);
                }
                else if (sheet == "OpenCampaign")
                {
                    const string table = "[dbo].[OpenCampaign]";
                    batch.Columns.Add("SrNo", typeof(int));
                    batch.Columns.Add("CampaignId", typeof(int));
                    batch.Columns.Add("Domain", typeof(string));

                    int maxSrNo = GetMaxSrNo(sqlConn, table);
                    CreateStaging(sqlConn, table, batch);
                    for (int row = 2; row <= rowCount; row++)
                    {
                        // A row carrying a Sr No is always imported, even when the domain is blank.
                        var hasSrNo = ExcelImportHelper.GetInt(ws, row, map, "SrNo", "Sr No") != null;
                        var domain = ExcelImportHelper.GetText(ws, row, map, "Domain");
                        if (!hasSrNo && string.IsNullOrWhiteSpace(domain)) { skipped++; continue; }
                        batch.Rows.Add(
                            BulkSrNo(ws, row, map, ref maxSrNo, seen),
                            campaignId,
                            Clip(domain, 100));
                        if (batch.Rows.Count >= BulkBatchSize) FlushBatch(sqlConn, batch);
                    }
                    FlushBatch(sqlConn, batch);
                    MergeStaging(sqlConn, table, batch, ref imported, ref updated);
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

            // Campaigns were inserted with raw SQL, which bypasses Serenity's save pipeline — expire
            // the row's cache group so the "Masters.DemandayCampaignId" lookup (and therefore the
            // Campaign dropdowns) picks the new entries up straight away.
            if (campaignsCreated > 0)
                cache?.ExpireGroupItems(DemandayCampaignIdRow.Fields.GenerationKey);

            var summary = $"Imported {imported}, updated {updated}, skipped {skipped} empty row(s) into '{sheet}'.";
            if (sheet == "MasterSuppression")
            {
                // Be explicit about the Campaign ID / Date columns — a silently blank column is the
                // hardest thing to diagnose from the grid alone.
                summary += campaignHeaderFound
                    ? $" Campaign ID set on {campaignsMatched} row(s)."
                    : " NOTE: no 'Campaign ID' column was found in the file, so that column stays empty.";
                if (campaignsCreated > 0)
                    summary += $" Created {campaignsCreated} new Campaign ID(s) under this Master Account.";
                if (!dateHeaderFound)
                    summary += " NOTE: no 'Date' column was found in the file.";
            }
            else if (campaignsCreated > 0)
                summary += $" Created {campaignsCreated} new Campaign ID(s) under this Master Account.";
            return Content(summary, "text/plain");
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
