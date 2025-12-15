using AdvanceCRM.Demanday;
using OfficeOpenXml;
using System.Collections.Generic;

namespace AdvanceCRM.Web.Modules.Common.AppServices
{
    public class DemandayMisExcelExporter
    {
        public static byte[] ExportToExcel(List<DemandayMisRow> demandaymisRows)
        {
            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("MIS");
            // Headers
            string[] headers = new[]
            {
            "Id","SLOT", "Company Name", "FIRSTNAME", "LASTNAME", "TITLE", "Email", "WORKPHONE", "ALTERNATIVENUMBER", "STREET", "CITY", "STATE", "ZIP CODE", "COUNTRY", "INDUSTRY", "REVENUE","COMPANY EMPLOYEE SIZE", "PROFILE LINK", "Company link", "REVENUE LINK", "EMAIL FORMAT","Adress link","PRIMARY REASON","CATEGORY","COMMENTS","QA STATUS","DELIVERY STATUS","AGENT NAME","QA NAME","CALL DATE","DATE AUDITED","DELIVERY DATE","SOURCE","VERIFICATION MODE","ASSET 1","ASSET 2","TL NAME", "TENURITY", "CODE", "LINK", "MD5","CREATED BY"
        };
            for (int i = 0; i < headers.Length; i++)
                ws.Cells[1, i + 1].Value = headers[i];
            int row = 2;
            foreach (var en in demandaymisRows)
            {
                int col = 1;
                ws.Cells[row, col++].Value = en.Id;
                ws.Cells[row, col++].Value = en.Slot;
                ws.Cells[row, col++].Value = en.CompanyName;
                ws.Cells[row, col++].Value = en.FirstName;
                ws.Cells[row, col++].Value = en.LastName;
                ws.Cells[row, col++].Value = en.Title;
                ws.Cells[row, col++].Value = en.Email;
                ws.Cells[row, col++].Value = en.WorkPhone;
                ws.Cells[row, col++].Value = en.AlternativeNumber;
                ws.Cells[row, col++].Value = en.Street;
                ws.Cells[row, col++].Value = en.City;
                ws.Cells[row, col++].Value = en.State;
                ws.Cells[row, col++].Value = en.ZipCode;
                ws.Cells[row, col++].Value = en.Country;
                ws.Cells[row, col++].Value = en.CompanyEmployeeSize;
                ws.Cells[row, col++].Value = en.Industry;
                ws.Cells[row, col++].Value = en.Revenue;
                ws.Cells[row, col++].Value = en.ProfileLink;
                ws.Cells[row, col++].Value = en.CompanyLink;
                ws.Cells[row, col++].Value = en.RevenueLink;
                ws.Cells[row, col++].Value = en.EmailFormat;
                ws.Cells[row, col++].Value = en.AdressLink;
                ws.Cells[row, col++].Value = en.PrimaryReason;
                ws.Cells[row, col++].Value = en.Category;
                ws.Cells[row, col++].Value = en.Comments;
                ws.Cells[row, col++].Value = en.QaStatus;
                ws.Cells[row, col++].Value = en.DeliveryStatus;
                ws.Cells[row, col++].Value = en.AgentName;
                ws.Cells[row, col++].Value = en.QaName;
                ws.Cells[row, col++].Value = en.CallDate?.ToString("yyyy-MM-dd");
                ws.Cells[row, col++].Value = en.DateAudited?.ToString("yyyy-MM-dd");
                ws.Cells[row, col++].Value = en.DeliveryDate?.ToString("yyyy-MM-dd");
                ws.Cells[row, col++].Value = en.Source;
                ws.Cells[row, col++].Value = en.VerificationMode;
                ws.Cells[row, col++].Value = en.Asset1;
                ws.Cells[row, col++].Value = en.Asset2;
                ws.Cells[row, col++].Value = en.TlName;
                ws.Cells[row, col++].Value = en.Tenurity;
                ws.Cells[row, col++].Value = en.Code;
                ws.Cells[row, col++].Value = en.Link;
                ws.Cells[row, col++].Value = en.Md5;
                ws.Cells[row, col++].Value = en.OwnerId;
                row++;
            }
            ws.Cells.AutoFitColumns();
            return package.GetAsByteArray();
        }
    }
}
