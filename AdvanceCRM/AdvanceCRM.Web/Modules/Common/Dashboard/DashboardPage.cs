
namespace AdvanceCRM.Common.Pages
{
    using Serenity;
    using AdvanceCRM.Web.Helpers;
    using Serenity.Data;
    using Microsoft.AspNetCore.Mvc;
    using System;
    
    using Administration;
    using System.IO;
    using System.Net;
    using AdvanceCRM.Reports;
    using System.Data;
    using System.Linq;
    using Serenity.Services;
    using System.Net.Mail;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Products;
    using AdvanceCRM.Tasks;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Quotation;
    using AdvanceCRM.Settings;
    using AdvanceCRM.Common;
    using AdvanceCRM.Services;
    using AdvanceCRM.Purchase;
    using AdvanceCRM.Sales;
    using System.Collections.Generic;
    using AdvanceCRM.Accounting;
    using AdvanceCRM.Common.Calendar;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Authorization;
    [Route("Dashboard")]
    public class DashboardController : Controller
    {
        private readonly ISqlConnections _connections;
        private readonly IMemoryCache _cache;
        private readonly ILogger<CalendarController> _logger;
        private readonly IRequestContext Context;

        public DashboardController(ISqlConnections connections, IRequestContext context, IMemoryCache cache, ILogger<CalendarController> logger)
        {
            _connections = connections;
            _cache = cache;
            _logger = logger;
            Context = context ?? throw new ArgumentNullException(nameof(context));

        }
        [Authorize, HttpGet, Route("~/")]
        public IActionResult Index()
        {
            var cachedModel = LocalCache.GetLocalStoreOnly("DashboardPageModel", TimeSpan.FromSeconds(1),
                UserRow.Fields.GenerationKey, () =>
                {
                    var model = new DashboardPageModel();

                    var e = EnquiryRow.Fields;
                    var q = QuotationRow.Fields;
                    var t = TasksRow.Fields;
                    var CMS = CMSRow.Fields;
                    var a = AMCVisitPlannerRow.Fields;
                    var amc = AMCRow.Fields;
                    var ef = EnquiryFollowupsRow.Fields;
                    var qf = QuotationFollowupsRow.Fields;
                    var c = ContactsRow.Fields;
                    var sc = SubContactsRow.Fields;
                    var u = UserRow.Fields;
                    var inv = InvoiceFollowupsRow.Fields;
                    var invo = InvoiceRow.Fields;
                    var sal = SalesFollowupsRow.Fields;
                    var sale = SalesRow.Fields;
                    var tel = TeleCallingFollowupsRow.Fields;
                    var CMSf = CMSFollowupsRow.Fields;
                    var p = ProductsRow.Fields;
                    var pp = PurchaseProductsRow.Fields;
                    var sp = SalesProductsRow.Fields;
                    var prp = PurchaseReturnProductsRow.Fields;
                    var srp = SalesReturnProductsRow.Fields;
                    var cp = ChallanProductsRow.Fields;
                    var ch = ChallanRow.Fields;
                    var pur = PurchaseRow.Fields;
                    var cash = CashbookRow.Fields;



                    var user = (UserDefinition)Context.User.ToUserDefinition();

                    using (var connection = _connections.NewFor<EnquiryRow>())
                    {
                        model.OpenEnq = connection.Count<EnquiryRow>(e.Status == 1 && e.AssignedId == user.UserId);
                        model.OpenQuot = connection.Count<QuotationRow>(q.Status == 1 && q.AssignedId == user.UserId);
                        model.CustomerCount = connection.Count<ContactsRow>(c.AssignedId == user.UserId);
                        model.OpenTasks = connection.Count<TasksRow>(t.StatusId ==1 && t.AssignedTo == user.UserId);
                        model.OpenCMS = connection.Count<CMSRow>(CMS.Status == 1 && CMS.AssignedTo == user.UserId);
                        model.OpenAMC = connection.Count<AMCVisitPlannerRow>(a.Status == 1 && a.AssignedTo == user.UserId);
                        model.Opensale = connection.Count<SalesRow>(sale.Status == 1 && sale.AssignedId == user.UserId);
                        model.OpenPi = connection.Count<InvoiceRow>(invo.Status == 1 && invo.AssignedId == user.UserId);

                        model.Customer = connection.List<ContactsRow>(f => f
                         .SelectTableFields()
                         .Select(c.Id)
                         .Select(c.Name)
                         );

                        var EnqAmtList = connection.List<EnquiryRow>(f => f
                          .SelectTableFields()
                          .Select(e.Id)
                          .Select(e.Total)
                          .Where(e.Total > 0)
                          .Where(e.Status==1)
                          .Where(e.AssignedId == user.UserId)
                        );

                        model.EnqAmt = 0;
                        foreach (var item in EnqAmtList)
                        {
                            //++model.amtcaselock;
                            model.EnqAmt += (Int32)item.Total;
                        }
                        //Quot
                        var QuotAmtList = connection.List<QuotationRow>(f => f
                          .SelectTableFields()
                          .Select(q.Id)
                          .Select(q.Total)
                          .Where(q.Total > 0)
                          .Where(q.Status == 1)
                          .Where(q.AssignedId == user.UserId)
                        );

                        model.QuotAmt = 0;
                        foreach (var item in QuotAmtList)
                        {
                            //++model.amtcaselock;
                            model.QuotAmt += (Int32)item.Total;
                        }

                        //pi
                        var PiAmtList = connection.List<InvoiceRow>(f => f
                          .SelectTableFields()
                          .Select(invo.Id)
                          .Select(invo.Total)
                          .Where(invo.Total > 0)
                          .Where(invo.Status == 1)
                          .Where(invo.AssignedId == user.UserId)
                        );

                        model.PIAmt = 0;
                        foreach (var item in PiAmtList)
                        {
                            //++model.amtcaselock;
                            model.PIAmt += (Int32)item.Total;
                        }
                        //pi
                        var saleAmtList = connection.List<SalesRow>(f => f
                          .SelectTableFields()
                          .Select(sale.Id)
                          .Select(sale.Total)
                          .Where(sale.Total > 0)
                          .Where(sale.Status == 1)
                          .Where(sale.AssignedId == user.UserId)
                        );

                        model.SaleAmt = 0;
                        foreach (var item in saleAmtList)
                        {
                            //++model.amtcaselock;
                            model.SaleAmt += (Int32)item.Total;
                        }

                        model.EnqFollowups = connection.List<EnquiryFollowupsRow>(f => f
                         .SelectTableFields()
                         .Select(ef.EnquiryId)
                         .Select(ef.EnquiryContactsId)
                         .Select(ef.FollowupNote)
                         .Select(ef.Details)
                         .Where(ef.Status == 1)
                         .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(ef.EnquiryAssignedId == user.UserId)
                         );

                        model.EnqFollowupsCompleted = connection.List<EnquiryFollowupsRow>(f => f
                         .SelectTableFields()
                         .Select(ef.EnquiryId)
                         .Select(ef.EnquiryContactsId)
                         .Select(ef.FollowupNote)
                         .Select(ef.Details)
                         .Where(ef.Status == 2)
                         .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(ef.EnquiryAssignedId == user.UserId)
                         );

                        model.QuotFollowups = connection.List<QuotationFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(qf.QuotationId)
                         .Select(qf.QuotationContactsId)
                         .Select(qf.FollowupNote)
                         .Select(qf.Details)
                         .Where(qf.Status == 1)
                         .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(qf.QuotationAssignedId == user.UserId)
                         );

                        model.QuotFollowupsCompleted = connection.List<QuotationFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(qf.QuotationId)
                         .Select(qf.QuotationContactsId)
                         .Select(qf.FollowupNote)
                         .Select(qf.Details)
                         .Where(qf.Status == 2)
                         .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(qf.QuotationAssignedId == user.UserId)
                         );


                        //model.QuotApprovalList = connection.List<QuotationRow>(g => g
                        // .SelectTableFields()
                        // .Select(q.Id)
                        // .Select(q.ContactsId)
                        // .Select(q.DisGrandTotal)
                        // .Where(q.Status == 2)
                        // .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                        // .Where(q.QuotationAssignedId == user.UserId)
                        // );


                        //model.QuotApprovalList = connection.List<QuotationRow>(g => g
                        //.SelectTableFields()
                        //.Select(q.Id)
                        //.Select(q.ContactsId)
                        //.Select(q.QuotationN)
                        //.Where(q.ApprovedBy.IsNull())
                        //);

                        //model.InvoiceApprovalList = connection.List<InvoiceRow>(g => g
                        //.SelectTableFields()
                        //.Select(invo.InvoiceNo)
                        //.Select(invo.ContactsId)
                        //.Select(invo.GrandTotal)
                        //.Where(invo.ApprovedBy.IsNull())
                        //);

                        //model.SalesApprovalList = connection.List<SalesRow>(g => g
                        //.SelectTableFields()
                        //.Select(sale.Id)
                        //.Select(sale.ContactsId)
                        //.Select(sale.GrandTotal)
                        //.Where(sale.ApprovedBy.IsNull())
                        //);

                        //model.ChallanApprovalList = connection.List<ChallanRow>(g => g
                        //.SelectTableFields()
                        //.Select(ch.Id)
                        //.Select(ch.ContactsId)
                        //.Select(ch.Date)
                        //.Where(ch.ApprovedBy.IsNull())
                        //);

                        //model.PurchaseApprovalList = connection.List<PurchaseRow>(g => g
                        //.SelectTableFields()
                        //.Select(pur.Id)
                        //.Select(pur.PurchaseFromId)
                        //.Select(pur.Total)
                        //.Where(pur.ApprovedBy.IsNull())
                        //);

                        //model.CashbookApprovalList = connection.List<CashbookRow>(g => g
                        //.SelectTableFields()
                        //.Select(cash.Id)
                        //.Select(cash.Type)
                        //.Select(cash.Date)
                        //.Where(cash.ApprovedBy.IsNull())
                        //);

                        model.ODEnqFollowups = connection.List<EnquiryFollowupsRow>(f => f
                         .SelectTableFields()
                         .Select(ef.EnquiryId)
                         .Select(ef.EnquiryContactsId)
                         .Select(ef.FollowupNote)
                         .Select(ef.Details)
                         .Where(ef.Status == 1)
                         .Where(new Criteria("CAST(FollowupDate as DATE)<" + DateTime.Now.ToSqlDate()))
                         .Where(ef.EnquiryAssignedId == user.UserId)
                         );

                        model.ODQuotFollowups = connection.List<QuotationFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(qf.QuotationId)
                         .Select(qf.QuotationContactsId)
                         .Select(qf.FollowupNote)
                         .Select(qf.Details)
                         .Where(qf.Status == 1)
                         .Where(new Criteria("CAST(FollowupDate as DATE)<" + DateTime.Now.ToSqlDate()))
                         .Where(qf.QuotationAssignedId == user.UserId)
                         );

                        model.Tasks = connection.List<TasksRow>(ts => ts
                         .SelectTableFields()
                         .Select(t.Id)
                         .Select(t.Task)
                         .Select(t.Details)
                         .Select(t.CreationDate)
                         .Select(t.ExpectedCompletion)
                         .Select(t.AssignedBy)
                         .Select(t.AssignedTo)
                         .Where(t.StatusId != 2)
                         .Where(new Criteria("CAST(CreationDate as DATE)<=" + DateTime.Now.ToSqlDate()))
                         .Where(new Criteria("CAST(ExpectedCompletion as DATE)>" + DateTime.Now.ToSqlDate()))
                         .Where(t.AssignedTo == user.UserId)
                         );

                        model.TasksOpen = connection.List<TasksRow>(ts => ts
                       .SelectTableFields()
                       .Select(t.Id)
                       .Select(t.Task)
                       .Select(t.Details)
                       .Select(t.CreationDate)
                       .Select(t.ExpectedCompletion)
                       .Select(t.AssignedBy)
                       .Select(t.AssignedTo)
                       .Where(t.StatusId != 2)
                       .Where(new Criteria("CAST(ExpectedCompletion as DATE)=" + DateTime.Now.ToSqlDate()))
                       .Where(t.AssignedTo == user.UserId)
                       );

                        model.TasksCompleted = connection.List<TasksRow>(ts => ts
                         .SelectTableFields()
                         .Select(t.Id)
                         .Select(t.Task)
                         .Select(t.Details)
                         .Select(t.CreationDate)
                         .Select(t.ExpectedCompletion)
                         .Select(t.AssignedBy)
                         .Select(t.AssignedTo)
                         .Where(t.StatusId == 2)
                         .Where(new Criteria("CAST(ExpectedCompletion as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(t.AssignedTo == user.UserId)
                         );

                        model.ODTasks = connection.List<TasksRow>(td => td
                         .SelectTableFields()
                         .Select(t.Id)
                         .Select(t.Task)
                         .Select(t.Details)
                         .Select(t.CreationDate)
                         .Select(t.ExpectedCompletion)
                         .Select(t.AssignedBy)
                         .Select(t.AssignedTo)
                         .Where(t.StatusId != 2)
                         .Where(new Criteria("CAST(ExpectedCompletion as DATE)<" + DateTime.Now.ToSqlDate()))
                         .Where(t.AssignedTo == user.UserId)
                         );

                        model.CMS = connection.List<CMSRow>(ts => ts
                         .SelectTableFields()
                         .Select(CMS.Id)
                         .Select(CMS.ContactsName)
                         .Select(CMS.ContactsPhone)
                         .Select(CMS.Date)
                         .Select(CMS.ProductsName)
                         .Select(CMS.ComplaintComplaintType)
                         .Select(CMS.Instructions)
                         .Where(CMS.Status == 1)
                         .Where(new Criteria("CAST(ExpectedCompletion as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(CMS.AssignedTo == user.UserId)
                         );

                        model.CMSCompleted = connection.List<CMSRow>(ts => ts
                         .SelectTableFields()
                         .Select(CMS.Id)
                         .Select(CMS.ContactsName)
                         .Select(CMS.ContactsPhone)
                         .Select(CMS.Date)
                         .Select(CMS.ProductsName)
                         .Select(CMS.ComplaintComplaintType)
                         .Select(CMS.Instructions)
                         .Where(CMS.Status == 2)
                         .Where(new Criteria("CAST(ExpectedCompletion as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(CMS.AssignedTo == user.UserId)
                         );

                        model.ODCMS = connection.List<CMSRow>(td => td
                         .Select(CMS.Id)
                         .Select(CMS.ContactsName)
                         .Select(CMS.Date)
                         .Select(CMS.ProductsName)
                         .Select(CMS.ContactsPhone)
                         .Select(CMS.ComplaintComplaintType)
                         .Select(CMS.Instructions)
                         .Where(CMS.Status == 1)
                         .Where(new Criteria("CAST(ExpectedCompletion as DATE)<" + DateTime.Now.ToSqlDate()))
                         .Where(CMS.AssignedTo == user.UserId)
                         );

                        model.InvoiceFollowups = connection.List<InvoiceFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(inv.InvoiceId)
                         .Select(inv.InvoiceContactsId)
                         .Select(inv.FollowupNote)
                         .Select(inv.Details)
                         .Where(inv.Status == 1)
                         .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(inv.InvoiceAssignedId == user.UserId)
                         );

                        model.InvoiceFollowupsCompleted = connection.List<InvoiceFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(inv.InvoiceId)
                         .Select(inv.InvoiceContactsId)
                         .Select(inv.FollowupNote)
                         .Select(inv.Details)
                         .Where(inv.Status == 2)
                         .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(inv.InvoiceAssignedId == user.UserId)
                         );

                        model.ODInvoiceFollowups = connection.List<InvoiceFollowupsRow>(f => f
                         .SelectTableFields()
                         .Select(inv.InvoiceId)
                         .Select(inv.InvoiceContactsId)
                         .Select(inv.FollowupNote)
                         .Select(inv.Details)
                         .Where(inv.Status == 1)
                         .Where(new Criteria("CAST(FollowupDate as DATE)<" + DateTime.Now.ToSqlDate()))
                         .Where(inv.InvoiceAssignedId == user.UserId)
                         );

                        model.SalesFollowups = connection.List<SalesFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(sal.SalesId)
                         .Select(sal.SalesContactsId)
                         .Select(sal.FollowupNote)
                         .Select(sal.Details)
                         .Where(sal.Status == 1)
                         .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(sal.SalesAssignedId == user.UserId)
                         );

                        model.SalesFollowupsCompleted = connection.List<SalesFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(sal.SalesId)
                         .Select(sal.SalesContactsId)
                         .Select(sal.FollowupNote)
                         .Select(sal.Details)
                         .Where(sal.Status == 2)
                         .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(sal.SalesAssignedId == user.UserId)
                         );

                        model.ODSalesFollowups = connection.List<SalesFollowupsRow>(f => f
                         .SelectTableFields()
                         .Select(sal.SalesId)
                         .Select(sal.SalesContactsId)
                         .Select(sal.FollowupNote)
                         .Select(sal.Details)
                         .Where(sal.Status == 1)
                         .Where(new Criteria("CAST(FollowupDate as DATE)<" + DateTime.Now.ToSqlDate()))
                         .Where(sal.SalesAssignedId == user.UserId)
                         );

                        model.SalesPaymentDue = connection.List<SalesRow>(f => f
                         .SelectTableFields()
                         .Select(sale.Id)
                         .Select(sale.Date)
                         .Select(sale.ContactsName)
                         .Select(sale.ContactsPhone)
                         .Select(sale.ContactsCreditDays)
                         .Select(sale.Type)
                         .Select(sale.Total)
                         .Where(sale.Status == 1)
                         .Where(sale.Type == 2)
                         .Where(sale.AssignedId == user.UserId)
                         );


                        //Stock check for reorder level
                        #region Stockdata
                        model.StockData = false;

                        Products = connection.List<ProductsRow>(q1 => q1
                         .SelectTableFields()
                         .Select(p.Name)
                         .Select(p.OpeningStock)
                         .Select(p.MinimumStock)
                         .Select(p.MaximumStock)
                         .Where(p.RawMaterial == 0)
                         );

                        PurchaseProducts = connection.List<PurchaseProductsRow>(q2 => q2
                                 .SelectTableFields()
                                 .Select(pp.ProductsName)
                                 .Select(pp.Quantity)
                                 .Select(pp.Price)
                                 );

                        SalesProducts = connection.List<SalesProductsRow>(q4 => q4
                         .SelectTableFields()
                         .Select(sp.ProductsName)
                         .Select(sp.Quantity)
                         .Select(sp.Price)
                         );

                        PurchaseReturnProducts = connection.List<PurchaseReturnProductsRow>(q5 => q5
                         .SelectTableFields()
                         .Select(prp.ProductsName)
                         .Select(prp.Quantity)
                         .Select(prp.Price)
                         );

                        SalesReturnProducts = connection.List<SalesReturnProductsRow>(q6 => q6
                         .SelectTableFields()
                         .Select(srp.ProductsName)
                         .Select(srp.Quantity)
                         .Select(srp.Price)
                         );

                        ChallanProducts = connection.List<ChallanProductsRow>(q7 => q7
                         .SelectTableFields()
                         .Select(cp.ProductsName)
                         .Select(cp.Quantity)
                         .Select(cp.Price)
                         .Where(cp.ChallanInvoiceMade != 1)
                         );

                        double pqty = 0; double sqty = 0; double prqty = 0; double srqty = 0; double cqty = 0; double qty = 0;

                        foreach (var item in Products)
                        {
                            pqty = (double)PurchaseProducts.Where(y => y.ProductsName == item.Name).Sum(x => x.Quantity);

                            sqty = (double)SalesProducts.Where(y => y.ProductsName == item.Name).Sum(x => x.Quantity);

                            prqty = (double)PurchaseReturnProducts.Where(y => y.ProductsName == item.Name).Sum(x => x.Quantity);

                            srqty = (double)SalesReturnProducts.Where(y => y.ProductsName == item.Name).Sum(x => x.Quantity);

                            cqty = (double)ChallanProducts.Where(y => y.ProductsName == item.Name).Sum(x => x.Quantity);

                            qty = pqty + srqty + item.OpeningStock.Value - (sqty + prqty + cqty);

                            if (qty < item.MinimumStock)
                            {
                                model.StockData = true;
                            }
                        }
                        #endregion Stockdata

                     model.AMCED = connection.List<AMCRow>(ts => ts
                    .SelectTableFields()
                    .Select(amc.Id)
                    .Select(amc.ContactsId)
                    .Select(amc.ContactsPhone)
                    .Where(amc.Status == 1)
                    .Where(new Criteria("CAST(EndDate as DATE)=" + DateTime.Now.ToSqlDate()))
                    .Where(amc.AssignedId == user.UserId)
                    );

                     model.AMCOD = connection.List<AMCRow>(ts => ts
                    .SelectTableFields()
                    .Select(amc.Id)
                    .Select(amc.ContactsId)
                    .Select(amc.ContactsPhone)
                    .Where(amc.Status == 1)
                    .Where(new Criteria("CAST(EndDate as DATE)<" + DateTime.Now.ToSqlDate()))
                    .Where(amc.AssignedId == user.UserId)
                    );

                        model.AMC = connection.List<AMCVisitPlannerRow>(ts => ts
                     .SelectTableFields()
                     .Select(a.Id)
                     .Select(a.AMCContactsId)
                     .Select(a.VisitDetails)
                     .Where(a.Status == 1)
                     .Where(new Criteria("CAST(VisitDate as DATE)=" + DateTime.Now.ToSqlDate()))
                     .Where(a.AssignedTo == user.UserId)
                     );

                        model.AMCCompleted = connection.List<AMCVisitPlannerRow>(ts => ts
                         .SelectTableFields()
                         .Select(a.Id)
                         .Select(a.AMCContactsId)
                         .Select(a.VisitDetails)
                         .Where(a.Status == 2)
                         .Where(new Criteria("CAST(VisitDate as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(a.AssignedTo == user.UserId)
                         );

                        model.ODAMC = connection.List<AMCVisitPlannerRow>(ts => ts
                         .SelectTableFields()
                         .Select(a.Id)
                         .Select(a.AMCContactsId)
                         .Select(a.VisitDetails)
                         .Where(a.Status == 1)
                         .Where(new Criteria("CAST(VisitDate as DATE)<" + DateTime.Now.ToSqlDate()))
                         .Where(a.AssignedTo == user.UserId)
                         );

                        model.Users = connection.List<UserRow>(us => us
                         .SelectTableFields()
                         .Select(u.UserId)
                         .Select(u.Username)
                         );

                        model.EnqListChart = connection.List<EnquiryRow>(el => el
                         .SelectTableFields()
                         .Select(e.Id)
                         .Where(e.Date <= (DateTime.Now.Date))
                         .Where(e.Date > (DateTime.Now.Date.AddDays(-9)))
                         .Where(e.AssignedId == user.UserId)
                         );

                        model.QuotListChart = connection.List<QuotationRow>(ql => ql
                         .SelectTableFields()
                         .Select(q.Id)
                         .Where(q.Date <= (DateTime.Now.Date))
                         .Where(q.Date > (DateTime.Now.Date.AddDays(-9)))
                         .Where(q.AssignedId == user.UserId)
                         );


                        //TeleCalling Followups
                        model.TCFollowups = connection.List<TeleCallingFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(tel.TeleCallingId)
                         .Select(tel.TeleCallingContactsId)
                         .Select(tel.FollowupNote)
                         .Select(tel.Details)
                         .Where(tel.Status == 1)
                         .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(tel.RepresentativeId == user.UserId)
                         );

                        model.TCCompleted = connection.List<TeleCallingFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(tel.TeleCallingId)
                         .Select(tel.TeleCallingContactsId)
                         .Select(tel.FollowupNote)
                         .Select(tel.Details)
                         .Where(tel.Status == 2)
                         .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(tel.RepresentativeId == user.UserId)
                         );

                        model.ODTCFollowups = connection.List<TeleCallingFollowupsRow>(f => f
                         .SelectTableFields()
                         .Select(tel.TeleCallingId)
                         .Select(tel.TeleCallingContactsId)
                         .Select(tel.FollowupNote)
                         .Select(tel.Details)
                         .Where(tel.Status == 1)
                         .Where(new Criteria("CAST(FollowupDate as DATE)<" + DateTime.Now.ToSqlDate()))
                         .Where(tel.RepresentativeId == user.UserId)
                         );

                        model.CMSFollowups = connection.List<CMSFollowupsRow>(f => f
                              .SelectTableFields()
                              .Select(CMSf.CMSId)
                              .Select(CMSf.CMSContactsId)
                              .Select(CMSf.FollowupNote)
                              .Select(CMSf.Details)
                              .Select(CMSf.ContactName)
                              .Select(CMSf.ContactPhone)
                              .Select(CMSf.ProductsName)
                              .Select(CMSf.ComplaintType)
                              .Where(CMSf.Status == 1)
                              .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                              .Where(CMSf.CMSAssignedTo == user.UserId)
                            );

                        model.CMSFollowupsCompleted = connection.List<CMSFollowupsRow>(f => f
                               .SelectTableFields()
                              .Select(CMSf.CMSId)
                              .Select(CMSf.CMSContactsId)
                              .Select(CMSf.FollowupNote)
                              .Select(CMSf.Details)
                              .Select(CMSf.ContactName)
                              .Select(CMSf.ContactPhone)
                              .Select(CMSf.ProductsName)
                              .Select(CMSf.ComplaintType)
                              .Where(CMSf.Status == 2)
                              .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                              .Where(CMSf.CMSAssignedTo == user.UserId)
                            );

                        model.ODCMSFollowups = connection.List<CMSFollowupsRow>(f => f
                               .SelectTableFields()
                               .Select(CMSf.CMSId)
                               .Select(CMSf.CMSContactsId)
                               .Select(CMSf.FollowupNote)
                               .Select(CMSf.Details)
                               .Select(CMSf.ContactName)
                               .Select(CMSf.ContactPhone)
                               .Select(CMSf.ProductsName)
                               .Select(CMSf.ComplaintType)
                               .Where(CMSf.Status == 1)
                               .Where(new Criteria("CAST(FollowupDate as DATE)<" + DateTime.Now.ToSqlDate()))
                               .Where(CMSf.CMSAssignedTo == user.UserId)
                             );

                    }

                    return model;
                });

