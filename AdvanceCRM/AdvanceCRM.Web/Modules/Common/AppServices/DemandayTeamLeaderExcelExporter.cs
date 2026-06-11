using AdvanceCRM.Demanday;
using OfficeOpenXml;
using System.Collections.Generic;

namespace AdvanceCRM.Web.Modules.Common.AppServices
{
    public class DemandayTeamLeaderExcelExporter
    {
        public static byte[] ExportToExcel(List<DemandayTeamLeaderRow> demandayteamLeaderRows)
        {
            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("TeamLeader");
            // Headers
            string[] headers = new[]
            {
                "CAMPAIGN ID", "SLOT", "Id", "Company Name", "FIRSTNAME", "LASTNAME", "TITLE", "DATE", "Email", "WORKPHONE", "ALTERNATIVENUMBER", "STREET", "CITY", "STATE", "ZIP CODE", "COUNTRY", "INDUSTRY", "SUB INDUSTRY", "ZOOMINFO INDUSTRY", "ZOOMINFO EMPLOYEE SIZE", "REVENUE", "COMPANY EMPLOYEE SIZE", "PROFILE LINK", "Company link", "REVENUE LINK", "Adress link", "EMAIL FORMAT", "TENURITY", "CODE", "MD5", "Attachments", "CREATED BY"
            };
            for (int i = 0; i < headers.Length; i++)
                ws.Cells[1, i + 1].Value = headers[i];
            int row = 2;
            foreach (var en in demandayteamLeaderRows)
            {
                int col = 1;
                ws.Cells[row, col++].Value = en.CampaignId;
                ws.Cells[row, col++].Value = en.Slot;
                ws.Cells[row, col++].Value = en.Id;
                ws.Cells[row, col++].Value = en.CompanyName;
                ws.Cells[row, col++].Value = en.FirstName;
                ws.Cells[row, col++].Value = en.LastName;
                ws.Cells[row, col++].Value = en.Title;
                ws.Cells[row, col++].Value = en.Date?.ToString("MM-dd-yyyy");
                ws.Cells[row, col++].Value = en.Email;
                ws.Cells[row, col++].Value = en.WorkPhone;
                ws.Cells[row, col++].Value = en.AlternativeNumber;
                ws.Cells[row, col++].Value = en.Street;
                ws.Cells[row, col++].Value = en.City;
                ws.Cells[row, col++].Value = en.State;
                ws.Cells[row, col++].Value = en.ZipCode;
                ws.Cells[row, col++].Value = en.Country;
                ws.Cells[row, col++].Value = en.Industry;
                ws.Cells[row, col++].Value = en.SubIndustry;
                ws.Cells[row, col++].Value = en.ZoomInfoIndustry;
                ws.Cells[row, col++].Value = en.ZoomInfoEmployeeSize;
                ws.Cells[row, col++].Value = en.Revenue;
                ws.Cells[row, col++].Value = en.CompanyEmployeeSize;
                ws.Cells[row, col++].Value = en.ProfileLink;
                ws.Cells[row, col++].Value = en.CompanyLink;
                ws.Cells[row, col++].Value = en.RevenueLink;
                ws.Cells[row, col++].Value = en.AddressLink;
                ws.Cells[row, col++].Value = en.EmailFormat;
                ws.Cells[row, col++].Value = en.Tenurity;
                ws.Cells[row, col++].Value = en.Code;
                ws.Cells[row, col++].Value = en.Md5;
                ws.Cells[row, col++].Value = en.Attachments;
                ws.Cells[row, col++].Value = en.OwnerUsername;
                row++;
            }
            ws.Cells.AutoFitColumns();
            return package.GetAsByteArray();
        }
    }
}
