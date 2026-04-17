using OfficeOpenXml;
using System;
using System.IO;

class CreateClientSupressionTemplate
{
    static void Main()
    {
        string basePath = @"C:\Users\dudhe\source\repos\Demanday-Latest\AdvanceCRM\AdvanceCRM.Web\Templates";
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        
        // ClientSupression Template
        using (var pkg = new ExcelPackage())
        {
            var ws = pkg.Workbook.Worksheets.Add("ClientSupression");
            ws.Cells[1, 1].Value = "CompanyName";
            ws.Cells[1, 2].Value = "FirstName";
            ws.Cells[1, 3].Value = "LastName";
            ws.Cells[1, 4].Value = "Email";
            ws.Cells[1, 5].Value = "Domain";
            ws.Cells[1, 6].Value = "Date";
            ws.Cells[1, 1, 1, 6].Style.Font.Bold = true;
            ws.Cells.AutoFitColumns();
            pkg.SaveAs(new FileInfo(Path.Combine(basePath, "ClientSupression_Template.xlsx")));
        }
        
        Console.WriteLine("ClientSupression Template created successfully!");
    }
}