            return View(MVC.Views.Common.Dashboard.DashboardIndex, cachedModel);
        }


        //public ActionResult SendMail(string subject, string tomail, string body)
        //{
        //    var User = new UserRow();

        //    using (var connection = _connections.NewFor<UserRow>())
        //    {
        //        var u = UserRow.Fields;
        //        User = connection.TryById<UserRow>(Context.User.GetIdentifier(), q => q
        //            .SelectTableFields()
        //            .Select(u.Host)
        //            .Select(u.Port)
        //            .Select(u.SSL)
        //            .Select(u.EmailId)
        //            .Select(u.EmailPassword));
        //    }

        //    string response;

        //    try
        //    {
        //        var message = new MailMessage();
        //        var m = new MailAddress(User.EmailId, User.EmailId);
        //        message.From = m;
        //        List<string> Receipent = tomail.Split(',').ToList();
        //        for (int i = 0; i < Receipent.Count; i++)
        //        {
        //            message.To.Add(Receipent.ElementAt(i));
        //        }

        //        message.Subject = subject;
        //        message.IsBodyHtml = true;
        //        message.Body = HttpUtility.UrlDecode(body, System.Text.Encoding.Default);
        //        response = EmailHelper.Send(message, User.EmailId, User.EmailPassword, User.SSL.Value, User.Host, User.Port.Value);
        //    }
        //    catch (Exception ex)
        //    {

