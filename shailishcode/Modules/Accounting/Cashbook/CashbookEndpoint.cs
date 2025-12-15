
namespace AdvanceCRM.Accounting.Endpoints
{
    using Serenity;
    using Serenity.Data;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.Linq;
    using System.Data;
    using Microsoft.AspNetCore.Mvc;
    using MyRepository = Repositories.CashbookRepository;
    using MyRow =CashbookRow;
    using AdvanceCRM.Contacts;
    using System.Collections.Generic;
    using AdvanceCRM.Sales;
    using AdvanceCRM.Purchase;
    using AdvanceCRM.Accounting;

    [Route("Services/Accounting/Cashbook/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class CashbookController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;

        public CashbookController(ISqlConnections connections)
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

        //Send SMS for SMS Sender
        [HttpPost]
        public StandardResponse GetOutstandingBalance(IUnitOfWork uow, StandardRequest request)
        {
            var response = new StandardResponse();

            //var data = new CashbookReportData();

            List<CashbookRow> Cash;
            List<SalesRow> Cust;            
            var m = CashbookRow.Fields;
            var c = SalesRow.Fields;
            var p = PurchaseRow.Fields;
            var cnt = ContactsRow.Fields;

            try
            {
                using (var connection = _connections.NewFor<CashbookRow>())
                {
                    Cash = connection.List<CashbookRow>(q => q
                     .SelectTableFields()
                     .Select(m.Date)
                     .Select(m.CashIn)
                     .Select(m.ContactsName)
                     .Where(m.Type == 1)
                     .Where(m.Head == 1)
                     .Where(m.ContactsId == request.Id)
                     );

                    Cust = connection.List<SalesRow>(q => q
                     .SelectTableFields()
                     .Select(c.Date)
                     //.Select(c.Total)
                     .Select(c.Advacne)
                     .Select(c.GrandTotal)
                     .Select(c.ContactsName)
                     .Select(c.ContactsDebtorsOpening)
                     .Select(c.ContactsCreditorsOpening)
                     //.Where(c.Type == 2)
                     .Where(c.ContactsId == request.Id)
                     );
                }

                double t_tot = 0;
              
                double debtorsopening = 0;
            
                //Debit
                if (Cust.Count > 0)
                {
                    if (Cust.Where(x => x.ContactsId == request.Id).FirstOrDefault().ContactsDebtorsOpening.HasValue)
                    {
                        debtorsopening = (double)Cust.Where(x => x.ContactsId == request.Id).FirstOrDefault().ContactsDebtorsOpening;
                    }
                }

                t_tot = ((double)Cust.Where(y => y.ContactsId == request.Id).Sum(x => x.GrandTotal)+ (double)Cust.Where(y => y.ContactsId == request.Id).Sum(x => x.Advacne)) - (double)Cash.Where(y => y.ContactsId == request.Id).Sum(x => x.CashIn) + debtorsopening;

                // Credits
             

                response.Status = t_tot.ToString("#,##0.00");
            }
            catch (Exception ex)
            {
                response.Status = "Error " + ex.Message.ToString();
            }


            return response;
        }


        [HttpPost]
        public StandardResponse GetOutstandingCreditBalance(IUnitOfWork uow, StandardRequest request)
        {
            var response = new StandardResponse();

            //var data = new CashbookReportData();

            List<CashbookRow> Cash;
          
            List<PurchaseRow> Sup;
            var m = CashbookRow.Fields;
            var c = SalesRow.Fields;
            var p = PurchaseRow.Fields;
            var cnt = ContactsRow.Fields;

            try
            {
                using (var connection = _connections.NewFor<CashbookRow>())
                {
                    Cash = connection.List<CashbookRow>(q => q
                     .SelectTableFields()
                     .Select(m.Date)
                     .Select(m.CashOut)
                     .Select(m.ContactsName)
                     .Where(m.Type == 2)
                     .Where(m.Head == 11)
                     .Where(m.ContactsId == request.Id)
                     );

                    Sup = connection.List<PurchaseRow>(q => q
                   .SelectTableFields()
                   .Select(p.InvoiceDate)
                   .Select(p.Total)
                   //.Select(p.Advacne)                 
                   .Select(p.PurchaseFromName)
                   .Select(p.PurchaseFromDebtorsOpening)
                   .Select(p.PurchaseFromCreditorsOpening)
                   //.Where(c.Type == 2)
                   .Where(p.PurchaseFromId == request.Id)
                   );
                }

               
                double c_tot = 0;
              
                double creditorsopening = 0;
                //Debit
               
                // Credits
                if (Sup.Count > 0)
                {
                    if (Sup.Where(x => x.PurchaseFromId == request.Id).FirstOrDefault().PurchaseFromCreditorsOpening.HasValue)
                    {
                        creditorsopening = (double)Sup.Where(x => x.PurchaseFromId == request.Id).FirstOrDefault().PurchaseFromCreditorsOpening;
                    }
                }

                c_tot = ((double)Sup.Where(y => y.PurchaseFromId == request.Id).Sum(x => x.Total)) - (double)Cash.Where(y => y.ContactsId == request.Id).Sum(x => x.CashOut) + creditorsopening;


                response.Status = c_tot.ToString("#,##0.00");
            }
            catch (Exception ex)
            {
                response.Status = "Error " + ex.Message.ToString();
            }


            return response;
        }


        [ServiceAuthorize("Cashbook:Can Approve")]
        public StandardResponse Approve(SendSMSRequest request)
        {
            var response = new StandardResponse();

            try
            {
                var connection = _connections.NewByKey("Default");
                connection.Execute("UPDATE Cashbook SET ApprovedBy=" + Convert.ToInt32(Context.User.GetIdentifier()) + "WHERE Id=" + request.Id);

                var em = CashbookRow.Fields;
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



        [ServiceAuthorize("Cashbook:Export")]
        public Microsoft.AspNetCore.Mvc.FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.CashbookColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "Cashbook_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }
    }
}
