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
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingEnquiryRow;

namespace AdvanceCRM.Demanday.Endpoints
{
    [Route("Services/Demanday/DemandayTeleMarketingEnquiry/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class DemandayTeleMarketingEnquiryController : ServiceEndpoint
    {
        private readonly ISqlConnections sqlConnections;
        public DemandayTeleMarketingEnquiryController(ISqlConnections sqlConnections)
        {
            this.sqlConnections = sqlConnections;
        }

        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayTeleMarketingEnquirySaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayTeleMarketingEnquirySaveHandler handler)
        {
            return handler.Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] IDemandayTeleMarketingEnquiryDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] IDemandayTeleMarketingEnquiryRetrieveHandler handler)
        {
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] IDemandayTeleMarketingEnquiryListHandler handler)
        {
            return handler.List(connection, request);
        }

        public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
            [FromServices] IDemandayTeleMarketingEnquiryListHandler handler,
            [FromServices] IExcelExporter exporter)
        {
            var data = List(connection, request, handler).Entities;
            var bytes = exporter.Export(data, typeof(Columns.DemandayTeleMarketingEnquiryColumns), request.ExportColumns);
            return ExcelContentResult.Create(bytes, "DemandayTeleMarketingEnquiryList_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
        }

        // Move the selected TM Enquiry records to the Team Leader module
        // (copies the same fields and the QA details, then removes the original enquiry).
        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public StandardResponse MoveToTeamLeader(IUnitOfWork uow, MoveToTeamLeaderRequest request)
        {
            if (request?.Ids == null)
                throw new ArgumentNullException(nameof(request.Ids));

            var response = new StandardResponse();
            var enquiryConn = sqlConnections.NewFor<DemandayTeleMarketingEnquiryRow>();
            var teamLeaderConn = sqlConnections.NewFor<DemandayTeleMarketingTeamLeaderRow>();

            foreach (var id in request.Ids)
            {
                var enquiry = enquiryConn.TryById<DemandayTeleMarketingEnquiryRow>(id);
                if (enquiry == null)
                    throw new ValidationError("Enquiry record not found!");

                var teamLeader = new DemandayTeleMarketingTeamLeaderRow
                {
                    CompanyName = enquiry.CompanyName,
                    FirstName = enquiry.FirstName,
                    LastName = enquiry.LastName,
                    Title = enquiry.Title,
                    Email = enquiry.Email,
                    WorkPhone = enquiry.WorkPhone,
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
                    CallStatus = enquiry.CallStatus,
                    AdditionalNotes = enquiry.AdditionalNotes,
                    Asset = enquiry.Asset,
                    ProfileLink = enquiry.ProfileLink,
                    CompanyLink = enquiry.CompanyLink,
                    RevenueLink = enquiry.RevenueLink,
                    AddressLink = enquiry.AddressLink,
                    EmailFormat = enquiry.EmailFormat,
                    Tenurity = enquiry.Tenurity,
                    Code = enquiry.Code,
                    Link = enquiry.Link,
                    Md5 = enquiry.Md5,
                    Attachments = enquiry.Attachments,
                    OwnerId = enquiry.OwnerId
                };

                teamLeaderConn.Insert(teamLeader);

                // Serenity Insert does not populate identity; retrieve via @@IDENTITY
                // (@@IDENTITY is connection-scoped, safe on this dedicated connection).
                using (var cmd = teamLeaderConn.CreateCommand())
                {
                    cmd.CommandText = "SELECT CAST(@@IDENTITY AS INT)";
                    var newId = cmd.ExecuteScalar();
                    if (newId != null && newId != DBNull.Value)
                        teamLeader.Id = Convert.ToInt32(newId);
                }

                // Move QADetails from the enquiry to the new Team Leader record.
                var qaFlds = DemandayTeleMarketingEnquiryQADetailsRow.Fields;
                var qaDetails = enquiryConn.List<DemandayTeleMarketingEnquiryQADetailsRow>(q =>
                    q.Select(qaFlds.Id, qaFlds.EnquiryId, qaFlds.QuestionId, qaFlds.AnswerId)
                     .Where(qaFlds.EnquiryId == id));

                foreach (var qa in qaDetails)
                {
                    enquiryConn.Insert(new DemandayTeleMarketingEnquiryQADetailsRow
                    {
                        EnquiryId = teamLeader.Id,
                        QuestionId = qa.QuestionId,
                        AnswerId = qa.AnswerId
                    });
                    enquiryConn.DeleteById<DemandayTeleMarketingEnquiryQADetailsRow>(qa.Id.Value);
                }

                enquiryConn.DeleteById<DemandayTeleMarketingEnquiryRow>(id);
                response.Id = teamLeader.Id ?? 0;
            }
            response.Status = "Enquiry successfully moved to Team Leader!";

            return response;
        }

        public class MoveToTeamLeaderRequest : ServiceRequest
        {
            public List<int> Ids { get; set; }
        }

        // Download a blank Excel template containing the importable TM Enquiry columns.
        public FileContentResult DownloadTemplate()
        {
            var headers = new[]
            {
                "Campaign Id", "First Name", "Last Name", "Title", "Email",
                "Work Phone", "Alternative Number", "Company Name", "Industry", "Revenue",
                "Company Employee Size", "ZoomInfo Industry", "Sub Industry", "ZoomInfo Employee Size",
                "Asset", "Call Status", "Street", "City", "State", "Zip Code", "Country",
                "Profile Link", "Company Link", "Revenue Link", "Address Link", "Email Format",
                "Tenurity", "Code", "Md5", "Date", "Additional Notes", "Created By"
            };
            return ExcelContentResult.Create(BuildTemplate("DemandayTeleMarketingEnquiry", headers),
                "DemandayTeleMarketingEnquiry_Template.xlsx");
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

        // Excel import for the TM Enquiry form fields.
        [HttpPost, IgnoreAntiforgeryToken]
        [RequestSizeLimit(52428800)]
        [RequestFormLimits(MultipartBodyLengthLimit = 52428800)]
        public IActionResult ImportExcel([FromServices] IUnitOfWork uow, IFormFile file,
            [FromServices] IDemandayTeleMarketingEnquirySaveHandler saveHandler)
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
                            var tmenquiry = new DemandayTeleMarketingEnquiryRow
                            {
                                Id = ExcelImportHelper.GetInt(ws, row, map, "Id"),
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
                                Asset = ExcelImportHelper.GetText(ws, row, map, "Asset"),
                                CallStatus = ExcelImportHelper.GetText(ws, row, map, "CallStatus", "Call Status"),
                                Street = ExcelImportHelper.GetText(ws, row, map, "Street"),
                                City = ExcelImportHelper.GetText(ws, row, map, "City"),
                                State = ExcelImportHelper.GetText(ws, row, map, "State"),
                                ZipCode = ExcelImportHelper.GetText(ws, row, map, "ZipCode", "Zip Code"),
                                Country = ExcelImportHelper.GetText(ws, row, map, "Country"),
                                ProfileLink = ExcelImportHelper.GetText(ws, row, map, "ProfileLink", "Profile Link"),
                                CompanyLink = ExcelImportHelper.GetText(ws, row, map, "CompanyLink", "Company Link"),
                                RevenueLink = ExcelImportHelper.GetText(ws, row, map, "RevenueLink", "Revenue Link"),
                                AddressLink = ExcelImportHelper.GetText(ws, row, map, "AddressLink", "Address Link", "AdressLink", "Adress Link"),
                                EmailFormat = ExcelImportHelper.GetText(ws, row, map, "EmailFormat", "Email Format"),
                                Tenurity = ExcelImportHelper.GetText(ws, row, map, "Tenurity"),
                                Code = ExcelImportHelper.GetText(ws, row, map, "Code"),
                                Md5 = ExcelImportHelper.GetText(ws, row, map, "Md5", "MD5"),
                                Date = ExcelImportHelper.GetDate(ws, row, map, "Date"),
                                AdditionalNotes = ExcelImportHelper.GetText(ws, row, map, "AdditionalNotes", "Additional Notes"),
                                OwnerId = ResolveOwnerId(ws, row, map, userByName),
                            };
                            if (tmenquiry.Id.HasValue && tmenquiry.Id.Value > 0)
                            {
                                skipped++; continue;
                            }
                            ExcelImportHelper.ClampStringFields(tmenquiry);
                            var creReq = new SaveRequest<DemandayTeleMarketingEnquiryRow> { Entity = tmenquiry };
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