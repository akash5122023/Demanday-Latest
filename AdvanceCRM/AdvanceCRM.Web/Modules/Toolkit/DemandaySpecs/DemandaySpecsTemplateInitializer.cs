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
                // Check if template file exists and is valid (should be > 100 bytes)
                if (File.Exists(templatePath) && new FileInfo(templatePath).Length > 100)
                {
                    return; // File exists and appears valid
                }

                // Create or recreate the template
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("DemandaySpecs");

                    // Add headers - all ExcelImportable fields from DemandaySpecsRow
                    var headers = new string[]
                    {
                        "Order ID",                    // 1
                        "Job Title",                   // 2
                        "Job Level",                   // 3
                        "Job Function",                // 4
                        "Industry",                    // 5
                        "Company Employee Size",       // 6
                        "Annual Revenue",              // 7
                        "Address",                     // 8
                        "City",                        // 9
                        "State",                       // 10
                        "Zip Code",                    // 11
                        "Country",                     // 12
                        "Comments",                    // 13
                        "Additional Notes"             // 14
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
    }
}
