using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using Serenity;
using Serenity.Abstractions;
using Serenity.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace AdvanceCRM.Masters
{
    // Shared helper for the master (dropdown value) Excel imports. Masters are single-column
    // name lists, so we only need to pull one column of values out of the sheet.
    public static class MasterExcelImportHelper
    {
        private static readonly string[] KnownHeaders =
        {
            "Name", "Value",
            "Country",
            "Job Level", "JobLevel",
            "Job Function", "JobFunctionRole", "JobFunction",
            "Sub Industry", "SubIndustry",
            "Employee Size", "CompanyEmployeeSize", "EmployeeSize"
        };

        // Reads a single column of values from the first worksheet. If row 1 is a recognized header
        // it is skipped (data from row 2); otherwise the first column is read from row 1. Blanks skipped.
        public static List<string> ReadColumnValues(IFormFile file)
        {
            var result = new List<string>();
            using (var package = new ExcelPackage(file.OpenReadStream()))
            {
                var ws = package.Workbook.Worksheets.Count > 0 ? package.Workbook.Worksheets[0] : null;
                if (ws?.Dimension == null)
                    return result;

                int rowCount = ws.Dimension.End.Row;
                int colCount = ws.Dimension.End.Column;

                int nameCol = 0;
                for (int c = 1; c <= colCount && nameCol == 0; c++)
                {
                    var h = (ws.Cells[1, c].Text ?? "").Trim();
                    foreach (var kh in KnownHeaders)
                    {
                        if (string.Equals(h, kh, StringComparison.OrdinalIgnoreCase)) { nameCol = c; break; }
                    }
                }

                int startRow = nameCol > 0 ? 2 : 1;
                if (nameCol == 0) nameCol = 1;

                for (int r = startRow; r <= rowCount; r++)
                {
                    var val = (ws.Cells[r, nameCol].Text ?? "").Trim();
                    if (val.Length > 0)
                        result.Add(val);
                }
            }
            return result;
        }

        // Inserts any of the given values that don't already exist (case-insensitive) into the
        // master table. Used by module imports (DemandayContacts etc.) to auto-grow the masters
        // that back the filter dropdowns. masterTable is a fixed constant (never user input).
        // Returns how many new values were inserted.
        public static int SyncMaster(IUnitOfWork uow, ITwoLevelCache cache, RowFieldsBase fields,
            string masterTable, IEnumerable<string> values)
        {
            if (values == null) return 0;
            var connection = uow.Connection;

            var existing = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var n in connection.Query<string>("SELECT Name FROM dbo.[" + masterTable + "]"))
                if (!string.IsNullOrWhiteSpace(n)) existing.Add(n.Trim());

            int added = 0;
            foreach (var v in values)
            {
                var val = (v ?? "").Trim();
                if (val.Length == 0 || existing.Contains(val)) continue;
                connection.Execute("INSERT INTO dbo.[" + masterTable + "] (Name) VALUES (@n)", new { n = val });
                existing.Add(val);
                added++;
            }

            // Invalidate the lookup cache so the master's filter dropdown picks up the new values.
            if (added > 0 && cache != null && fields != null)
                cache.ExpireGroupItems(fields.GenerationKey);

            return added;
        }
    }
}
