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
using AdvanceCRM.Web.Modules.Common.AppServices;
using MyRow = AdvanceCRM.Demanday.DemandayTeamLeaderRow;

namespace AdvanceCRM.Demanday.Endpoints
{
    [Route("Services/Demanday/DemandayTeamLeader/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class DemandayTeamLeaderController : ServiceEndpoint
    {
        private readonly ISqlConnections sqlConnections;

        public DemandayTeamLeaderController(ISqlConnections sqlConnections)
        {
            this.sqlConnections = sqlConnections;
        }
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayTeamLeaderSaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayTeamLeaderSaveHandler handler)
        {
            return handler.Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] IDemandayTeamLeaderDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] IDemandayTeamLeaderRetrieveHandler handler)
        {
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] IDemandayTeamLeaderListHandler handler)
        {
            return handler.List(connection, request);
        }
        [HttpPost, AuthorizeUpdate(typeof(DemandayTeamLeaderRow))]
        public StandardResponse MoveToQuality(IUnitOfWork uow, MoveToDemandayQualityRequest request)
        {
            if (request?.Ids == null || request.Ids.Count == 0)
                throw new ValidationError("Please select at least one record.");

            var response = new StandardResponse();
            var demandayteamleaderConn = sqlConnections.NewFor<DemandayTeamLeaderRow>();
            var demandayqualityConn = sqlConnections.NewFor<DemandayQualityRow>();


            foreach (var id in request.Ids)
            {
                var demandayteamleader = demandayteamleaderConn.TryById<DemandayTeamLeaderRow>(id);
                if (demandayteamleader == null)
                    throw new ValidationError("Enquiry record not found!");
                var demandayquality = new DemandayQualityRow
                {
                    Slot = demandayteamleader.Slot,
                    CompanyName = demandayteamleader.CompanyName,
                    FirstName = demandayteamleader.FirstName,
                    LastName = demandayteamleader.LastName,
                    Title = demandayteamleader.Title,
                    Email = demandayteamleader.Email,
                    CampaignId = demandayteamleader.CampaignId,
                    WorkPhone = demandayteamleader.WorkPhone,
                    AlternativeNumber = demandayteamleader.AlternativeNumber,
                    Street = demandayteamleader.Street,
                    City = demandayteamleader.City,
                    State = demandayteamleader.State,
                    ZipCode = demandayteamleader.ZipCode,
                    Country = demandayteamleader.Country,
                    Industry = demandayteamleader.Industry,
                    SubIndustry = demandayteamleader.SubIndustry,
                    Revenue = demandayteamleader.Revenue,
                    CompanyEmployeeSize = demandayteamleader.CompanyEmployeeSize,
                    ZoomInfoIndustry = demandayteamleader.ZoomInfoIndustry,
                    Date = demandayteamleader.Date,
                    ZoomInfoEmployeeSize = demandayteamleader.ZoomInfoEmployeeSize,
                    ProfileLink = demandayteamleader.ProfileLink,
                    CompanyLink = demandayteamleader.CompanyLink,
                    EmailFormat = demandayteamleader.EmailFormat,
                    RevenueLink = demandayteamleader.RevenueLink,
                    AdressLink = demandayteamleader.AddressLink,
                    Tenurity = demandayteamleader.Tenurity,
                    Code = demandayteamleader.Code,
                    Md5 = demandayteamleader.Md5,
                    OwnerId = demandayteamleader.OwnerId
                };

                demandayqualityConn.Insert(demandayquality);
                demandayteamleaderConn.DeleteById<DemandayTeamLeaderRow>(id);

                response.Id = demandayquality.Id ?? 0;
            }
            response.Status = "Enquiry successfully moved to Quality module!";

            return response;
        }
        public class MoveToDemandayQualityRequest : ServiceRequest
        {
            public List<int> Ids { get; set; }
        }
        [HttpPost, IgnoreAntiforgeryToken, AuthorizeList(typeof(DemandayTeamLeaderRow))]
        public FileContentResult ListExcel(
            IDbConnection connection,
            [FromForm] ListRequest request,
            [FromForm] string Ids,
            [FromServices] IDemandayTeamLeaderListHandler handler)
        {
            request ??= new ListRequest { Take = 0 };
            var data = List(connection, request, handler).Entities.ToList();

            // When records are selected in the grid, export only those; otherwise export all.
            if (!string.IsNullOrWhiteSpace(Ids))
            {
                var idList = Ids.Split(',').Select(x =>
                {
                    int v; return int.TryParse(x.Trim(), out v) ? (int?)v : null;
                }).Where(x => x.HasValue).Select(x => x!.Value).ToHashSet();
                if (idList.Count > 0)
                    data = data.Where(x => x.Id.HasValue && idList.Contains(x.Id.Value)).ToList();
            }

            // Ensure the "Created By" (OwnerUsername) column is populated for export.
            var ownerIds = data.Where(x => x.OwnerId.HasValue &&
                                           string.IsNullOrEmpty(x.OwnerUsername))
                                .Select(x => x.OwnerId.Value)
                                .Distinct()
                                .ToList();
            if (ownerIds.Count > 0)
            {
                var uFlds = AdvanceCRM.Administration.UserRow.Fields;
                var userMap = connection.List<AdvanceCRM.Administration.UserRow>(q => q
                        .Select(uFlds.UserId)
                        .Select(uFlds.Username)
                        .Where(uFlds.UserId.In(ownerIds)))
                    .Where(u => u.UserId.HasValue)
                    .GroupBy(u => u.UserId.Value)
                    .ToDictionary(g => g.Key, g => g.First().Username);

                foreach (var row in data)
                {
                    if (string.IsNullOrEmpty(row.OwnerUsername) && row.OwnerId.HasValue &&
                        userMap.TryGetValue(row.OwnerId.Value, out var uname))
                        row.OwnerUsername = uname;
                }
            }

            var bytes = DemandayTeamLeaderExcelExporter.ExportToExcel(data);
            var fileName = "DemandayTeamLeaderList_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx";
            return ExcelContentResult.Create(bytes, fileName);
        }

        // Download a blank Excel template containing the importable Demanday Team Leader columns.
        public FileContentResult DownloadTemplate()
        {
            var headers = new[]
            {
                "Slot", "Campaign Id", "First Name", "Last Name", "Title", "Email",
                "Work Phone", "Alternative Number", "Company Name", "Industry", "Revenue",
                "Company Employee Size", "ZoomInfo Industry", "Sub Industry", "ZoomInfo Employee Size",
                "Street", "City", "State", "Zip Code", "Country",
                "Profile Link", "Company Link", "Revenue Link", "Address Link", "Email Format",
                "Tenurity", "Code", "Md5", "Date", "Created By", "Demanday Enquiry Id"
            };
            return ExcelContentResult.Create(BuildTemplate("DemandayTeamLeader", headers),
                "DemandayTeamLeader_Template.xlsx");
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

        // Excel import for the Demanday Team Leader form fields.
        [HttpPost, IgnoreAntiforgeryToken]
        [RequestSizeLimit(52428800)]
        [RequestFormLimits(MultipartBodyLengthLimit = 52428800)]
        public IActionResult ImportExcel([FromServices] IUnitOfWork uow, IFormFile file,
            [FromServices] IDemandayTeamLeaderSaveHandler saveHandler)
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
                            var teamLeader = new MyRow
                            {
                                Id = ExcelImportHelper.GetInt(ws, row, map, "Id"),
                                Slot = ExcelImportHelper.GetText(ws, row, map, "Slot"),
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
                                AddressLink = ExcelImportHelper.GetText(ws, row, map, "AddressLink", "Address Link", "AdressLink", "Adress Link"),
                                EmailFormat = ExcelImportHelper.GetText(ws, row, map, "EmailFormat", "Email Format"),
                                Tenurity = ExcelImportHelper.GetText(ws, row, map, "Tenurity"),
                                Code = ExcelImportHelper.GetText(ws, row, map, "Code"),
                                Md5 = ExcelImportHelper.GetText(ws, row, map, "Md5", "MD5"),
                                Date = ExcelImportHelper.GetDate(ws, row, map, "Date"),
                                OwnerId = ResolveOwnerId(ws, row, map, userByName),
                                DemandayEnquiryId = ExcelImportHelper.GetInt(ws, row, map, "DemandayEnquiryId", "Demanday Enquiry Id", "Demanday Enquiry ID"),
                            };
                            if (teamLeader.Id.HasValue && teamLeader.Id.Value > 0)
                            {
                                skipped++; continue;
                            }
                            ExcelImportHelper.ClampStringFields(teamLeader);
                            var creReq = new SaveRequest<MyRow> { Entity = teamLeader };
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