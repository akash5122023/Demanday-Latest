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
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingTeamLeaderRow;

namespace AdvanceCRM.Demanday.Endpoints
{
    [Route("Services/Demanday/DemandayTeleMarketingTeamLeader/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class DemandayTeleMarketingTeamLeaderController : ServiceEndpoint
    {
        private readonly ISqlConnections sqlConnections;

        public DemandayTeleMarketingTeamLeaderController(ISqlConnections sqlConnections)
        {
            this.sqlConnections = sqlConnections;
        }
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayTeleMarketingTeamLeaderSaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayTeleMarketingTeamLeaderSaveHandler handler)
        {
            return handler.Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] IDemandayTeleMarketingTeamLeaderDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] IDemandayTeleMarketingTeamLeaderRetrieveHandler handler)
        {
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] IDemandayTeleMarketingTeamLeaderListHandler handler)
        {
            return handler.List(connection, request);
        }

        public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
            [FromServices] IDemandayTeleMarketingTeamLeaderListHandler handler,
            [FromServices] IExcelExporter exporter)
        {
            var data = List(connection, request, handler).Entities;
            var bytes = exporter.Export(data, typeof(Columns.DemandayTeleMarketingTeamLeaderColumns), request.ExportColumns);
            return ExcelContentResult.Create(bytes, "DemandayTeleMarketingTeamLeaderList_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
        }
        [HttpPost, AuthorizeUpdate(typeof(DemandayTeleMarketingTeamLeaderRow))]
        public StandardResponse MoveToQuality(IUnitOfWork uow, MoveToQualityRequest request)
        {
            if (request?.Ids == null)
                throw new ArgumentNullException(nameof(request.Ids));

            var response = new StandardResponse();

            // 1️⃣ Get the Enquiry record
            var demandaytelemarketingteamleaderConn = sqlConnections.NewFor<DemandayTeleMarketingTeamLeaderRow>();
            var demandaytelemarketingqualityConn = sqlConnections.NewFor<DemandayTeleMarketingQualiltyRow>();
            //var telemarketingteamConn = sqlConnections.NewFor<TeleMarketingTeammRow>();



            foreach (var id in request.Ids)
            {
                var demandaytelemarketingteamleader = demandaytelemarketingteamleaderConn.TryById<DemandayTeleMarketingTeamLeaderRow>(id);
                if (demandaytelemarketingteamleader == null)
                    throw new ValidationError("Enquiry record not found!");
                // 2️⃣ Prepare a new QualityRow with mapped data
                var demandaytelemarketingqualilty = new DemandayTeleMarketingQualiltyRow
                {
                    // Map fields that exist in EnquiryRow
                    //TlName = enquiry.TlName,
                    //CampaignId = enquiry.CampaignId,
                    CompanyName = demandaytelemarketingteamleader.CompanyName,
                    FirstName = demandaytelemarketingteamleader.FirstName,
                    LastName = demandaytelemarketingteamleader.LastName,
                    Title = demandaytelemarketingteamleader.Title,
                    Email = demandaytelemarketingteamleader.Email,
                    WorkPhone = demandaytelemarketingteamleader.WorkPhone,
                    AlternativeNumber = demandaytelemarketingteamleader.AlternativeNumber,
                    Street = demandaytelemarketingteamleader.Street,
                    City = demandaytelemarketingteamleader.City,
                    State = demandaytelemarketingteamleader.State,
                    ZipCode = demandaytelemarketingteamleader.ZipCode,
                    Country = demandaytelemarketingteamleader.Country,
                    //CompanyEmp = teamleader.CompanyName,      // Mapping CompanyName to CompanyEmp
                    Industry = demandaytelemarketingteamleader.Industry,
                    Revenue = demandaytelemarketingteamleader.Revenue,
                    CompanyEmployeeSize = demandaytelemarketingteamleader.CompanyEmployeeSize,
                    ProfileLink = demandaytelemarketingteamleader.ProfileLink,
                    CompanyLink = demandaytelemarketingteamleader.CompanyLink,
                    RevenueLink = demandaytelemarketingteamleader.RevenueLink,
                    AddressLink = demandaytelemarketingteamleader.AddressLink,     // Note: mapped AddressLink
                    Tenurity = demandaytelemarketingteamleader.Tenurity,
                    Code = demandaytelemarketingteamleader.Code,
                    Md5 = demandaytelemarketingteamleader.Md5,
                    OwnerId = demandaytelemarketingteamleader.OwnerId
                };

                // 3️⃣ Insert into Quality table

                demandaytelemarketingqualityConn.Insert(demandaytelemarketingqualilty);

                demandaytelemarketingteamleaderConn.DeleteById<DemandayTeleMarketingTeamLeaderRow>(id);

                response.Id = demandaytelemarketingqualilty.Id ?? 0; // optional: return the new Quality record ID
            }
            response.Status = "Enquiry successfully moved to Quality module!";

            return response;
        }
        public class MoveToQualityRequest : ServiceRequest
        {
            public List<int> Ids { get; set; }
        }        
    }
}