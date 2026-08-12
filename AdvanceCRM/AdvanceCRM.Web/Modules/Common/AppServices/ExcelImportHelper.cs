using AdvanceCRM.Administration;
using OfficeOpenXml;
using Serenity.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;

namespace AdvanceCRM.Web.Modules.Common.AppServices
{
    public static class ExcelImportHelper
    {
        // Defensive backstop for Excel imports: clamp every string field to its
        // declared column size so an over-length cell (e.g. a long URL pasted into
        // a short column) is trimmed to fit instead of failing the whole row with
        // "String or binary data would be truncated".
        public static void ClampStringFields(IRow row)
        {
            if (row == null)
                return;
            foreach (var field in row.GetFields())
            {
                if (field is StringField sf && sf.Size > 0)
                {
                    var val = sf[row];
                    if (val != null && val.Length > sf.Size)
                        sf[row] = val.Substring(0, sf.Size);
                }
            }
        }

        public static Dictionary<string, int> BuildHeaderMap(ExcelWorksheet ws)
        {
            var map = new Dictionary<string, int>();
            if (ws?.Dimension == null)
                return map;
            int colCount = ws.Dimension.End.Column;
            for (int c = 1; c <= colCount; c++)
            {
                var raw = ws.Cells[1, c].Text;
                var key = Normalize(raw);
                if (!string.IsNullOrEmpty(key) && !map.ContainsKey(key))
                    map[key] = c;
            }
            return map;
        }

        private static string Normalize(string header)
        {
            if (string.IsNullOrEmpty(header))
                return null;
            var sb = new StringBuilder(header.Length);
            foreach (var ch in header)
            {
                if (char.IsLetterOrDigit(ch))
                    sb.Append(char.ToLowerInvariant(ch));
            }
            return sb.ToString();
        }

        private static bool TryGetCol(Dictionary<string, int> map, string[] names, out int col)
        {
            col = 0;
            if (map == null || names == null) return false;
            foreach (var n in names)
            {
                var key = Normalize(n);
                if (!string.IsNullOrEmpty(key) && map.TryGetValue(key, out col))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// True when a row carries values only in the given "detail" columns. That is the shape
        /// an export uses for child rows - QA Details under a Quality record, sub contacts under
        /// a contact - where the record's own columns are left empty. Such a row describes the
        /// record above it, not a record of its own, so an import must skip it instead of
        /// creating an empty record out of it.
        /// </summary>
        public static bool IsDetailOnlyRow(ExcelWorksheet ws, int row, Dictionary<string, int> map,
            params string[] detailHeaders)
        {
            if (ws == null || map == null || detailHeaders == null || detailHeaders.Length == 0)
                return false;

            var detailCols = new HashSet<int>();
            foreach (var name in detailHeaders)
            {
                if (TryGetCol(map, new[] { name }, out int c))
                    detailCols.Add(c);
            }

            if (detailCols.Count == 0)
                return false;      // sheet has no detail columns at all - nothing to recognise

            bool hasDetail = false;
            foreach (var col in map.Values)
            {
                if (string.IsNullOrWhiteSpace(ws.Cells[row, col].Text))
                    continue;
                if (!detailCols.Contains(col))
                    return false;  // something outside the detail columns - this is a record row
                hasDetail = true;
            }

            return hasDetail;
        }

        public static string GetText(ExcelWorksheet ws, int row, Dictionary<string, int> map, params string[] names)
        {
            if (!TryGetCol(map, names, out int col)) return null;
            return ws.Cells[row, col].Text;
        }

        public static int? GetInt(ExcelWorksheet ws, int row, Dictionary<string, int> map, params string[] names)
        {
            if (!TryGetCol(map, names, out int col)) return null;
            var v = ws.Cells[row, col].Value;
            if (v == null) return null;
            var s = v.ToString();
            if (string.IsNullOrWhiteSpace(s)) return null;
            return int.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out int r) ? r : (int?)null;
        }

        // Resolves an owner column to a Users.UserId. The cell may contain either a
        // numeric user id (legacy/export round-trip) OR an owner name typed by the user.
        // When it's text, we look the user up by Username or Display Name (case-insensitive
        // per the DB collation) so the owner actually resolves and shows in the grid —
        // GetInt alone returns null for a name, leaving the owner blank after import.
        public static int? GetUserId(ExcelWorksheet ws, int row, Dictionary<string, int> map, IDbConnection connection, params string[] names)
        {
            if (!TryGetCol(map, names, out int col)) return null;
            var v = ws.Cells[row, col].Value;
            if (v == null) return null;
            var s = v.ToString()?.Trim();
            if (string.IsNullOrWhiteSpace(s)) return null;

            // A plain integer is taken as the UserId directly.
            if (int.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out int id))
                return id;

            if (connection == null)
                return null;

            // Otherwise resolve the typed name against Username or Display Name.
            var u = UserRow.Fields;
            var user = connection.TryFirst<UserRow>(q => q
                .Select(u.UserId)
                .Where((u.Username == s) | (u.DisplayName == s)));
            return user?.UserId;
        }

