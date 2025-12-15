
namespace AdvanceCRM.Purchase.Endpoints
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Purchase;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.Data;
    using Microsoft.Extensions.DependencyInjection;
    
    using MyRepository = Repositories.PurchaseRepository;
    using MyRow = PurchaseRow;
    using Serenity.Extensions.DependencyInjection;

    [Route("Services/Purchase/Purchase/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class PurchaseController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;

        [ActivatorUtilitiesConstructor]
        public PurchaseController(ISqlConnections connections, IRequestContext context)
        {
            _connections = connections;
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public PurchaseController(ISqlConnections connections)
            : this(connections, Dependency.Resolve<IRequestContext>())
        {
        }

        public PurchaseController() : this(Dependency.Resolve<ISqlConnections>(), Dependency.Resolve<IRequestContext>())
        {
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

        [ServiceAuthorize("Purchase:Can Approve")]
        public StandardResponse Approve(SendSMSRequest request)
        {
            var response = new StandardResponse();

            try
            {
                var connection = _connections.NewByKey("Default");
                connection.Execute("UPDATE Purchase SET ApprovedBy=" + Convert.ToInt32(Context.User.GetIdentifier()) + "WHERE Id=" + request.Id);

                var em = PurchaseRow.Fields;
                //var data = connection.TryById<CashbookRow>(request.Id, q => q
                //        .SelectTableFields()
                //        .Select(em.Id)
                //        .Select(em.Head)
                //        .Select(em.CashIn)
                //        .Select(em.CashOut)
                //        .Select(em.ContactsName)
                //        .Select(em.EmployeeName)
                //        .Select(em.RepresentativeDisplayName)


                //    );

                //connection.Execute("INSERT INTO Cashbook(Date,Type,Head,CashOut,Narration) VALUES('" + System.DateTime.Now.ToString("yyyy-MM-dd") + "'," + 2 + "," + data.Head + "," + data.CashIn + ",'" + data.CashOut + ", For:" + data.RepresentativeDisplayName + " - Cashbook Id:  " + data.Id + "')");



                response.Status = "Approved";
            }
            catch (Exception ex)
            {
                response.Status = ex.Message;
            }

            return response;
        }

        [ServiceAuthorize("Purchase:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.PurchaseColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "Purchase_" +
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
                var br = UserRow.Fields;
                var UData = new UserRow();

                UData = connection.First<UserRow>(q => q
                 .SelectTableFields()
                 .Select(br.CompanyId)
                 .Where(br.UserId == Context.User.GetIdentifier())
                );

                var br1 = CompanyDetailsRow.Fields;
                var Bdata = new CompanyDetailsRow();
                Bdata = connection.First<CompanyDetailsRow>(q => q
                  .SelectTableFields()
                  .Select(br1.InvoicePrefix)
                  .Select(br1.InvoiceSuffix)
                  .Select(br1.YearInPrefix)
                  .Select(br1.InvStartNo)
                    .Where(br1.Id == Convert.ToInt32(UData.CompanyId))
                 );

                // if (request.BranchId.Trim() != "")
                {
                    data = connection.TryFirst<MyRow>(q => q
                        .SelectTableFields()
                        .Select(sl.Id)
                        .Select(sl.InvoiceNo)
                         .Where(sl.CompanyId == Convert.ToInt32(UData.CompanyId))
                        .OrderBy(sl.Id, desc: true)
                        );
                }


                var Ypre = string.Empty;
                //int month = 2;
                int month = Convert.ToInt32(DateTime.Now.Month);
                var year = Convert.ToString(DateTime.Now.Year);
                // Ypre = year;

                if (Bdata.YearInPrefix == AdvanceCRM.Masters.YearInPrefix.Year)
                {
                    Ypre = year;
                }
                else if (Bdata.YearInPrefix == AdvanceCRM.Masters.YearInPrefix.FinacialYear)
                {
                    var yearF = year.Substring(year.Length - 2);
                    if (month >= 4)
                    {
                        var year1 = Convert.ToString(Convert.ToInt32(yearF) + 1);
                        Ypre = yearF + "" + year1;
                    }
                    else
                    {
                        var year1 = Convert.ToString(Convert.ToInt32(yearF) - 1);
                        Ypre = year1 + "" + yearF;
                    }
                }


                string stPre = string.Empty;
                string stsuf = string.Empty;
                if (Bdata.InvoicePrefix != null)
                {
                    stPre = Bdata.InvoicePrefix;
                }
                if (Bdata.InvoiceSuffix != null)
                {
                    stsuf = Bdata.InvoiceSuffix;
                }

                if (Bdata.InvStartNo == null)
                {
                    if (data != null)
                    {

                        response.SerialN = stPre + "" + Ypre + "" + (data.InvoiceNo + 1).ToString() + "" + stsuf;

                        response.Serial = (data.InvoiceNo + 1).ToString();
                    }
                    else
                    {
                        response.SerialN = stPre + "" + Ypre + "" + (1).ToString() + "" + stsuf;

                        response.Serial = (1).ToString();
                    }
                }
                else
                {
                    if (data != null)
                    {

                        response.SerialN = stPre + "" + Ypre + "" + (Bdata.InvStartNo).ToString() + "" + stsuf;

                        response.Serial = (Bdata.InvStartNo).ToString();
                    }
                    else
                    {
                        response.SerialN = stPre + "" + Ypre + "" + (Bdata.InvStartNo).ToString() + "" + stsuf;

                        response.Serial = (Bdata.InvStartNo).ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                return null;
            }

          
            return response;
        }

    }
}