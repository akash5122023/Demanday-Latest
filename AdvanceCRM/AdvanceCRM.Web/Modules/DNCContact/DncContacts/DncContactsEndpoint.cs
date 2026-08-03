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
using MyRow = AdvanceCRM.DNCContact.DncContactsRow;

namespace AdvanceCRM.DNCContact.Endpoints
{
    [Route("Services/DNCContact/DncContacts/[action]")]
    [ConnectionKey(typeof(MyRow))]
    public class DncContactsController : ServiceEndpoint
    {
        private void CheckReadPermission()
        {
            if (!Authorization.HasPermission("DNCContacts:Read") &&
                !Authorization.HasPermission("Toolkit:VerifySheets") &&
                !Authorization.HasPermission("Toolkit:VerifySheets:DNCContact"))
                throw new ValidationError("Access denied");
        }

        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDncContactsSaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDncContactsSaveHandler handler)
        {
            return handler.Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] IDncContactsDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] IDncContactsRetrieveHandler handler)
        {
            CheckReadPermission();
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] IDncContactsListHandler handler)
        {
            CheckReadPermission();
            return handler.List(connection, request);
        }

        [HttpPost]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
            [FromServices] IDncContactsListHandler handler,
            [FromServices] IExcelExporter exporter)
        {
            CheckReadPermission();
            var data = List(connection, request, handler).Entities;
            var bytes = exporter.Export(data, typeof(Columns.DncContactsColumns), request.ExportColumns);
            return ExcelContentResult.Create(bytes, "DncContactsList_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
        }
    }
}