        //        response = "Error\n\n" + ex.Message.ToString();
        //    }


        //    return Json(response, JsonRequestBehavior.AllowGet);
        //}
        //[HttpPost, ServiceAuthorize, Route("~/Dashboard/SendMail")]
        //public ActionResult SendMail(string subject, string tomail, string body, List<string> attachments)

        //{
        //    var User = new UserRow();

        //    using (var connection = _connections.NewFor<UserRow>())
        //    {
        //        var u = UserRow.Fields;
        //        User = connection.TryById<UserRow>(Context.User.GetIdentifier(), q => q
        //            .SelectTableFields()
        //            .Select(u.Host)
        //            .Select(u.Port)
        //            .Select(u.SSL)
        //            .Select(u.EmailId)
        //            .Select(u.EmailPassword));
        //    }

        //    string response;

        //    try
        //    {
        //        var message = new MailMessage();
        //        var m = new MailAddress(User.EmailId, User.EmailId);
        //        message.From = m;
        //        List<string> Receipent = tomail.Split(',').ToList();
        //        for (int i = 0; i < Receipent.Count; i++)
        //        {
        //            message.To.Add(Receipent.ElementAt(i));
        //        }

        //        message.Subject = subject;
        //        message.IsBodyHtml = true;
        //        message.Body = HttpUtility.UrlDecode(body, System.Text.Encoding.Default);
        //        // ✅ Handle Attachments

