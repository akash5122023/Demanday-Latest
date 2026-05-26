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
using MyRow = AdvanceCRM.Demanday.DemandayQualityRow;

namespace AdvanceCRM.Demanday.Endpoints
{
    [Route("Services/Demanday/DemandayQuality/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class DemandayQualityController : ServiceEndpoint
    {
        private readonly ISqlConnections sqlConnections;
        public DemandayQualityController(ISqlConnections sqlConnections)
        {
            this.sqlConnections = sqlConnections;
        }
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayQualitySaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayQualitySaveHandler handler)
        {
            return handler.Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] IDemandayQualityDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] IDemandayQualityRetrieveHandler handler)
        {
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] IDemandayQualityListHandler handler)
        {
            return handler.List(connection, request);
        }
        [HttpPost, AuthorizeUpdate(typeof(DemandayQualityRow))]
        public StandardResponse MoveToMIS(IUnitOfWork uow, MoveToMISRequest request)
        {
            if (request?.Ids == null)
                throw new ArgumentNullException(nameof(request.Ids));

            var response = new StandardResponse();
            var demandayqualityConn = sqlConnections.NewFor<DemandayQualityRow>();
            var demandaymisConn = sqlConnections.NewFor<DemandayMisRow>();

            foreach (var id in request.Ids)
            {
                var demandayquality = demandayqualityConn.TryById<DemandayQualityRow>(id);

                if (demandayquality == null)
                    throw new ValidationError("Quality record not found!");


                var demandayMIS = new DemandayMisRow
                {

                    // Map fields that exist in EnquiryRow

                    Slot = demandayquality.Slot,
                    PrimaryReason = demandayquality.PrimaryReason,
                    Category = demandayquality.Category,
                    Comments = demandayquality.Comments,
                    QaStatus = demandayquality.QaStatus,
                    CampaignId = demandayquality.CampaignId,
                    DeliveryStatus = demandayquality.DeliveryStatus,
                    AgentName = demandayquality.AgentName,
                    QaName = demandayquality.QaName,
                    CallDate = demandayquality.CallDate,
                    DateAudited =       demandayquality.DateAudited,
                    DeliveryDate = demandayquality.DeliveryDate,
                    Source = demandayquality.Source,
                    VerificationMode = demandayquality.VerificationMode,
                    Asset1 = demandayquality.Asset1,
                    Asset2 = demandayquality.Asset2,
                    AgentsName = demandayquality.AgentsName,
                    TlName = demandayquality.TlName,
                    CompanyName = demandayquality.CompanyName,
                    FirstName = demandayquality.FirstName,
                    LastName = demandayquality.LastName,
                    Title = demandayquality.Title,
                    Email = demandayquality.Email,
                    WorkPhone = demandayquality.WorkPhone,
                    AlternativeNumber = demandayquality.AlternativeNumber,
                    Street = demandayquality.Street,
                    City = demandayquality.City,
                    State = demandayquality.State,
                    ZipCode = demandayquality.ZipCode,
                    Country = demandayquality.Country,
                    CompanyEmployeeSize = demandayquality.CompanyEmployeeSize,      // Mapping CompanyName to CompanyEmp
                    Industry = demandayquality.Industry,
                    ZoomInfoIndustry = demandayquality.ZoomInfoIndustry,
                    Date = demandayquality.Date,
                    ZoomInfoEmployeeSize = demandayquality.ZoomInfoEmployeeSize,
                    Revenue = demandayquality.Revenue,
                    ProfileLink = demandayquality.ProfileLink,
                    CompanyLink = demandayquality.CompanyLink,
                    RevenueLink = demandayquality.RevenueLink,
                    EmailFormat = demandayquality.EmailFormat,
                    AdressLink = demandayquality.AdressLink,     // Note: mapped AddressLink
                    Tenurity = demandayquality.Tenurity,
                    Code = demandayquality.Code,
                    Link = demandayquality.Link,
                    Md5 = demandayquality.Md5,
                    OwnerId = demandayquality.OwnerId
                };
                demandaymisConn.Insert(demandayMIS);

                demandayqualityConn.DeleteById<DemandayQualityRow>(id);
                response.Id = demandayMIS.Id ?? 0;
            }
            response.Status = "Quality successfully moved to MIS module!";

            return response;
        }

        public class MoveToMISRequest : ServiceRequest
        {
            public List<int> Ids { get; set; }
        }
        [HttpPost, IgnoreAntiforgeryToken, AuthorizeList(typeof(DemandayQualityRow))]
        public FileContentResult ListExcel(
        IDbConnection connection,
        [FromForm] ListRequest request, // Bind from form POSTs
        [FromForm] string Ids,
        [FromServices] IDemandayQualityListHandler handler)
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

                foreach (var row in data)
                {
                    if (string.IsNullOrEmpty(row.OwnerUsername) && row.OwnerId.HasValue &&
                        userMap.TryGetValue(row.OwnerId.Value, out var uname))
                        row.OwnerUsername = uname;
                }
            }

            var bytes = AdvanceCRM.Web.Modules.Common.AppServices.DemandayQualityExcelExporter.ExportToExcel(data);
            var fileName = "QualityList_" + DateTime.Now.ToString("yyyyMMdd_HHmmss", System.Globalization.CultureInfo.InvariantCulture) + ".xlsx";
            return Serenity.Web.ExcelContentResult.Create(bytes, fileName);
        }
        [HttpPost, IgnoreAntiforgeryToken]
        [RequestSizeLimit(52428800)]
        [RequestFormLimits(MultipartBodyLengthLimit = 52428800)]
        public IActionResult ImportExcel([FromServices] IUnitOfWork uow, IFormFile file, [FromServices] IDemandayQualitySaveHandler saveHandler)
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
                            var demandayquality = new DemandayQualityRow
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
                                Date = ExcelImportHelper.GetDate(ws, row, map, "Date"),
                                State = ExcelImportHelper.GetText(ws, row, map, "State"),
                                ZipCode = ExcelImportHelper.GetText(ws, row, map, "ZipCode", "Zip Code"),
                                Country = ExcelImportHelper.GetText(ws, row, map, "Country"),
                                CompanyEmployeeSize = ExcelImportHelper.GetText(ws, row, map, "CompanyEmployeeSize", "Company Employee Size"),
                                Industry = ExcelImportHelper.GetText(ws, row, map, "Industry"),
                                Revenue = ExcelImportHelper.GetText(ws, row, map, "Revenue"),
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
                                OwnerId = ResolveOwnerId(ws, row, map, userByName),
                            };
                            if (demandayquality.Id.HasValue && demandayquality.Id.Value > 0)
                            {
                                skipped++; continue;
                            }
                            var creReq = new Serenity.Services.SaveRequest<DemandayQualityRow> { Entity = demandayquality };
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
       // public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
        //    [FromServices] IDemandayQualityListHandler handler,
        //    [FromServices] IExcelExporter exporter)
        //{
         //   var data = List(connection, request, handler).Entities;
         //   var bytes = exporter.Export(data, typeof(Columns.DemandayQualityColumns), request.ExportColumns);
          //  return ExcelContentResult.Create(bytes, "DemandayQualityList_" +
          //      DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
        //}
    }
}