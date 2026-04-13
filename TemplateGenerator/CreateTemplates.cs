using OfficeOpenXml;
using System;
using System.IO;

class CreateTemplates
{
    static void Main()
    {
        string basePath = @"C:\Users\dudhe\source\repos\Demanday-Latest\AdvanceCRM\AdvanceCRM.Web\Templates";
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        // MasterSupression Template
        using (var pkg = new ExcelPackage())
        {
            var ws = pkg.Workbook.Worksheets.Add("MasterSupression");
            ws.Cells[1, 1].Value = "CampaignId";
            ws.Cells[1, 2].Value = "CompanyName";
            ws.Cells[1, 3].Value = "FirstName";
            ws.Cells[1, 4].Value = "LastName";
            ws.Cells[1, 5].Value = "Email";
            ws.Cells[1, 6].Value = "Domain";
            ws.Cells[1, 7].Value = "Date";
            ws.Cells[1, 1, 1, 7].Style.Font.Bold = true;
            ws.Cells.AutoFitColumns();
            pkg.SaveAs(new FileInfo(Path.Combine(basePath, "MasterSupression_Template.xlsx")));
        }

        // TalCampaign Template
        using (var pkg = new ExcelPackage())
        {
            var ws = pkg.Workbook.Worksheets.Add("TalCampaign");
            ws.Cells[1, 1].Value = "CompanyName";
            ws.Cells[1, 2].Value = "Domain";
            ws.Cells[1, 3].Value = "AgentsName";
            ws.Cells[1, 4].Value = "Reason";
            ws.Cells[1, 1, 1, 4].Style.Font.Bold = true;
            ws.Cells.AutoFitColumns();
            pkg.SaveAs(new FileInfo(Path.Combine(basePath, "TalCampaign_Template.xlsx")));
        }

        Console.WriteLine("Templates created successfully!");
    }
}
