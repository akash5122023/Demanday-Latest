using AdvanceCRM.Web.Modules.Common.AppServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Serenity;
using Serenity.Data;
using Serenity.Reporting;
using Serenity.Services;
using Serenity.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using MyRow = AdvanceCRM.Demanday.DemandayContactsRow;

namespace AdvanceCRM.Demanday.Endpoints
{
    [Route("Services/Demanday/DemandayContacts/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class DemandayContactsController : ServiceEndpoint
    {
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayContactsSaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayContactsSaveHandler handler)
        {
            return handler.Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] IDemandayContactsDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] IDemandayContactsRetrieveHandler handler)
        {
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] IDemandayContactsListHandler handler)
        {
            return handler.List(connection, request);
        }

        [HttpPost, IgnoreAntiforgeryToken, AuthorizeList(typeof(DemandayContactsRow))]
        public FileContentResult ListExcel(
            IDbConnection connection,
            [FromForm] ListRequest request, // Bind from form POSTs
            [FromForm] string Ids,
            [FromServices] IDemandayContactsListHandler handler)
        {
            request ??= new ListRequest { Take = 0 }; // Defensive: always have a request
            var data = List(connection, request, handler).Entities.ToList();
            if (!string.IsNullOrWhiteSpace(Ids))
            {
                var idList = Ids.Split(',').Select(x =>
                {
                    int v; return int.TryParse(x.Trim(), out v) ? (int?)v : null;
                }).Where(x => x.HasValue).Select(x => x!.Value).ToHashSet();
                if (idList.Count > 0)
                    data = data.Where(x => x.Id.HasValue && idList.Contains(x.Id.Value)).ToList();
            }
            var bytes = AdvanceCRM.Web.Modules.Common.AppServices.DemandayContactsExcelExporter.ExportToExcel(data);
            var fileName = "DemandayContactsList_" + DateTime.Now.ToString("yyyyMMdd_HHmmss", System.Globalization.CultureInfo.InvariantCulture) + ".xlsx";
            return Serenity.Web.ExcelContentResult.Create(bytes, fileName);
        }

        [HttpPost, IgnoreAntiforgeryToken]
        [RequestSizeLimit(52428800)]
        [RequestFormLimits(MultipartBodyLengthLimit = 52428800)]
        public IActionResult ImportExcel([FromServices] IUnitOfWork uow, IFormFile file, [FromServices] IDemandayContactsSaveHandler saveHandler)
        {
            try
            {
                if (file == null || !file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                    return Content("Please upload a valid .xlsx file.", "text/plain");
                int imported = 0, skipped = 0, failed = 0;
                var errors = new List<string>();
                using (var package = new ExcelPackage(file.OpenReadStream()))
                {
                    var ws = package.Workbook.Worksheets[0];
                    int rowCount = ws.Dimension.End.Row;
                    var map = ExcelImportHelper.BuildHeaderMap(ws);
                    for (int row = 2; row <= rowCount; row++)
                    {
                        try
                        {
                            var demandaycontacts = new DemandayContactsRow
                            {
                                Id = ExcelImportHelper.GetInt(ws, row, map, "Id"),
                                Slot = ExcelImportHelper.GetText(ws, row, map, "Slot"),
                                CampaignId = ExcelImportHelper.GetText(ws, row, map, "CampaignId", "Campaign Id"),
                                CompanyName = ExcelImportHelper.GetText(ws, row, map, "CompanyName", "Company Name"),
                                FirstName = ExcelImportHelper.GetText(ws, row, map, "FirstName", "First Name"),
                                LastName = ExcelImportHelper.GetText(ws, row, map, "LastName", "Last Name"),
                                Title = ExcelImportHelper.GetText(ws, row, map, "Title"),
                                Email = ExcelImportHelper.GetText(ws, row, map, "Email"),
                                WorkPhone = ExcelImportHelper.GetText(ws, row, map, "WorkPhone", "Work Phone"),
                                AlternativeNumber = ExcelImportHelper.GetText(ws, row, map, "AlternativeNumber", "Alternative Number"),
                                Domain = ExcelImportHelper.GetText(ws, row, map, "Domain"),
                                JobLevel = ExcelImportHelper.GetText(ws, row, map, "JobLevel", "Job Level", "Job_Level"),
                                JobFunctionRole = ExcelImportHelper.GetText(ws, row, map, "JobFunctionRole", "Job Function Role", "Job_Function_Role", "JobFunction", "Job Function"),
                                Street = ExcelImportHelper.GetText(ws, row, map, "Street"),
                                City = ExcelImportHelper.GetText(ws, row, map, "City"),
                                State = ExcelImportHelper.GetText(ws, row, map, "State"),
                                Date = ExcelImportHelper.GetDate(ws, row, map, "Date"),
                                ZipCode = ExcelImportHelper.GetText(ws, row, map, "ZipCode", "Zip Code"),
                                Country = ExcelImportHelper.GetText(ws, row, map, "Country"),
                                Industry = ExcelImportHelper.GetText(ws, row, map, "Industry"),
                                SubIndustry = ExcelImportHelper.GetText(ws, row, map, "SubIndustry", "Sub Industry", "SUB INDUSTRY"),
                                ZoomInfoIndustry = ExcelImportHelper.GetText(ws, row, map, "ZoomInfoIndustry", "ZoomInfo Industry", "ZOOMINFO INDUSTRY"),
                                ZoomInfoEmployeeSize = ExcelImportHelper.GetText(ws, row, map, "ZoomInfoEmployeeSize", "ZoomInfo Employee Size", "ZOOMINFO EMPLOYEE SIZE"),
                                Revenue = ExcelImportHelper.GetText(ws, row, map, "Revenue"),
                                CompanyEmployeeSize = ExcelImportHelper.GetText(ws, row, map, "CompanyEmployeeSize", "Company Employee Size"),
                                ProfileLink = ExcelImportHelper.GetText(ws, row, map, "ProfileLink", "Profile Link"),
                                CompanyLink = ExcelImportHelper.GetText(ws, row, map, "CompanyLink", "Company Link"),
                                RevenueLink = ExcelImportHelper.GetText(ws, row, map, "RevenueLink", "Revenue Link"),
                                EmailFormat = ExcelImportHelper.GetText(ws, row, map, "EmailFormat", "Email Format"),
                                AdressLink = ExcelImportHelper.GetText(ws, row, map, "AdressLink", "Adress Link", "AddressLink", "Address Link"),
                                PrimaryReason = ExcelImportHelper.GetText(ws, row, map, "PrimaryReason", "Primary Reason"),
                                Category = ExcelImportHelper.GetText(ws, row, map, "Category"),
                                Comments = ExcelImportHelper.GetText(ws, row, map, "Comments"),
                                QaStatus = ExcelImportHelper.GetText(ws, row, map, "QaStatus", "QA Status"),
                                DeliveryStatus = ExcelImportHelper.GetText(ws, row, map, "DeliveryStatus", "Delivery Status"),
                                AgentName = ExcelImportHelper.GetText(ws, row, map, "AgentName", "Agent Name"),
                                AgentsName = ExcelImportHelper.GetText(ws, row, map, "AgentsName", "Agents Name"),
                                QaName = ExcelImportHelper.GetText(ws, row, map, "QaName", "QA Name"),
                                CallDate = ExcelImportHelper.GetDate(ws, row, map, "CallDate", "Call Date"),
                                DateAudited = ExcelImportHelper.GetDate(ws, row, map, "DateAudited", "Date Audited"),
                                DeliveryDate = ExcelImportHelper.GetDate(ws, row, map, "DeliveryDate", "Delivery Date"),
                                Source = ExcelImportHelper.GetText(ws, row, map, "Source"),
                                VerificationMode = ExcelImportHelper.GetText(ws, row, map, "VerificationMode", "Verification Mode"),
                                Asset1 = ExcelImportHelper.GetText(ws, row, map, "Asset1", "Asset 1"),
                                Asset2 = ExcelImportHelper.GetText(ws, row, map, "Asset2", "Asset 2"),
                                TlName = ExcelImportHelper.GetText(ws, row, map, "TlName", "TL Name"),
                                Tenurity = ExcelImportHelper.GetText(ws, row, map, "Tenurity"),
                                Code = ExcelImportHelper.GetText(ws, row, map, "Code"),
                                Link = ExcelImportHelper.GetText(ws, row, map, "Link"),
                                Md5 = ExcelImportHelper.GetText(ws, row, map, "Md5", "MD5"),
                                OwnerId = ExcelImportHelper.GetUserId(ws, row, map, uow.Connection, "OwnerId", "Owner", "Owner Name", "OwnerName", "Created By", "CreatedBy")
                            };
                            if (demandaycontacts.Id.HasValue && demandaycontacts.Id.Value > 0)
                            {
                                skipped++; continue;
                            }
                            ExcelImportHelper.ClampStringFields(demandaycontacts);
                            var creReq = new Serenity.Services.SaveRequest<DemandayContactsRow> { Entity = demandaycontacts };
                            saveHandler.Create(uow, creReq);
                            imported++;
                        }
                        catch (Exception ex)
                        {
                            failed++; errors.Add($"Row {row}: {ex.Message}");
                        }
                    }
                }
                if (imported == 0 && failed > 0)
                    return Content("All rows failed to import.\n" + string.Join("\n", errors), "text/plain");
                return Content($"Added: {imported}, Skipped (existing IDs): {skipped}, Failed: {failed}\n" + (errors.Count > 0 ? string.Join("\n", errors) : ""), "text/plain");
            }
            catch (Exception ex)
            {
                return Content("Import failed: " + ex.Message + "\n" + ex.StackTrace, "text/plain");
            }
        }

        private static int? GetInt(object val) { if (val == null) return null; int i; return int.TryParse(val.ToString(), out i) ? i : null; }
        private static decimal? GetDecimal(object val) { if (val == null) return null; decimal d; return decimal.TryParse(val.ToString(), out d) ? d : null; }
        private static DateTime? GetDate(object val) { if (val == null) return null; DateTime dt; return DateTime.TryParse(val.ToString(), out dt) ? dt : null; }

        [HttpGet, HttpPost, IgnoreAntiforgeryToken, AuthorizeList(typeof(DemandayContactsRow))]
        public FileContentResult DownloadTemplate()
        {
            string[] headers = new[]
            {
                "Id",
                "SLOT",
                "Company Name",
                "First Name",
                "Last Name",
                "Title",
                "Email",
                "Work Phone",
                "Alternative Number",
                "Domain",
                "Job Level",
                "Job Function Role",
                "Street",
                "City",
                "State",
                "Zip Code",
                "Country",
                "Industry",
                "ZoomInfo Industry",
                "ZoomInfo Employee Size",
                "Revenue",
                "Company Employee Size",
                "Profile Link",
                "Company Link",
                "Revenue Link",
                "Email Format",
                "Adress Link",
                "Primary Reason",
                "Category",
                "Comments",
                "QA Status",
                "Delivery Status",
                "Agent Name",
                "QA Name",
                "Call Date",
                "Date Audited",
                "Delivery Date",
                "Source",
                "Verification Mode",
                "Asset 1",
                "Asset 2",
                "TL Name",
                "Tenurity",
                "Code",
                "Link",
                "MD5",
                "Created By"
            };
            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("DemandayContacts");
            for (int i = 0; i < headers.Length; i++)
            {
                var cell = ws.Cells[1, i + 1];
                cell.Value = headers[i];
                cell.Style.Font.Bold = true;
            }
            ws.Cells.AutoFitColumns();
            var bytes = package.GetAsByteArray();
            var fileName = "DemandayContacts_ImportTemplate.xlsx";
            return Serenity.Web.ExcelContentResult.Create(bytes, fileName);
        }
    }
}