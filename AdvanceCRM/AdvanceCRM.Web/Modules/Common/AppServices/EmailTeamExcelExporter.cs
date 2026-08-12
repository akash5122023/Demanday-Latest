using AdvanceCRM.EmailTeam;
using OfficeOpenXml;
using System.Collections.Generic;

namespace AdvanceCRM.Web.Modules.Common.AppServices
{
    public class EmailTeamExcelExporter
    {
        // The columns the Email Team form offers, plus Id. Account, campaign and owner go out as
        // their readable values so the sheet can be read - and imported back - without ids.
        public static readonly string[] Headers = new[]
        {
            "Id", "MASTER ACCOUNT NO", "CAMPAIGN ID", "FIRST NAME", "LAST NAME", "EMAIL",
            "STATUS", "CREATED BY"
        };

        public static byte[] ExportToExcel(List<EmailTeamRow> emailTeamRows)
        {
            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("EmailTeam");

            for (int i = 0; i < Headers.Length; i++)
                ws.Cells[1, i + 1].Value = Headers[i];

            int row = 2;
            foreach (var en in emailTeamRows)
            {
                int col = 1;
                ws.Cells[row, col++].Value = en.Id;
                ws.Cells[row, col++].Value = en.MasterAccountNumber;
                ws.Cells[row, col++].Value = en.CampaignCode;
                ws.Cells[row, col++].Value = en.FirstName;
                ws.Cells[row, col++].Value = en.LastName;
                ws.Cells[row, col++].Value = en.Email;
                ws.Cells[row, col++].Value = ExcelEnumHelper.Text(en.Status);
                ws.Cells[row, col++].Value = en.OwnerUsername;
                row++;
            }

            ws.Cells.AutoFitColumns();
            return package.GetAsByteArray();
        }
    }
}
