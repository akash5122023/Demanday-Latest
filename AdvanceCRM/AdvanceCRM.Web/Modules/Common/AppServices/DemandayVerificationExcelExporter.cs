using AdvanceCRM.Demanday;
using OfficeOpenXml;
using System.Collections.Generic;

namespace AdvanceCRM.Web.Modules.Common.AppServices
{
    public class DemandayVerificationExcelExporter
    {
        public static byte[] ExportToExcel(List<DemandayVerificationRow> demandayverificationRows)
        {
            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("DemandayVerification");
            // Headers
            string[] headers = new[]
            {
            "Id","SrNo","AgentName","CdqaComments", "Company Name", "FIRSTNAME", "LASTNAME", "TITLE", "Email", "WORKPHONE", "Alternate01", "Alternate02", "PROFILE LINK","CREATED BY"
        };
            for (int i = 0; i < headers.Length; i++)
                ws.Cells[1, i + 1].Value = headers[i];
            int row = 2;
            foreach (var en in demandayverificationRows)
            {
                int col = 1;
                ws.Cells[row, col++].Value = en.Id;
                ws.Cells[row, col++].Value = en.SrNo;
                ws.Cells[row, col++].Value = en.AgentName;
                ws.Cells[row, col++].Value = en.CdqaComments;
                ws.Cells[row, col++].Value = en.CampaignId;
                ws.Cells[row, col++].Value = en.CompanyName;
                ws.Cells[row, col++].Value = en.FirstName;
                ws.Cells[row, col++].Value = en.LastName;
                ws.Cells[row, col++].Value = en.Title;
                ws.Cells[row, col++].Value = en.Email;
                ws.Cells[row, col++].Value = en.WorkPhone;
                ws.Cells[row, col++].Value = en.Alternate01;
                ws.Cells[row, col++].Value = en.Alternate02;
                ws.Cells[row, col++].Value = en.ProfileLink;
                ws.Cells[row, col++].Value = en.OwnerId;
                row++;
            }
            ws.Cells.AutoFitColumns();
            return package.GetAsByteArray();
        }
    }
}
