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

                    // Sr No is the upsert key on import; Master Account Id / Campaign Id are
                    // optional per-row overrides of the dialog's Campaign; the rest are the
                    // ExcelImportable fields.
                    var headers = new string[]
                    {
                        "Sr No",                       // 1
                        "Master Account ID",           // 2
                        "Campaign ID",                 // 3
                        "Order ID",                    // 4
                        "Job Title",                   // 5
                        "Job Level",                   // 6
                        "Job Function",                // 7
                        "Industry",                    // 8
                        "Company Employee Size",       // 9
                        "Annual Revenue",              // 10
                        "Exclude Company",             // 11
                        "Address",                     // 12
                        "City",                        // 13
                        "State",                       // 14
                        "Zip Code",                    // 15
                        "Country",                     // 16
                        "Comments",                    // 17
                        "Additional Notes"             // 18
                    };

                    for (int col = 1; col <= headers.Length; col++)
                    {
                        worksheet.Cells[1, col].Value = headers[col - 1];
                        worksheet.Cells[1, col].Style.Font.Bold = true;
                    }

                    // Set column widths for readability
                    worksheet.Column(1).Width = 12;
                    worksheet.Column(2).Width = 16;
                    worksheet.Column(3).Width = 14;
                    worksheet.Column(4).Width = 20;
                    worksheet.Column(5).Width = 12;
                    worksheet.Column(6).Width = 18;
                    worksheet.Column(7).Width = 15;
                    worksheet.Column(8).Width = 22;
                    worksheet.Column(9).Width = 15;
                    worksheet.Column(10).Width = 20;
                    worksheet.Column(11).Width = 22;
                    worksheet.Column(12).Width = 12;
                    worksheet.Column(13).Width = 12;
                    worksheet.Column(14).Width = 10;
                    worksheet.Column(15).Width = 12;
                    worksheet.Column(16).Width = 20;
                    worksheet.Column(17).Width = 20;
                    worksheet.Column(18).Width = 20;

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
        // the "Campaign ID" (C1) and "Exclude Company" (K1) columns are present. An older
        // template fails one of these checks and gets regenerated with the new columns.
        private static bool TemplateIsCurrent(string templatePath)
        {
            try
            {
                using (var package = new ExcelPackage(new FileInfo(templatePath)))
                {
                    var ws = package.Workbook.Worksheets.Count > 0 ? package.Workbook.Worksheets[0] : null;
                    var first = ws?.Cells[1, 1].Value?.ToString()?.Trim();
                    var campaignId = ws?.Cells[1, 3].Value?.ToString()?.Trim();
                    var excludeCompany = ws?.Cells[1, 11].Value?.ToString()?.Trim();
                    return string.Equals(first, "Sr No", StringComparison.OrdinalIgnoreCase) &&
                           string.Equals(campaignId, "Campaign ID", StringComparison.OrdinalIgnoreCase) &&
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
