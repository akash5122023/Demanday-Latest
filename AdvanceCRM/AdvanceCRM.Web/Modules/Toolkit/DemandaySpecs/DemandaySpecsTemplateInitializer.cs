using OfficeOpenXml;
using System;
using System.IO;

namespace AdvanceCRM.Toolkit
{
    /// <summary>
    /// Initializes the DemandaySpecs template Excel file
    /// This ensures the template file exists and has the proper format
    /// </summary>
    public static class DemandaySpecsTemplateInitializer
    {
        public static void EnsureTemplateExists(string templatePath)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // A pre-SrNo template has a stale header row, so regenerate unless "Sr No" is first.
                if (File.Exists(templatePath) && new FileInfo(templatePath).Length > 100 &&
                    FirstHeaderIsSrNo(templatePath))
                {
                    return;
                }

                if (File.Exists(templatePath))
                    File.Delete(templatePath);

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("DemandaySpecs");

                    // Sr No is the upsert key on import; the rest are the ExcelImportable fields.
                    var headers = new string[]
                    {
                        "Sr No",                       // 1
                        "Order ID",                    // 2
                        "Job Title",                   // 3
                        "Job Level",                   // 4
                        "Job Function",                // 5
                        "Industry",                    // 6
                        "Company Employee Size",       // 7
                        "Annual Revenue",              // 8
                        "Address",                     // 9
                        "City",                        // 10
                        "State",                       // 11
                        "Zip Code",                    // 12
                        "Country",                     // 13
                        "Comments",                    // 14
                        "Additional Notes"             // 15
                    };

                    for (int col = 1; col <= headers.Length; col++)
                    {
                        worksheet.Cells[1, col].Value = headers[col - 1];
                        worksheet.Cells[1, col].Style.Font.Bold = true;
                    }

                    // Set column widths for readability
                    worksheet.Column(1).Width = 12;
                    worksheet.Column(2).Width = 20;
                    worksheet.Column(3).Width = 12;
                    worksheet.Column(4).Width = 18;
                    worksheet.Column(5).Width = 15;
                    worksheet.Column(6).Width = 22;
                    worksheet.Column(7).Width = 15;
                    worksheet.Column(8).Width = 20;
                    worksheet.Column(9).Width = 12;
                    worksheet.Column(10).Width = 12;
                    worksheet.Column(11).Width = 10;
                    worksheet.Column(12).Width = 12;
                    worksheet.Column(13).Width = 20;
                    worksheet.Column(14).Width = 20;
                    worksheet.Column(15).Width = 20;

                    // Ensure directory exists
                    Directory.CreateDirectory(Path.GetDirectoryName(templatePath));

                    // Save the file
                    FileInfo file = new FileInfo(templatePath);
                    package.SaveAs(file);
                }
            }
            catch (Exception ex)
            {
                // Log error but don't throw - template can be manually created
                System.Diagnostics.Debug.WriteLine($"Error initializing DemandaySpecs template: {ex.Message}");
            }
        }

        // True when cell A1 already reads "Sr No" — i.e. the template is the current layout.
        private static bool FirstHeaderIsSrNo(string templatePath)
        {
            try
            {
                using (var package = new ExcelPackage(new FileInfo(templatePath)))
                {
                    var ws = package.Workbook.Worksheets.Count > 0 ? package.Workbook.Worksheets[0] : null;
                    var first = ws?.Cells[1, 1].Value?.ToString()?.Trim();
                    return string.Equals(first, "Sr No", StringComparison.OrdinalIgnoreCase);
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
