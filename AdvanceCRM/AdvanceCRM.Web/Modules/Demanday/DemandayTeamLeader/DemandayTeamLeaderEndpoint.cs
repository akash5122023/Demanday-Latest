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
            if (request?.Ids == null)
                throw new ArgumentNullException(nameof(request.Ids));

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
                    CompanyName = demandayteamleader.CompanyName,
                    FirstName = demandayteamleader.FirstName,
                    LastName = demandayteamleader.LastName,
                    Title = demandayteamleader.Title,
                    Email = demandayteamleader.Email,
                    WorkPhone = demandayteamleader.WorkPhone,
                    AlternativeNumber = demandayteamleader.AlternativeNumber,
                    Street = demandayteamleader.Street,
                    City = demandayteamleader.City,
                    State = demandayteamleader.State,
                    ZipCode = demandayteamleader.ZipCode,
                    Country = demandayteamleader.Country,
                    Industry = demandayteamleader.Industry,
                    Revenue = demandayteamleader.Revenue,
                    CompanyEmployeeSize = demandayteamleader.CompanyEmployeeSize,
                    ProfileLink = demandayteamleader.ProfileLink,
                    CompanyLink = demandayteamleader.CompanyLink,
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
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
            [FromServices] IDemandayTeamLeaderListHandler handler,
            [FromServices] IExcelExporter exporter)
        {
            var data = List(connection, request, handler).Entities;
            var bytes = exporter.Export(data, typeof(Columns.DemandayTeamLeaderColumns), request.ExportColumns);
            return ExcelContentResult.Create(bytes, "DemandayTeamLeaderList_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
        }
    }
}