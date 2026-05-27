using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AdvanceCRM.Web.Modules.Common.AppServices
{
    public static class ExcelImportHelper
    {
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
