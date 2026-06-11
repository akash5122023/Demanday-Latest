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
