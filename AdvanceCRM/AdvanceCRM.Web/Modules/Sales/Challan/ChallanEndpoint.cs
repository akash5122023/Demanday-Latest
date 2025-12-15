
namespace AdvanceCRM.Sales.Endpoints
{
    using AdvanceCRM.Sales;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.Data;
    
    using MyRepository = Repositories.ChallanRepository;
    using MyRow = ChallanRow;

    [Route("Services/Sales/Challan/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize("Sales:Challan")]
    public class ChallanController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;
        private IRequestContext Context { get; }

        public ChallanController(ISqlConnections connections, IRequestContext context)
        {
            _connections = connections;
            Context = context;
        }
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context, _connections).Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context, _connections).Update(uow, request);
        }

        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
           return new MyRepository(Context, _connections).Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
             return new MyRepository(Context, _connections).Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
            return new MyRepository(Context, _connections).List(connection, request);
        }


        [ServiceAuthorize("Challan:Can Approve")]
        public StandardResponse Approve(SendSMSRequest request)
        {
            var response = new StandardResponse();

            try
            {
                using var connection = _connections.NewFor<ChallanRow>();
                const string sql = "UPDATE Challan SET ApprovedBy=@UserId WHERE Id=@Id";
                connection.Execute(sql, new { UserId = Convert.ToInt32(Context.User.GetIdentifier()), Id = request.Id });

                response.Status = "Approved";
            }
            catch (Exception ex)
            {
                response.Status = ex.Message;
            }

            return response;
        }



        [ServiceAuthorize("Challan:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.ChallanColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "Challan_" +
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
