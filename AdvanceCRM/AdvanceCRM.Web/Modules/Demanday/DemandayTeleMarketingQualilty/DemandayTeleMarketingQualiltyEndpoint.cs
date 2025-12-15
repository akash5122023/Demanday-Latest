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
            var demandaytelemarketingcontactsConn = sqlConnections.NewFor<DemandayTeleMarketingContactsRow>();

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

                var demandaytelemarketingcontacts = new DemandayTeleMarketingContactsRow
                {
                    Slot = demandaytelemarketingquality.Slot,
                    PrimaryReason = demandaytelemarketingquality.PrimaryReason,
                    Category = demandaytelemarketingquality.Category,
                    Comments = demandaytelemarketingquality.Comments,
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

                demandaytelemarketingcontactsConn.Insert(demandaytelemarketingcontacts);

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
            var bytes = AdvanceCRM.Web.Modules.Common.AppServices.DemandayTeleMarketingQualityExcelExporter.ExportToExcel(data);
            var fileName = "TeleMarketingQualityList_" + DateTime.Now.ToString("yyyyMMdd_HHmmss", System.Globalization.CultureInfo.InvariantCulture) + ".xlsx";
            return Serenity.Web.ExcelContentResult.Create(bytes, fileName);
        }
    }
}