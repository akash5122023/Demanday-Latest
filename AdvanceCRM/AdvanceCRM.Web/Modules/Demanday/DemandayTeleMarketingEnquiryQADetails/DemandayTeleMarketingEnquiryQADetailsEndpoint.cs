using Microsoft.AspNetCore.Mvc;
using Serenity;
using Serenity.Data;
using Serenity.Services;
using Serenity.Web;
using System;
using System.Data;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingEnquiryQADetailsRow;

namespace AdvanceCRM.Demanday.Endpoints
{
    [Route("Services/Demanday/DemandayTeleMarketingEnquiryQADetails/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class DemandayTeleMarketingEnquiryQADetailsController : ServiceEndpoint
    {
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayTeleMarketingEnquiryQADetailsSaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayTeleMarketingEnquiryQADetailsSaveHandler handler)
        {
            return handler.Update(uow, request);
        }

        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] IDemandayTeleMarketingEnquiryQADetailsDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] IDemandayTeleMarketingEnquiryQADetailsRetrieveHandler handler)
        {
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] IDemandayTeleMarketingEnquiryQADetailsListHandler handler)
        {
            return handler.List(connection, request);
        }

        public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
            [FromServices] IDemandayTeleMarketingEnquiryQADetailsListHandler handler,
            [FromServices] Serenity.Reporting.IExcelExporter exporter)
        {
            var data = List(connection, request, handler).Entities;
            var bytes = exporter.Export(data, typeof(AdvanceCRM.Demanday.Columns.DemandayTeleMarketingEnquiryQADetailsColumns), request.ExportColumns);
            return ExcelContentResult.Create(bytes, "DemandayTeleMarketingEnquiryQADetailsList_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", System.Globalization.CultureInfo.InvariantCulture) + ".xlsx");
        }
    }
}
