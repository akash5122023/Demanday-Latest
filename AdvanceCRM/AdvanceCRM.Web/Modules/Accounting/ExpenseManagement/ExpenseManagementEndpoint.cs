
namespace AdvanceCRM.Accounting.Endpoints
{
    using AdvanceCRM.Accounting;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.Data;
    
    using MyRepository = Repositories.ExpenseManagementRepository;
    using MyRow =ExpenseManagementRow;

    [Route("Services/Accounting/ExpenseManagement/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class ExpenseManagementController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;

        public ExpenseManagementController(ISqlConnections connections)
        {
            _connections = connections;
        }
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

        [ServiceAuthorize("ExpenseManagement:Can Approve")]
        public StandardResponse Approve(SendSMSRequest request)
        {
            var response = new StandardResponse();

            try
            {
                var connection = _connections.NewByKey("Default");
                connection.Execute("UPDATE ExpenseManagement SET ApprovedBy=" + Convert.ToInt32(Context.User.GetIdentifier()) + "WHERE Id=" + request.Id);

                var em = ExpenseManagementRow.Fields;
                var data = connection.TryById<ExpenseManagementRow>(request.Id, q => q
                        .SelectTableFields()
                        .Select(em.Id)
                        .Select(em.HeadId)
                        .Select(em.Amount)
                        .Select(em.AdditionalInfo)
                        .Select(em.RepresentativeDisplayName)
                    );

                connection.Execute("INSERT INTO Cashbook(Date,Type,Head,CashOut,Narration) VALUES('" + System.DateTime.Now.ToString("yyyy-MM-dd") + "'," + 2 + "," + data.HeadId + "," + data.Amount + ",'" + data.AdditionalInfo + ", For:" + data.RepresentativeDisplayName + " - Expense Management Id:  " + data.Id + "')");


                //IUnitOfWork uow = null;
                //SaveRequest<CashbookRow> request1 = new SaveRequest<CashbookRow>();
                //request1.Entity.Date = DateTime.Now;
                //request1.Entity.Type = (Masters.TransactionTypeMaster)2;
                //request1.Entity.Head = data.HeadId;
                //request1.Entity.CashOut = data.Amount;
                //request1.Entity.Narration = data.AdditionalInfo;

                //CashbookController obj = new CashbookController();
                //obj.Create(uow, request1);

                response.Status = "Approved";
            }
            catch (Exception ex)
            {
                response.Status = ex.Message;
            }

            return response;
        }

        [ServiceAuthorize("ExpenseManagement:Export")]
        public Microsoft.AspNetCore.Mvc.FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.ExpenseManagementColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "ExpenseManagement_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }
        
    }
}
