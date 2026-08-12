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
using static MVC.Views.Demanday;
using MyRow = AdvanceCRM.Demanday.DemandayMisRow;

namespace AdvanceCRM.Demanday.Endpoints
{
    [Route("Services/Demanday/DemandayMis/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class DemandayMisController : ServiceEndpoint
    {
        private readonly ISqlConnections sqlConnections;
        public DemandayMisController(ISqlConnections sqlConnections)
        {
            this.sqlConnections = sqlConnections;
        }
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayMisSaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayMisSaveHandler handler)
        {
            return handler.Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] IDemandayMisDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] IDemandayMisRetrieveHandler handler)
        {
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] IDemandayMisListHandler handler)
        {
            return handler.List(connection, request);
        }
        [HttpPost, IgnoreAntiforgeryToken, AuthorizeList(typeof(DemandayMisRow))]
        public FileContentResult ListExcel(
        IDbConnection connection,
        [FromForm] ListRequest request, // Bind from form POSTs
        [FromForm] string Ids,
        [FromServices] IDemandayMisListHandler handler)
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
            var bytes = AdvanceCRM.Web.Modules.Common.AppServices.DemandayMisExcelExporter.ExportToExcel(data);
            var fileName = "DemandayMISList_" + DateTime.Now.ToString("yyyyMMdd_HHmmss", System.Globalization.CultureInfo.InvariantCulture) + ".xlsx";
            return Serenity.Web.ExcelContentResult.Create(bytes, fileName);
        }
        [HttpPost, IgnoreAntiforgeryToken]
        [RequestSizeLimit(52428800)]
        [RequestFormLimits(MultipartBodyLengthLimit = 52428800)]
        public IActionResult ImportExcel([FromServices] IUnitOfWork uow, IFormFile file, [FromServices] IDemandayMisSaveHandler saveHandler)
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
                    for (int row = 2; row <= rowCount; row++)
                    {
                        try
                        {
                            var demandaymis = new DemandayMisRow
                            {
                                Id = ExcelImportHelper.GetInt(ws, row, map, "Id"),
                                // Prefer the readable "Master Account No" that the template and the export now
                                // carry; fall back to a raw id column so older files still import.
                                MasterAccountId = ExcelImportHelper.GetMasterAccountId(ws, row, map, accounts,
                                        "Master Account No", "Account Number", "Account No")
                                    ?? ExcelImportHelper.GetInt(ws, row, map, "MasterAccountId", "Master Account Id"),
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
                                ZipCode = ExcelImportHelper.GetText(ws, row, map, "ZipCode", "Zip Code"),
                                Country = ExcelImportHelper.GetText(ws, row, map, "Country"),
                                CompanyEmployeeSize = ExcelImportHelper.GetText(ws, row, map, "CompanyEmployeeSize", "Company Employee Size"),
                                Industry = ExcelImportHelper.GetText(ws, row, map, "Industry"),
                                SubIndustry = ExcelImportHelper.GetText(ws, row, map, "SubIndustry", "Sub Industry"),
                                ZoomInfoIndustry = ExcelImportHelper.GetText(ws, row, map, "ZoomInfoIndustry", "ZoomInfo Industry", "ZOOMINFO INDUSTRY"),
                                ZoomInfoEmployeeSize = ExcelImportHelper.GetText(ws, row, map, "ZoomInfoEmployeeSize", "ZoomInfo Employee Size", "ZOOMINFO EMPLOYEE SIZE"),
                                Revenue = ExcelImportHelper.GetText(ws, row, map, "Revenue"),
                                Date = ExcelImportHelper.GetDate(ws, row, map, "Date"),
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
                                OwnerId = ExcelImportHelper.GetUserId(ws, row, map, uow.Connection, "OwnerId", "Owner", "Owner Name", "OwnerName", "Created By", "CreatedBy"),
                            };
                            if (demandaymis.Id.HasValue && demandaymis.Id.Value > 0)
                            {
                                skipped++; continue;
                            }
                            ExcelImportHelper.ClampStringFields(demandaymis);
                            var creReq = new Serenity.Services.SaveRequest<DemandayMisRow> { Entity = demandaymis };
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

        [HttpPost, AuthorizeUpdate(typeof(DemandayMisRow))]
        public StandardResponse MoveToETContacts(IUnitOfWork uow, MoveToETContactsRequest request)
        {
            if (request?.Ids == null)
                throw new ArgumentNullException(nameof(request.Ids));

            var response = new StandardResponse();
            var demandaymisConn = sqlConnections.NewFor<DemandayMisRow>();
            var demandaycontactsConn = sqlConnections.NewFor<DemandayContactsRow>();

            foreach (var id in request.Ids)
            {
                var demandaymis = demandaymisConn.TryById<DemandayMisRow>(id);

                if (demandaymis == null)
                    throw new ValidationError("MIS record not found!");

                var demandaycontacts = new DemandayContactsRow
                {
                    Slot = demandaymis.Slot,
                    PrimaryReason = demandaymis.PrimaryReason,
                    Category = demandaymis.Category,
                    Comments = demandaymis.Comments,
                    QaStatus = demandaymis.QaStatus,
                    CampaignId = demandaymis.CampaignId,
                    DeliveryStatus = demandaymis.DeliveryStatus,
                    AgentName = demandaymis.AgentName,
                    QaName = demandaymis.QaName,
                    CallDate = demandaymis.CallDate,
                    DateAudited = demandaymis.DateAudited,
                    DeliveryDate = demandaymis.DeliveryDate,
                    Source = demandaymis.Source,
                    VerificationMode = demandaymis.VerificationMode,
                    Asset1 = demandaymis.Asset1,
                    Asset2 = demandaymis.Asset2,
                    AgentsName = demandaymis.AgentsName,
                    TlName = demandaymis.TlName,
                    CompanyName = demandaymis.CompanyName,
                    FirstName = demandaymis.FirstName,
                    LastName = demandaymis.LastName,
                    Title = demandaymis.Title,
                    MasterAccountId = demandaymis.MasterAccountId,
                    Email = demandaymis.Email,
                    WorkPhone = demandaymis.WorkPhone,
                    AlternativeNumber = demandaymis.AlternativeNumber,
                    Domain = demandaymis.Domain,
                    JobLevel = demandaymis.JobLevel,
                    JobFunctionRole = demandaymis.JobFunctionRole,
                    Street = demandaymis.Street,
                    City = demandaymis.City,
                    State = demandaymis.State,
                    ZipCode = demandaymis.ZipCode,
                    Country = demandaymis.Country,
                    CompanyEmployeeSize = demandaymis.CompanyEmployeeSize,
                    Industry = demandaymis.Industry,
                    SubIndustry = demandaymis.SubIndustry,
                    ZoomInfoIndustry = demandaymis.ZoomInfoIndustry,
                    Date = demandaymis.Date,
                    ZoomInfoEmployeeSize = demandaymis.ZoomInfoEmployeeSize,
                    Revenue = demandaymis.Revenue,
                    ProfileLink = demandaymis.ProfileLink,
                    CompanyLink = demandaymis.CompanyLink,
                    RevenueLink = demandaymis.RevenueLink,
                    EmailFormat = demandaymis.EmailFormat,
                    AdressLink = demandaymis.AdressLink,
                    Tenurity = demandaymis.Tenurity,
                    Code = demandaymis.Code,
                    Link = demandaymis.Link,
                    Md5 = demandaymis.Md5,
                    OwnerId = demandaymis.OwnerId
                };

                demandaycontactsConn.Insert(demandaycontacts);

                // MIS record is intentionally retained (not deleted) after moving to ETContacts.
                response.Id = demandaycontacts.Id ?? 0;
            }
            response.Status = "MIS successfully moved to ETContacts module!";

            return response;
        }

        public class MoveToETContactsRequest : ServiceRequest
        {
            public List<int> Ids { get; set; }
        }

    }
}