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
using MyRow = AdvanceCRM.Demanday.DemandayEnquiryRow;

namespace AdvanceCRM.Demanday.Endpoints
{
    [Route("Services/Demanday/DemandayEnquiry/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class DemandayEnquiryController : ServiceEndpoint
    {
        private readonly ISqlConnections sqlConnections;
        public DemandayEnquiryController(ISqlConnections sqlConnections)
        {
            this.sqlConnections = sqlConnections;
        }

        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayEnquirySaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayEnquirySaveHandler handler)
        {
            return handler.Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] IDemandayEnquiryDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] IDemandayEnquiryRetrieveHandler handler)
        {
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] IDemandayEnquiryListHandler handler)
        {
            return handler.List(connection, request);
        }

        public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
            [FromServices] IDemandayEnquiryListHandler handler,
            [FromServices] IExcelExporter exporter)
        {
            var data = List(connection, request, handler).Entities;
            var bytes = exporter.Export(data, typeof(Columns.DemandayEnquiryColumns), request.ExportColumns);
            return ExcelContentResult.Create(bytes, "DemandayEnquiryList_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
        }

        // Move the selected Demanday Enquiry records to the Team Leader module
        // (copies the same fields, then removes the original enquiry).
        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public StandardResponse MoveToTeamLeader(IUnitOfWork uow, MoveToTeamLeaderRequest request)
        {
            if (request?.Ids == null)
                throw new ArgumentNullException(nameof(request.Ids));

            var response = new StandardResponse();
            var enquiryConn = sqlConnections.NewFor<DemandayEnquiryRow>();
            var teamLeaderConn = sqlConnections.NewFor<DemandayTeamLeaderRow>();

            foreach (var id in request.Ids)
            {
                var enquiry = enquiryConn.TryById<DemandayEnquiryRow>(id);
                if (enquiry == null)
                    throw new ValidationError("Enquiry record not found!");

                var teamLeader = new DemandayTeamLeaderRow
                {
                    CompanyName = enquiry.CompanyName,
                    FirstName = enquiry.FirstName,
                    LastName = enquiry.LastName,
                    Title = enquiry.Title,
                    Email = enquiry.Email,
                    WorkPhone = enquiry.WorkPhone,
                    MasterAccountId = enquiry.MasterAccountId,
                    CampaignId = enquiry.CampaignId,
                    AlternativeNumber = enquiry.AlternativeNumber,
                    Street = enquiry.Street,
                    City = enquiry.City,
                    State = enquiry.State,
                    ZipCode = enquiry.ZipCode,
                    Country = enquiry.Country,
                    Industry = enquiry.Industry,
                    SubIndustry = enquiry.SubIndustry,
                    Revenue = enquiry.Revenue,
                    CompanyEmployeeSize = enquiry.CompanyEmployeeSize,
                    ZoomInfoIndustry = enquiry.ZoomInfoIndustry,
                    Date = enquiry.Date,
                    ZoomInfoEmployeeSize = enquiry.ZoomInfoEmployeeSize,
                    ProfileLink = enquiry.ProfileLink,
                    CompanyLink = enquiry.CompanyLink,
                    RevenueLink = enquiry.RevenueLink,
                    AddressLink = enquiry.AdressLink,
                    EmailFormat = enquiry.EmailFormat,
                    Tenurity = enquiry.Tenurity,
                    Code = enquiry.Code,
                    Link = enquiry.Link,
                    Md5 = enquiry.Md5,
                    OwnerId = enquiry.OwnerId
                };
                teamLeaderConn.Insert(teamLeader);

                enquiryConn.DeleteById<DemandayEnquiryRow>(id);
                response.Id = teamLeader.Id ?? 0;
            }
            response.Status = "Enquiry successfully moved to Team Leader!";

            return response;
        }

        public class MoveToTeamLeaderRequest : ServiceRequest
        {
            public List<int> Ids { get; set; }
        }

        // Download a blank Excel template containing the importable Demanday Enquiry columns.
        public FileContentResult DownloadTemplate()
        {
            var headers = new[]
            {
                "Master Account No", "Campaign Id", "First Name", "Last Name", "Title", "Email",
                "Work Phone", "Alternative Number", "Company Name", "Industry", "Revenue",
                "Company Employee Size", "ZoomInfo Industry", "Sub Industry", "ZoomInfo Employee Size",
                "Street", "City", "State", "Zip Code", "Country",
                "Profile Link", "Company Link", "Revenue Link", "Address Link", "Email Format",
                "Tenurity", "Code", "Md5", "Date", "Created By"
            };
            return ExcelContentResult.Create(BuildTemplate("DemandayEnquiry", headers),
                "DemandayEnquiry_Template.xlsx");
        }

        private static byte[] BuildTemplate(string sheetName, string[] headers)
        {
            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add(sheetName);
                for (int c = 0; c < headers.Length; c++)
                {
                    var cell = ws.Cells[1, c + 1];
                    cell.Value = headers[c];
                    cell.Style.Font.Bold = true;
                    ws.Column(c + 1).Width = 22;
                }
                return package.GetAsByteArray();
            }
        }

        // Excel import for the Demanday Enquiry form fields.
        [HttpPost, IgnoreAntiforgeryToken]
        [RequestSizeLimit(52428800)]
        [RequestFormLimits(MultipartBodyLengthLimit = 52428800)]
        public IActionResult ImportExcel([FromServices] IUnitOfWork uow, IFormFile file,
            [FromServices] IDemandayEnquirySaveHandler saveHandler)
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
                    // Account Number -> id, read once so the per-row lookup below costs nothing.
                    var accounts = ExcelImportHelper.LoadMasterAccountMap(uow.Connection);

                    // Build a username -> UserId lookup so the exported "Created By"
                    // (which is a username string) can be resolved back to OwnerId.
                    var uFlds = AdvanceCRM.Administration.UserRow.Fields;
                    var userByName = uow.Connection.List<AdvanceCRM.Administration.UserRow>(q => q
                            .Select(uFlds.UserId)
                            .Select(uFlds.Username))
                        .Where(u => u.UserId.HasValue && !string.IsNullOrEmpty(u.Username))
                        .GroupBy(u => u.Username.Trim(), StringComparer.OrdinalIgnoreCase)
                        .ToDictionary(g => g.Key, g => g.First().UserId.Value,
                            StringComparer.OrdinalIgnoreCase);

                    for (int row = 2; row <= rowCount; row++)
                    {
                        try
                        {
                            var demandayenquiry = new DemandayEnquiryRow
                            {
                                Id = ExcelImportHelper.GetInt(ws, row, map, "Id"),
                                // Prefer the readable "Master Account No" that the template and the export now
                                // carry; fall back to a raw id column so older files still import.
                                MasterAccountId = ExcelImportHelper.GetMasterAccountId(ws, row, map, accounts,
                                        "Master Account No", "Account Number", "Account No")
                                    ?? ExcelImportHelper.GetInt(ws, row, map, "MasterAccountId", "Master Account Id"),
                                CampaignId = ExcelImportHelper.GetText(ws, row, map, "CampaignId", "Campaign Id"),
                                FirstName = ExcelImportHelper.GetText(ws, row, map, "FirstName", "First Name"),
                                LastName = ExcelImportHelper.GetText(ws, row, map, "LastName", "Last Name"),
                                Title = ExcelImportHelper.GetText(ws, row, map, "Title"),
                                Email = ExcelImportHelper.GetText(ws, row, map, "Email"),
                                WorkPhone = ExcelImportHelper.GetText(ws, row, map, "WorkPhone", "Work Phone"),
                                AlternativeNumber = ExcelImportHelper.GetText(ws, row, map, "AlternativeNumber", "Alternative Number"),
                                CompanyName = ExcelImportHelper.GetText(ws, row, map, "CompanyName", "Company Name"),
                                Industry = ExcelImportHelper.GetText(ws, row, map, "Industry"),
                                Revenue = ExcelImportHelper.GetText(ws, row, map, "Revenue"),
                                CompanyEmployeeSize = ExcelImportHelper.GetText(ws, row, map, "CompanyEmployeeSize", "Company Employee Size", "Employee Size"),
                                ZoomInfoIndustry = ExcelImportHelper.GetText(ws, row, map, "ZoomInfoIndustry", "ZoomInfo Industry", "ZOOMINFO INDUSTRY"),
                                SubIndustry = ExcelImportHelper.GetText(ws, row, map, "SubIndustry", "Sub Industry"),
                                ZoomInfoEmployeeSize = ExcelImportHelper.GetText(ws, row, map, "ZoomInfoEmployeeSize", "ZoomInfo Employee Size", "ZoomInfo EmployeeSize", "ZOOMINFO EMPLOYEE SIZE"),
                                Street = ExcelImportHelper.GetText(ws, row, map, "Street"),
                                City = ExcelImportHelper.GetText(ws, row, map, "City"),
                                State = ExcelImportHelper.GetText(ws, row, map, "State"),
                                ZipCode = ExcelImportHelper.GetText(ws, row, map, "ZipCode", "Zip Code"),
                                Country = ExcelImportHelper.GetText(ws, row, map, "Country"),
                                ProfileLink = ExcelImportHelper.GetText(ws, row, map, "ProfileLink", "Profile Link"),
                                CompanyLink = ExcelImportHelper.GetText(ws, row, map, "CompanyLink", "Company Link"),
                                RevenueLink = ExcelImportHelper.GetText(ws, row, map, "RevenueLink", "Revenue Link"),
                                AdressLink = ExcelImportHelper.GetText(ws, row, map, "AdressLink", "Adress Link", "AddressLink", "Address Link"),
                                EmailFormat = ExcelImportHelper.GetText(ws, row, map, "EmailFormat", "Email Format"),
                                Tenurity = ExcelImportHelper.GetText(ws, row, map, "Tenurity"),
                                Code = ExcelImportHelper.GetText(ws, row, map, "Code"),
                                Md5 = ExcelImportHelper.GetText(ws, row, map, "Md5", "MD5"),
                                Date = ExcelImportHelper.GetDate(ws, row, map, "Date"),
                                OwnerId = ResolveOwnerId(ws, row, map, userByName),
                            };
                            if (demandayenquiry.Id.HasValue && demandayenquiry.Id.Value > 0)
                            {
                                skipped++; continue;
                            }
                            ExcelImportHelper.ClampStringFields(demandayenquiry);
                            var creReq = new SaveRequest<DemandayEnquiryRow> { Entity = demandayenquiry };
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

        // Resolve OwnerId from the "Created By" column: accept a raw UserId int,
        // otherwise treat the cell as a username and map it back via userByName.
        private static int? ResolveOwnerId(ExcelWorksheet ws, int row,
            Dictionary<string, int> map, Dictionary<string, int> userByName)
        {
            var byId = ExcelImportHelper.GetInt(ws, row, map, "OwnerId", "Created By", "CreatedBy");
            if (byId.HasValue)
                return byId;

            var name = ExcelImportHelper.GetText(ws, row, map,
                "OwnerUsername", "Owner Username", "Created By", "CreatedBy", "OwnerId");
            if (!string.IsNullOrWhiteSpace(name) && userByName != null &&
                userByName.TryGetValue(name.Trim(), out var uid))
                return uid;

            return null;
        }
    }
}