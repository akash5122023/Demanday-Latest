using AdvanceCRM.Demanday;
using OfficeOpenXml;
using System.Collections.Generic;

namespace AdvanceCRM.Web.Modules.Common.AppServices
{
    public class DemandayTeleMarketingMISExcelExporter
    {
        public static byte[] ExportToExcel(List<DemandayTeleMarketingMISRow> demandaytelemarketingmisRows)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("MIS");
            // Headers
            string[] headers = new[]
            {
                "CAMPAIGN ID","Id","SLOT", "Company Name", "FIRSTNAME", "LASTNAME", "TITLE","Date","Additional Notes", "Email", "WORKPHONE", "ALTERNATIVENUMBER", "DOMAIN", "JOB LEVEL", "JOB FUNCTION ROLE", "STREET", "CITY", "STATE", "ZIP CODE", "COUNTRY","COMPANY EMPLOYEE SIZE", "INDUSTRY", "SUB INDUSTRY", "ZOOMINFO INDUSTRY", "ZOOMINFO EMPLOYEE SIZE", "REVENUE", "PROFILE LINK", "Company link", "REVENUE LINK", "EMAIL FORMAT","Adress link","PRIMARY REASON","CATEGORY","COMMENTS","QA STATUS","DELIVERY STATUS","AGENT NAME","QA NAME","CALL DATE","DATE AUDITED","DELIVERY DATE","SOURCE","VERIFICATION MODE","ASSET 1","ASSET 2","ASSET","CALL STATUS","TL NAME", "TENURITY", "CODE", "LINK", "MD5","CREATED BY"
            };
            for (int i = 0; i < headers.Length; i++)
                ws.Cells[1, i + 1].Value = headers[i];
            int row = 2;
            foreach (var en in demandaytelemarketingmisRows)
            {
                int col = 1;
                ws.Cells[row, col++].Value = en.CampaignId;
                ws.Cells[row, col++].Value = en.Id;
                ws.Cells[row, col++].Value = en.Slot;
                ws.Cells[row, col++].Value = en.CompanyName;
                ws.Cells[row, col++].Value = en.FirstName;
                ws.Cells[row, col++].Value = en.LastName;
                ws.Cells[row, col++].Value = en.Title;
                ws.Cells[row, col++].Value = en.Date?.ToString("MM-dd-yyyy");
                ws.Cells[row, col++].Value = en.AdditionalNotes;
                ws.Cells[row, col++].Value = en.Email;
                ws.Cells[row, col++].Value = en.WorkPhone;
                ws.Cells[row, col++].Value = en.AlternativeNumber;
                ws.Cells[row, col++].Value = en.Domain;
                ws.Cells[row, col++].Value = en.JobLevel;
                ws.Cells[row, col++].Value = en.JobFunctionRole;
                ws.Cells[row, col++].Value = en.Street;
                ws.Cells[row, col++].Value = en.City;
                ws.Cells[row, col++].Value = en.State;
                ws.Cells[row, col++].Value = en.ZipCode;
                ws.Cells[row, col++].Value = en.Country;
                ws.Cells[row, col++].Value = en.CompanyEmployeeSize;
                ws.Cells[row, col++].Value = en.Industry;
                ws.Cells[row, col++].Value = en.SubIndustry;
                ws.Cells[row, col++].Value = en.ZoomInfoIndustry;
                ws.Cells[row, col++].Value = en.ZoomInfoEmployeeSize;
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
                ws.Cells[row, col++].Value = en.CallDate?.ToString("MM-dd-yyyy");
                ws.Cells[row, col++].Value = en.DateAudited?.ToString("MM-dd-yyyy");
                ws.Cells[row, col++].Value = en.DeliveryDate?.ToString("MM-dd-yyyy");
                ws.Cells[row, col++].Value = en.Source;
                ws.Cells[row, col++].Value = en.VerificationMode;
                ws.Cells[row, col++].Value = en.Asset1;
                ws.Cells[row, col++].Value = en.Asset2;
                ws.Cells[row, col++].Value = en.Asset;
                ws.Cells[row, col++].Value = en.CallStatus;
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