        //        if (attachments != null && attachments.Count > 0)
        //        {
        //            foreach (var filePath in attachments)
        //            {

        //                string appDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "upload", "temporary", Path.GetFileName(filePath));

        //                if (System.IO.File.Exists(appDataPath)) // ✅ Check the correct absolute path
        //                {
        //                    string tempFilePath = Path.Combine(Path.GetTempPath(), Path.GetFileName(filePath));

        //                    System.IO.File.Copy(appDataPath, tempFilePath, true); // ✅ Copy file to temp folder
        //                    message.Attachments.Add(new Attachment(tempFilePath));
        //                }
        //            }
        //        }

        //        response = EmailHelper.Send(message, User.EmailId, User.EmailPassword, User.SSL.Value, User.Host, User.Port.Value);
        //    }
        //    catch (Exception ex)
        //    {

        //        response = "Error\n\n" + ex.Message.ToString();
        //    }


        //    return Json(response, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost, ServiceAuthorize, Route("~/Dashboard/SendMail")]
        public JsonResult SendMail(string subject, string tomail, string body, List<string> attachments)

        {
            var User = new UserRow();

            using (var connection = _connections.NewFor<UserRow>())
            {
                var u = UserRow.Fields;
                User = connection.TryById<UserRow>(Context.User.GetIdentifier(), q => q
                    .SelectTableFields()
                    .Select(u.Host)
                    .Select(u.Port)
                    .Select(u.SSL)
                    .Select(u.EmailId)
                    .Select(u.EmailPassword));
            }

            string response;

            try
            {
                var message = new MailMessage();
                var m = new MailAddress(User.EmailId, User.EmailId);
                message.From = m;
                List<string> Receipent = tomail.Split(',').ToList();
                //for (int i = 0; i < Receipent.Count; i++)
                //{
                //    message.To.Add(Receipent.ElementAt(i));
                //}

                if (Receipent.Count > 0)
                {
                    // Add the first recipient in "To"

                    message.To.Add(User.EmailId);

                    // Add remaining recipients in "BCC"
                    for (int i = 0; i < Receipent.Count; i++)
                    {
                        message.Bcc.Add(Receipent[i]);
                    }
                }

                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = WebUtility.UrlDecode(body);
                // ✅ Handle Attachments

                if (attachments != null && attachments.Count > 0)
                {
                    foreach (var filePath in attachments)
                    {

                        string appDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "upload", "temporary", Path.GetFileName(filePath));

                        if (System.IO.File.Exists(appDataPath)) // ✅ Check the correct absolute path
                        {
                            string tempFilePath = Path.Combine(Path.GetTempPath(), Path.GetFileName(filePath));

                            System.IO.File.Copy(appDataPath, tempFilePath, true); // ✅ Copy file to temp folder
                            message.Attachments.Add(new Attachment(tempFilePath));
                        }
                    }
                }

                response = EmailHelper.Send(message, User.EmailId, User.EmailPassword, User.SSL.Value, User.Host, User.Port.Value);
            }
            catch (Exception ex)
            {

                response = "Error\n\n" + ex.Message.ToString();
            }


            return new JsonResult(response);
        }



        [HttpPost, Route("~/Dashboard/SendSMS")]
        public JsonResult SendSMS(SendSMSRequest request)
        {
            string response;
            try
            {
                response = SMSHelper.SendSMS(request.Phone, request.SMSType,request.TemplateID);
            }
            catch (Exception ex)
            {
                response = ex.Message.ToString();
            }
            return new JsonResult(response);
        }

        private List<ProductsRow> Products { get; set; }
        private List<PurchaseProductsRow> PurchaseProducts { get; set; }
        private List<SalesProductsRow> SalesProducts { get; set; }
        private List<PurchaseReturnProductsRow> PurchaseReturnProducts { get; set; }
        private List<SalesReturnProductsRow> SalesReturnProducts { get; set; }
        private List<ChallanProductsRow> ChallanProducts { get; set; }
    }
}
