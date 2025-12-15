using Microsoft.AspNetCore.Mvc;
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
            var demandaycontactsConn = sqlConnections.NewFor<DemandayContactsRow>();

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

                var demandaycontacts = new DemandayContactsRow
                {
                    Slot = demandayquality.Slot,
                    PrimaryReason = demandayquality.PrimaryReason,
                    Category = demandayquality.Category,
                    Comments = demandayquality.Comments,
                    QaStatus = demandayquality.QaStatus,
                    DeliveryStatus = demandayquality.DeliveryStatus,
                    AgentName = demandayquality.AgentName,
                    QaName = demandayquality.QaName,
                    CallDate = demandayquality.CallDate,
                    DateAudited = demandayquality.DateAudited,
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

                demandaycontactsConn.Insert(demandaycontacts);

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
            var bytes = AdvanceCRM.Web.Modules.Common.AppServices.DemandayQualityExcelExporter.ExportToExcel(data);
            var fileName = "QualityList_" + DateTime.Now.ToString("yyyyMMdd_HHmmss", System.Globalization.CultureInfo.InvariantCulture) + ".xlsx";
            return Serenity.Web.ExcelContentResult.Create(bytes, fileName);
        }
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
            [FromServices] IDemandayQualityListHandler handler,
            [FromServices] IExcelExporter exporter)
        {
            var data = List(connection, request, handler).Entities;
            var bytes = exporter.Export(data, typeof(Columns.DemandayQualityColumns), request.ExportColumns);
            return ExcelContentResult.Create(bytes, "DemandayQualityList_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
        }
    }
}