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

                // Regenerate unless the file already has the current layout (Sr No first AND the
                // Exclude Company column present); an older template is missing that column.
                if (File.Exists(templatePath) && new FileInfo(templatePath).Length > 100 &&
                    TemplateIsCurrent(templatePath))
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
                        "Exclude Company",             // 9
                        "Address",                     // 10
                        "City",                        // 11
                        "State",                       // 12
                        "Zip Code",                    // 13
                        "Country",                     // 14
                        "Comments",                    // 15
                        "Additional Notes"             // 16
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
                    worksheet.Column(9).Width = 22;
                    worksheet.Column(10).Width = 12;
                    worksheet.Column(11).Width = 12;
                    worksheet.Column(12).Width = 10;
                    worksheet.Column(13).Width = 12;
                    worksheet.Column(14).Width = 20;
                    worksheet.Column(15).Width = 20;
                    worksheet.Column(16).Width = 20;

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

        // True when the template already matches the current layout: cell A1 reads "Sr No" and
        // the "Exclude Company" column (I1) is present. An older template fails the second check
        // and gets regenerated with the new column.
        private static bool TemplateIsCurrent(string templatePath)
        {
            try
            {
                using (var package = new ExcelPackage(new FileInfo(templatePath)))
                {
                    var ws = package.Workbook.Worksheets.Count > 0 ? package.Workbook.Worksheets[0] : null;
                    var first = ws?.Cells[1, 1].Value?.ToString()?.Trim();
                    var excludeCompany = ws?.Cells[1, 9].Value?.ToString()?.Trim();
                    return string.Equals(first, "Sr No", StringComparison.OrdinalIgnoreCase) &&
                           string.Equals(excludeCompany, "Exclude Company", StringComparison.OrdinalIgnoreCase);
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