        /// <summary>
        /// Case-insensitive Account Number to DemandayMasterAccount.Id map. Read once per import
        /// so a file with thousands of rows does not issue one lookup query per row.
        /// </summary>
        public static Dictionary<string, int> LoadMasterAccountMap(IDbConnection connection)
        {
            var accounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            if (connection == null)
                return accounts;

            var a = Masters.DemandayMasterAccountRow.Fields;
            foreach (var acc in connection.List<Masters.DemandayMasterAccountRow>(q => q
                .Select(a.Id)
                .Select(a.AccountNumber)))
            {
                if (acc.Id.HasValue && !string.IsNullOrWhiteSpace(acc.AccountNumber))
                    accounts[acc.AccountNumber.Trim()] = acc.Id.Value;
            }
            return accounts;
        }

        /// <summary>
        /// Resolves a "Master Account No" column to a DemandayMasterAccount.Id by looking the cell
        /// up as an Account Number. Deliberately does NOT fall back to reading the cell as an id:
        /// account numbers can themselves be numeric, so a numeric fallback would happily link the
        /// wrong account. Callers keep a separate GetInt on the "Master Account Id" column for that.
        /// Returns null for a blank or unknown account, leaving the row's account unset.
        /// </summary>
        public static int? GetMasterAccountId(ExcelWorksheet ws, int row, Dictionary<string, int> map,
            Dictionary<string, int> accounts, params string[] names)
        {
            if (accounts == null || accounts.Count == 0) return null;
            if (!TryGetCol(map, names, out int col)) return null;
            var v = ws.Cells[row, col].Value;
            if (v == null) return null;
            var s = v.ToString()?.Trim();
            if (string.IsNullOrWhiteSpace(s)) return null;

            return accounts.TryGetValue(s, out int id) ? id : (int?)null;
        }

        /// <summary>
        /// Campaign code ("79580") -> DemandayCampaignId.Id, keyed by account. The same code may
        /// exist under more than one Master Account, so it only identifies a campaign together
        /// with its account - the key here is "accountId|code".
        /// </summary>
        public static Dictionary<string, int> LoadCampaignMap(IDbConnection connection)
        {
            var campaigns = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            if (connection == null)
                return campaigns;

            var c = Masters.DemandayCampaignIdRow.Fields;
            foreach (var cam in connection.List<Masters.DemandayCampaignIdRow>(q => q
                .Select(c.Id)
                .Select(c.CampaignId)
                .Select(c.DemandayMasterAccountId)))
            {
                if (!cam.Id.HasValue || !cam.DemandayMasterAccountId.HasValue ||
                    string.IsNullOrWhiteSpace(cam.CampaignId))
                    continue;

                var key = cam.DemandayMasterAccountId.Value + "|" + cam.CampaignId.Trim();
                if (!campaigns.ContainsKey(key))
                    campaigns[key] = cam.Id.Value;
            }
            return campaigns;
        }

        /// <summary>
        /// Resolves a "Campaign Id" column to a DemandayCampaignId.Id within the row's own Master
        /// Account. Returns null when the account is unknown or the code does not belong to it,
        /// which leaves the campaign unset rather than pointing at another account's campaign.
        /// </summary>
        public static int? GetCampaignId(ExcelWorksheet ws, int row, Dictionary<string, int> map,
            Dictionary<string, int> campaigns, int? masterAccountId, params string[] names)
        {
            if (campaigns == null || campaigns.Count == 0 || masterAccountId == null) return null;
            if (!TryGetCol(map, names, out int col)) return null;
            var s = ws.Cells[row, col].Text?.Trim();
            if (string.IsNullOrWhiteSpace(s)) return null;

            return campaigns.TryGetValue(masterAccountId.Value + "|" + s, out int id) ? id : (int?)null;
        }

        public static decimal? GetDecimal(ExcelWorksheet ws, int row, Dictionary<string, int> map, params string[] names)
        {
            if (!TryGetCol(map, names, out int col)) return null;
            var v = ws.Cells[row, col].Value;
            if (v == null) return null;
            var s = v.ToString();
            if (string.IsNullOrWhiteSpace(s)) return null;
            return decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal r) ? r : (decimal?)null;
        }

        public static DateTime? GetDate(ExcelWorksheet ws, int row, Dictionary<string, int> map, params string[] names)
        {
            if (!TryGetCol(map, names, out int col)) return null;
            var v = ws.Cells[row, col].Value;
            if (v == null) return null;
            if (v is DateTime dt) return dt;
            if (v is double d)
            {
                try { return DateTime.FromOADate(d); } catch { }
            }
            var s = v.ToString()?.Trim();
            if (string.IsNullOrWhiteSpace(s)) return null;

            string[] formats = new string[] { "MM-dd-yyyy", "MM/dd/yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "dd-MM-yyyy", "dd/MM/yyyy" };
            if (DateTime.TryParseExact(s, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime rex))
                return rex;

            return DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime r) ? r : (DateTime?)null;
        }
    }
}
