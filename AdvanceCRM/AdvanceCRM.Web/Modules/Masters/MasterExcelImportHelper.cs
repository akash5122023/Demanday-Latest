using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;

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
    }
}
