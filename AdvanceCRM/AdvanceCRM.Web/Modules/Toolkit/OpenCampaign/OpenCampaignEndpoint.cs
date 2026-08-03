using Microsoft.AspNetCore.Mvc;
using Serenity;
using Serenity.Data;
using Serenity.Reporting;
using Serenity.Services;
using Serenity.Web;
using AdvanceCRM.Web.Helpers;
using System;
using System.Data;
using System.Globalization;
using MyRow = AdvanceCRM.Toolkit.OpenCampaignRow;

namespace AdvanceCRM.Toolkit.Endpoints
{
    [Route("Services/Toolkit/OpenCampaign/[action]")]
    [ConnectionKey(typeof(MyRow))]
    public class OpenCampaignController : ServiceEndpoint
    {
        private void CheckReadPermission()
        {
            if (!Authorization.HasPermission("OpenCampaign:Read") &&
                !Authorization.HasPermission("Toolkit:VerifySheets") &&
                !Authorization.HasPermission("Toolkit:VerifySheets:OpenCampaign"))
                throw new ValidationError("Access denied");
        }

        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IOpenCampaignSaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IOpenCampaignSaveHandler handler)
        {
            return handler.Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] IOpenCampaignDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] IOpenCampaignRetrieveHandler handler)
        {
            CheckReadPermission();
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] IOpenCampaignListHandler handler)
        {
            CheckReadPermission();
            return handler.List(connection, request);
        }

        [HttpPost]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
            [FromServices] IOpenCampaignListHandler handler,
            [FromServices] IExcelExporter exporter)
        {
            CheckReadPermission();
            var data = List(connection, request, handler).Entities;
            var bytes = exporter.Export(data, typeof(Columns.OpenCampaignColumns), request.ExportColumns);
            return ExcelContentResult.Create(bytes, "OpenCampaignList_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
        }
    }
}