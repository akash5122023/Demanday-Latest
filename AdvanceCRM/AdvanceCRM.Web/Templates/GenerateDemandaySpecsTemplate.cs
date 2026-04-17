// This is a template generation helper - can be used to regenerate DemandaySpecs_Template.xlsx
// Usage: Run this code to generate the template file

using OfficeOpenXml;
using System;
using System.IO;

namespace AdvanceCRM.Web.Templates
{
    public class GenerateDemandaySpecsTemplate
    {
        public static void Generate(string outputPath)
        {
            // Set EPPlus license context
            EPPlus.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("DemandaySpecs");

                // Add headers
                worksheet.Cells[1, 1].Value = "Order ID";
                worksheet.Cells[1, 2].Value = "Job Title";
                worksheet.Cells[1, 3].Value = "Job Level";
                worksheet.Cells[1, 4].Value = "Job Function";
                worksheet.Cells[1, 5].Value = "Industry";

                // Format header row
                for (int col = 1; col <= 5; col++)
                {
                    var cell = worksheet.Cells[1, col];
                    cell.Style.Font.Bold = true;
                    cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                }

                // Set column widths
                worksheet.Column(1).Width = 15;
                worksheet.Column(2).Width = 25;
                worksheet.Column(3).Width = 15;
                worksheet.Column(4).Width = 20;
                worksheet.Column(5).Width = 20;

                // Save the file
                FileInfo file = new FileInfo(outputPath);
                package.SaveAs(file);
            }
        }
    }
}
