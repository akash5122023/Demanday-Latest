using Microsoft.AspNetCore.Mvc;
using Serenity;
using Serenity.Data;
using Serenity.Reporting;
using Serenity.Services;
using Serenity.Web;
using System;
using System.Data;
using System.Globalization;
using MyRow = AdvanceCRM.Sales.InwardRow;

namespace AdvanceCRM.Sales.Endpoints
{
    [Route("Services/Sales/Inward/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class InwardController : ServiceEndpoint
    {
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IInwardSaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IInwardSaveHandler handler)
        {
            return handler.Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] IInwardDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] IInwardRetrieveHandler handler)
        {
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] IInwardListHandler handler)
        {
            return handler.List(connection, request);
        }
        [ServiceAuthorize("Inward:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
            [FromServices] IInwardListHandler handler,
            [FromServices] IExcelExporter exporter)
        {
            var data = List(connection, request, handler).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.InwardColumns));
            var bytes = exporter.Export(data, typeof(Columns.InwardColumns), request.ExportColumns);
            return ExcelContentResult.Create(bytes, "InwardList_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
        }
        public GetNextNumberResponse GetNextNumber(IDbConnection connection, GetNextNumberRequest request)
        {
            var response = new GetNextNumberResponse();
            response.Serial = "1";
            try
            {
                var sl = MyRow.Fields;
                var data = new MyRow();
                data = connection.First<MyRow>(q => q
                    .SelectTableFields()
                    .Select(sl.Id)
                    .Select(sl.ChallanNo)
                    .OrderBy(sl.Id, desc: true)
                    );

                if (data != null)
                    response.Serial = (data.ChallanNo + 1).ToString();
            }
            catch (Exception)
            {

                return null;
            }

            return response;
        }
    }
}