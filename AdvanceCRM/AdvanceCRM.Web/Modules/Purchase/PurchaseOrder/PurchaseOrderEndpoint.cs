
namespace AdvanceCRM.Purchase.Endpoints
{
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Common;
    using AdvanceCRM.Template;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.Data;
    
    using MyRepository = Repositories.PurchaseOrderRepository;
    using MyRow = PurchaseOrderRow;

    [Route("Services/Purchase/PurchaseOrder/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class PurchaseOrderController : ServiceEndpoint
    {
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context).Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context).Update(uow, request);
        }

        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
           return new MyRepository(Context).Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
             return new MyRepository(Context).Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
            return new MyRepository(Context).List(connection, request);
        }


        [ServiceAuthorize("PurchaseOrder:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.PurchaseOrderColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "PO_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        public GetNextNumberResponse GetNextNumber(IDbConnection connection, GetNextNumberRequest request)
        {
            var response = new GetNextNumberResponse();
            response.Serial = "1";
            try
            {
                var sl = MyRow.Fields;
                var data = new MyRow();


                data = connection.TryFirst<MyRow>(q => q
                    .SelectTableFields()
                    .Select(sl.Id)
                    .Select(sl.PurchaseOrderNo)
                    .OrderBy(sl.Id, desc: true)
                    );

                if (data != null)
                    response.Serial = (data.PurchaseOrderNo + 1).ToString();
            }
            catch (Exception)
            {

                return null;
            }

            return response;
        }
    }
}
