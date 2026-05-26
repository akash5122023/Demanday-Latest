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
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingQualiltyRow;

namespace AdvanceCRM.Demanday.Endpoints
{
    [Route("Services/Demanday/DemandayTeleMarketingQualilty/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class DemandayTeleMarketingQualiltyController : ServiceEndpoint
    {
        private readonly ISqlConnections sqlConnections;
        public DemandayTeleMarketingQualiltyController(ISqlConnections sqlConnections)
        {
            this.sqlConnections = sqlConnections;
        }
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayTeleMarketingQualiltySaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayTeleMarketingQualiltySaveHandler handler)
        {
            return handler.Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] IDemandayTeleMarketingQualiltyDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] IDemandayTeleMarketingQualiltyRetrieveHandler handler)
        {
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] IDemandayTeleMarketingQualiltyListHandler handler)
        {
            return handler.List(connection, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(DemandayTeleMarketingQualiltyRow))]
        public StandardResponse MoveToTeleMarketingMIS(IUnitOfWork uow, MoveToTeleMarketingMISRequest request)
        {
            if (request?.Ids == null)
                throw new ArgumentNullException(nameof(request.Ids));

            var response = new StandardResponse();
            var demandaytelemarketingqualityConn = sqlConnections.NewFor<DemandayTeleMarketingQualiltyRow>();
            var demandaytelemarketingmisConn = sqlConnections.NewFor<DemandayTeleMarketingMISRow>();

            foreach (var id in request.Ids)
            {
                var demandaytelemarketingquality = demandaytelemarketingqualityConn.TryById<DemandayTeleMarketingQualiltyRow>(id);

                if (demandaytelemarketingquality == null)
                    throw new ValidationError("Quality record not found!");


                var demandayTeleMarketingMIS = new DemandayTeleMarketingMISRow
                {

                    // Map fields that exist in EnquiryRow

                    Slot = demandaytelemarketingquality.Slot,
                    PrimaryReason = demandaytelemarketingquality.PrimaryReason,
                    Category = demandaytelemarketingquality.Category,
                    Comments = demandaytelemarketingquality.Comments,
                    CampaignId = demandaytelemarketingquality.CampaignId,
                    QaStatus = demandaytelemarketingquality.QaStatus,
                    DeliveryStatus = demandaytelemarketingquality.DeliveryStatus,
                    AgentName = demandaytelemarketingquality.AgentName,
                    QaName = demandaytelemarketingquality.QaName,
                    CallDate = demandaytelemarketingquality.CallDate,
                    DateAudited = demandaytelemarketingquality.DateAudited,
                    DeliveryDate = demandaytelemarketingquality.DeliveryDate,
                    Source = demandaytelemarketingquality.Source,
                    VerificationMode = demandaytelemarketingquality.VerificationMode,
                    Asset1 = demandaytelemarketingquality.Asset1,
                    Asset2 = demandaytelemarketingquality.Asset2,
                    AgentsName = demandaytelemarketingquality.AgentsName,
                    TlName = demandaytelemarketingquality.TlName,
                    CompanyName = demandaytelemarketingquality.CompanyName,
                    FirstName = demandaytelemarketingquality.FirstName,
                    LastName = demandaytelemarketingquality.LastName,
                    Title = demandaytelemarketingquality.Title,
                    Email = demandaytelemarketingquality.Email,
                    WorkPhone = demandaytelemarketingquality.WorkPhone,
                    AlternativeNumber = demandaytelemarketingquality.AlternativeNumber,
                    Street = demandaytelemarketingquality.Street,
                    City = demandaytelemarketingquality.City,
                    State = demandaytelemarketingquality.State,
                    ZipCode = demandaytelemarketingquality.ZipCode,
                    Country = demandaytelemarketingquality.Country,
                    CompanyEmployeeSize = demandaytelemarketingquality.CompanyEmployeeSize,      // Mapping CompanyName to CompanyEmp
                    Industry = demandaytelemarketingquality.Industry,
                    ZoomInfoIndustry = demandaytelemarketingquality.ZoomInfoIndustry,
                    Date = demandaytelemarketingquality.Date,
                    ZoomInfoEmployeeSize = demandaytelemarketingquality.ZoomInfoEmployeeSize,
                    CallStatus = demandaytelemarketingquality.CallStatus,
                    AdditionalNotes = demandaytelemarketingquality.AdditionalNotes,
                    Asset = demandaytelemarketingquality.Asset,
                    Revenue = demandaytelemarketingquality.Revenue,
                    ProfileLink = demandaytelemarketingquality.ProfileLink,
                    CompanyLink = demandaytelemarketingquality.CompanyLink,
                    RevenueLink = demandaytelemarketingquality.RevenueLink,
                    AdressLink = demandaytelemarketingquality.AddressLink,     // Note: mapped AddressLink
                    Tenurity = demandaytelemarketingquality.Tenurity,
                    Code = demandaytelemarketingquality.Code,
                    Link = demandaytelemarketingquality.Link,
                    Md5 = demandaytelemarketingquality.Md5,
                    OwnerId = demandaytelemarketingquality.OwnerId
                };
                demandaytelemarketingmisConn.Insert(demandayTeleMarketingMIS);

                // Retrieve the auto-generated Id via @@IDENTITY
                using (var cmd = demandaytelemarketingmisConn.CreateCommand())
                {
                    cmd.CommandText = "SELECT CAST(@@IDENTITY AS INT)";
                    var newId = cmd.ExecuteScalar();
                    if (newId != null && newId != DBNull.Value)
                        demandayTeleMarketingMIS.Id = Convert.ToInt32(newId);
                }

                // Move QADetails from Quality to new MIS record
                var qaFlds = DemandayTeleMarketingEnquiryQADetailsRow.Fields;
                var qaDetails = demandaytelemarketingqualityConn.List<DemandayTeleMarketingEnquiryQADetailsRow>(q =>
                    q.Select(qaFlds.Id, qaFlds.EnquiryId, qaFlds.QuestionId, qaFlds.AnswerId)
                     .Where(qaFlds.EnquiryId == id));

                foreach (var qa in qaDetails)
                {
                    demandaytelemarketingqualityConn.Insert(new DemandayTeleMarketingEnquiryQADetailsRow
                    {
                        EnquiryId = demandayTeleMarketingMIS.Id,
                        QuestionId = qa.QuestionId,
                        AnswerId = qa.AnswerId
                    });
                    demandaytelemarketingqualityConn.DeleteById<DemandayTeleMarketingEnquiryQADetailsRow>(qa.Id.Value);
                }

                demandaytelemarketingqualityConn.DeleteById<DemandayTeleMarketingQualiltyRow>(id);
                response.Id = demandayTeleMarketingMIS.Id ?? 0;
            }
            response.Status = "Quality successfully moved to MIS module!";

            return response;
        }

        public class MoveToTeleMarketingMISRequest : ServiceRequest
        {
            public List<int> Ids { get; set; }
        }
        //public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
        //    [FromServices] IQualityListHandler handler,
        //    [FromServices] IExcelExporter exporter)
        //{
        //    var data = List(connection, request, handler).Entities;
        //    var bytes = exporter.Export(data, typeof(Columns.QualityColumns), request.ExportColumns);
        //    return ExcelContentResult.Create(bytes, "QualityList_" +
        //        DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
        //}
        [HttpPost, IgnoreAntiforgeryToken, AuthorizeList(typeof(DemandayTeleMarketingQualiltyRow))]
        public FileContentResult ListExcel(
        IDbConnection connection,
        [FromForm] ListRequest request, // Bind from form POSTs
        [FromForm] string Ids,
        [FromServices] IDemandayTeleMarketingQualiltyListHandler handler)
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

            // Ensure the "Created By" (OwnerUsername) column is populated. The join
            // expression is not reliably selected by the bare export ListRequest, so
            // resolve usernames directly from Users for any rows missing it.
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

                foreach (var rowItem in data)
                {
                    if (string.IsNullOrEmpty(rowItem.OwnerUsername) && rowItem.OwnerId.HasValue &&
                        userMap.TryGetValue(rowItem.OwnerId.Value, out var uname))
                        rowItem.OwnerUsername = uname;
                }
            }

            var bytes = AdvanceCRM.Web.Modules.Common.AppServices.DemandayTeleMarketingQualityExcelExporter.ExportToExcel(data);
            var fileName = "TeleMarketingQualityList_" + DateTime.Now.ToString("yyyyMMdd_HHmmss", System.Globalization.CultureInfo.InvariantCulture) + ".xlsx";
            return Serenity.Web.ExcelContentResult.Create(bytes, fileName);
        }
        [HttpPost, IgnoreAntiforgeryToken]
        [RequestSizeLimit(52428800)]
        [RequestFormLimits(MultipartBodyLengthLimit = 52428800)]
        public IActionResult ImportExcel([FromServices] IUnitOfWork uow, IFormFile file, [FromServices] IDemandayTeleMarketingQualiltySaveHandler saveHandler)
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
                    // (which is a username string) can be resolved back to OwnerId
                    // on import instead of erroring / losing the owner.
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
                            var demandaytmquality = new DemandayTeleMarketingQualiltyRow
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
                                Street = ExcelImportHelper.GetText(ws, row, map, "Street"),
                                City = ExcelImportHelper.GetText(ws, row, map, "City"),
                                State = ExcelImportHelper.GetText(ws, row, map, "State"),
                                ZipCode = ExcelImportHelper.GetText(ws, row, map, "ZipCode", "Zip Code"),
                                Country = ExcelImportHelper.GetText(ws, row, map, "Country"),
                                Date = ExcelImportHelper.GetDate(ws, row, map, "Date"),
                                AdditionalNotes = ExcelImportHelper.GetText(ws, row, map, "AdditionalNotes", "Additional Notes"),
                                CompanyEmployeeSize = ExcelImportHelper.GetText(ws, row, map, "CompanyEmployeeSize", "Company Employee Size"),
                                Industry = ExcelImportHelper.GetText(ws, row, map, "Industry"),
                                Revenue = ExcelImportHelper.GetText(ws, row, map, "Revenue"),
                                ProfileLink = ExcelImportHelper.GetText(ws, row, map, "ProfileLink", "Profile Link"),
                                CompanyLink = ExcelImportHelper.GetText(ws, row, map, "CompanyLink", "Company Link"),
                                RevenueLink = ExcelImportHelper.GetText(ws, row, map, "RevenueLink", "Revenue Link"),
                                EmailFormat = ExcelImportHelper.GetText(ws, row, map, "EmailFormat", "Email Format"),
                                AddressLink = ExcelImportHelper.GetText(ws, row, map, "AddressLink", "Address Link", "AdressLink", "Adress Link"),
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
                                OwnerId = ResolveOwnerId(ws, row, map, userByName),
                            };
                            if (demandaytmquality.Id.HasValue && demandaytmquality.Id.Value > 0)
                            {
                                skipped++; continue;
                            }
                            var creReq = new Serenity.Services.SaveRequest<DemandayTeleMarketingQualiltyRow> { Entity = demandaytmquality };
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
            System.Collections.Generic.Dictionary<string, int> map,
            System.Collections.Generic.Dictionary<string, int> userByName)
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

        private static int? GetInt(object val) { if (val == null) return null; int i; return int.TryParse(val.ToString(), out i) ? i : null; }
        private static decimal? GetDecimal(object val) { if (val == null) return null; decimal d; return decimal.TryParse(val.ToString(), out d) ? d : null; }
        private static DateTime? GetDate(object val) { if (val == null) return null; DateTime dt; return DateTime.TryParse(val.ToString(), out dt) ? dt : null; }
    }
}