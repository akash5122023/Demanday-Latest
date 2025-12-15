
namespace AdvanceCRM.Common.Pages
{
    using Serenity;
using AdvanceCRM.Web.Helpers;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    
    using Administration;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Tasks;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Quotation;
    using System.Linq;
    using AdvanceCRM.Services;
    using AdvanceCRM.Sales;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using MailChimp.Net;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Configuration;
    using Serenity.Abstractions;
    using Serenity.Services;

    [Route("DSRDashboard")]
    public class DSRDashboardController : Controller
    {
        private readonly ISqlConnections _connections;


        public DSRDashboardController(

            ISqlConnections connections
          )
        {
            this._connections = connections;
        }
            [Authorize, HttpGet, Route("~/DSR")]
        public ActionResult Index()
        {
            var cachedModel = LocalCache.GetLocalStoreOnly("DSRDashboardModel", TimeSpan.FromSeconds(60),
                         ContactsRow.Fields.GenerationKey, () =>
                         {
                             var model = new DSRDashboardPageModel();
                             var u = UserRow.Fields;
                             var e = EnquiryRow.Fields;
                             using (var connection = _connections.NewFor<UserRow>())
                             {
                                 model.Users = connection.List<UserRow>(q => q
                                 .SelectTableFields()
                                 .Select(u.UserId)
                                 .Select(u.DisplayName));

                                 model.SourceWise = connection.List<EnquiryRow>(q => q
                                  .SelectTableFields()
                                  .Select(e.Source)
                                  .Select(e.SourceId)
                                  .Where(e.Date >= DateTime.Now.AddMonths(-1)));

                                 model.RepWise = connection.List<EnquiryRow>(q => q
                                  .SelectTableFields()
                                  .Select(e.AssignedUsername)
                                  .Select(e.AssignedId)
                                  .Where(e.Date >= DateTime.Now.AddMonths(-1)));
                                 ViewBag.ListData = model.Users;
                             }
                             return model;
                         });
            ViewBag.ListData = cachedModel.Users;
            return View(MVC.Views.Common.Dashboard.DSRDashboardIndex, cachedModel);

        }

        [HttpGet]
        public ActionResult LoadData(int UserId, string date,string date1)
        {
            DateTime dt; DateTime dt1;
            if (date == null)
                dt = DateTime.Now.AddDays(-1);
            else
                dt = DateTime.ParseExact(date, "dd-MM-yyyy", null);

            if (date1 == null)
                dt1 = DateTime.Now.AddDays(0);
            else
                dt1 = DateTime.ParseExact(date1, "dd-MM-yyyy", null);

            var model = new DSRDashboardPageModel();
            using (var connection = _connections.NewFor<EnquiryRow>())
            {
                var t = TasksRow.Fields;
                var e = EnquiryRow.Fields;
                var d = QuotationRow.Fields;
                var s = SalesRow.Fields;
                var i = InvoiceRow.Fields;
                var c = CMSRow.Fields;

                string criteria = "";

                //  var enquiryList = connection.List<EnquiryRow>(q => q
                //  .SelectTableFields()
                //  .Select(e.Date)
                //  .Select(e.ClosingDate)
                //  .Select(e.ClosingType)
                //  .Select(e.Total)
                //  .Where(e.AssignedId == UserId)
                //  .Where(e.ClosingDate >= dt)
                //  .Where(e.ClosingDate <= dt1));

                //  model.WonEnq = model.LostEnq = 0;
                //  model.EnqClosure = model.WonEnqAmt = model.LostEnqAmt = 0;
                //  foreach (var item in enquiryList)
                //  {
                //      if (item.ClosingType == Masters.ClosingTypeMaster.Won)
                //      {
                //          ++model.WonEnq;
                //          model.WonEnqAmt += item.Total;
                //          model.EnqClosure += (item.ClosingDate.Value - item.Date.Value).TotalDays;
                //      }
                //      else if (item.ClosingType == Masters.ClosingTypeMaster.Lost)
                //      {
                //          ++model.LostEnq;
                //          model.LostEnqAmt += item.Total;
                //          model.EnqClosure += (item.ClosingDate.Value - item.Date.Value).TotalDays;
                //      }
                //  }

                //  model.EnqClosure = model.EnqClosure / (model.WonEnq + model.LostEnq);

                //  var quotationList = connection.List<QuotationRow>(q => q
                //  .SelectTableFields()
                //  .Select(d.Date)
                //  .Select(d.ClosingDate)
                //  .Select(d.ClosingType)
                //  .Select(d.Total)
                //  .Where(d.AssignedId == UserId)
                //  .Where(d.ClosingDate >= dt)
                //.Where(d.ClosingDate <= dt1));

                //  model.WonQuo = model.LostQuo = 0;
                //  model.WonQuoAmt = model.LostQuoAmt = 0;
                //  foreach (var item in quotationList)
                //  {
                //      if (item.ClosingType == Masters.ClosingTypeMaster.Won)
                //      {
                //          ++model.WonQuo;
                //          model.WonQuoAmt += item.Total;
                //          model.QuoClosure += (item.ClosingDate.Value - item.Date.Value).TotalDays;
                //      }
                //      else if (item.ClosingType == Masters.ClosingTypeMaster.Lost)
                //      {
                //          ++model.LostQuo;
                //          model.LostQuoAmt += item.Total;
                //          model.QuoClosure += (item.ClosingDate.Value - item.Date.Value).TotalDays;
                //      }
                //  }



                //  model.QuoClosure = model.QuoClosure / (model.WonQuo + model.LostQuo);

                //  var PiList = connection.List<InvoiceRow>(q => q
                // .SelectTableFields()
                // .Select(i.Date)
                // .Select(i.ClosingDate)
                // .Select(i.DueDate)
                // .Select(i.Status)
                // .Select(i.Total)
                // .Where(i.AssignedId == UserId)
                // .Where(i.Date >= dt)
                // .Where(i.Date <= dt1));

                //  model.OpenPI = model.ClosePI = model.NewPI = model.OverPI = 0;
                //  model.OpenPIAmt = model.ClosePIAmt = model.NewPIAmt = model.OverPIAmt = 0;
                //  foreach (var item in PiList)
                //  {
                //      if (item.Status == Masters.StatusMaster.Open)
                //      {
                //          ++model.OpenPI;
                //          model.OpenPIAmt += item.Total;

                //      }
                //      if (item.Status == Masters.StatusMaster.Closed)
                //      {
                //          ++model.ClosePI;
                //          model.ClosePIAmt += item.Total;
                //      }
                //      if (item.Date >= dt)
                //      {
                //          ++model.NewPI;
                //          model.NewPIAmt += item.Total;
                //      }
                //      if (item.DueDate < item.ClosingDate)
                //      {
                //          ++model.OverPI;
                //          model.OverPIAmt += item.Total;
                //      }
                //  }


                //  var salesList = connection.List<SalesRow>(q => q
                //  .SelectTableFields()
                //  .Select(s.Date)
                //  .Select(s.ClosingDate)
                //  .Select(s.DueDate)
                //  .Select(s.Status)
                //  .Select(s.Total)
                //  .Where(s.AssignedId == UserId)
                //  .Where(s.DueDate >= dt)
                //  .Where(s.DueDate <= dt1));

                //  model.OpenSales = model.CloseSales = model.NewSales = model.OverSales = 0;
                //  model.OpenSalesAmt = model.CloseSalesAmt = model.NewSalesAmt = model.OverSalesAmt = 0;
                //  foreach (var item in salesList)
                //  {
                //      if (item.Status == Masters.StatusMaster.Open)
                //      {
                //          ++model.OpenSales;
                //          model.OpenSalesAmt += item.Total;

                //      }
                //      if (item.Status == Masters.StatusMaster.Closed)
                //      {
                //          ++model.CloseSales;
                //          model.CloseSalesAmt += item.Total;
                //      }
                //      if (item.Date >= dt)
                //      {
                //          ++model.NewSales;
                //          model.NewSalesAmt += item.Total;
                //      }
                //      if (item.DueDate < item.ClosingDate)
                //      {
                //          ++model.OverSales;
                //          model.OverSalesAmt += item.Total;
                //      }
                //  }

                //  var tasksList = connection.List<TasksRow>(q => q
                //  .SelectTableFields()
                //  .Select(t.ExpectedCompletion)
                //  .Select(t.CreationDate)
                //  .Select(t.CompletionDate)
                //  .Select(t.StatusId)
                //  .Where(t.AssignedTo == UserId)
                //  .Where(t.ExpectedCompletion >= dt)
                //  .Where(t.ExpectedCompletion <= dt1));

                //  model.NewTask = tasksList.Count;
                //  foreach (var item in tasksList)
                //  {
                //      if (item.StatusId == 2)
                //          ++model.CloseTask;

                //      if (item.StatusId != 2)
                //          ++model.OpenTask;

                //      if (item.CompletionDate > item.ExpectedCompletion)
                //          ++model.OverTask;
                //  }

                //  var cmsList = connection.List<CMSRow>(q => q
                //  .SelectTableFields()
                //  .Select(c.ExpectedCompletion)
                //  .Select(c.Date)
                //  .Select(c.CompletionDate)
                //  .Select(c.Status)
                //  .Where(c.AssignedTo == UserId)
                //  .Where(c.ExpectedCompletion >= dt)
                //  .Where(c.ExpectedCompletion <= dt1));

                //  model.NewCMS = cmsList.Count;
                //  foreach (var item in cmsList)
                //  {
                //      if (item.Status == Masters.CMSStatusMaster.Closed)
                //          ++model.CloseCMS;

                //      if (item.Status != Masters.CMSStatusMaster.Closed)
                //          ++model.OpenCMS;

                //      if (item.CompletionDate > item.ExpectedCompletion)
                //          ++model.OverCMS;
                //  }

                var enquiryList = connection.List<EnquiryRow>(q => q
              .SelectTableFields()
              .Select(e.Date)
              .Select(e.ClosingDate)
              .Select(e.ClosingType)
              .Select(e.Total)
              .Select(e.Status)
              .Where(e.AssignedId == UserId)
              .Where(e.Date >= dt)
              .Where(e.Date <= dt1));

                var totalList = enquiryList.Count();

                model.TotalEnq = totalList;

                model.WonEnq = model.LostEnq = 0;
                model.EnqClosure = model.WonEnqAmt = model.LostEnqAmt = 0;
                foreach (var item in enquiryList)
                {

                    if (item.ClosingType == Masters.ClosingTypeMaster.Won)
                    {
                        ++model.WonEnq;
                        model.WonEnqAmt += item.Total ?? 0;
                        model.EnqClosure += (item.ClosingDate.Value - item.Date.Value).TotalDays;
                    }
                    else if (item.ClosingType == Masters.ClosingTypeMaster.Lost)
                    {
                        ++model.LostEnq;
                        model.LostEnqAmt += item.Total ?? 0;
                        model.EnqClosure += (item.ClosingDate.Value - item.Date.Value).TotalDays;
                    }

                }

                model.EnqClosure = model.EnqClosure / (model.WonEnq + model.LostEnq);

                var quotationList = connection.List<QuotationRow>(q => q
                .SelectTableFields()
                .Select(d.Date)
                .Select(d.ClosingDate)
                .Select(d.ClosingType)
                .Select(d.Total)
                .Where(d.AssignedId == UserId)
                .Where(d.Date >= dt)
              .Where(d.Date <= dt1));

                model.WonQuo = model.LostQuo = 0;
                model.WonQuoAmt = model.LostQuoAmt = 0;

                var totalQuotation = quotationList.Count();
                model.totalQua = totalQuotation;
                foreach (var item in quotationList)
                {
                    if (item.ClosingType == Masters.ClosingTypeMaster.Won)
                    {
                        ++model.WonQuo;
                        model.WonQuoAmt += item.Total ?? 0;
                        model.QuoClosure += (item.ClosingDate.Value - item.Date.Value).TotalDays;
                    }
                    else if (item.ClosingType == Masters.ClosingTypeMaster.Lost)
                    {
                        ++model.LostQuo;
                        model.LostQuoAmt += item.Total ?? 0;
                        model.QuoClosure += (item.ClosingDate.Value - item.Date.Value).TotalDays;
                    }
                }



                model.QuoClosure = model.QuoClosure / (model.WonQuo + model.LostQuo);

                var PiList = connection.List<InvoiceRow>(q => q
               .SelectTableFields()
               .Select(i.Date)
               .Select(i.ClosingDate)
               .Select(i.DueDate)
               .Select(i.Status)
               .Select(i.Total)
               .Where(i.AssignedId == UserId)
               .Where(i.Date >= dt)
               .Where(i.Date <= dt1));

                model.OpenPI = model.ClosePI = model.NewPI = model.OverPI = 0;
                model.OpenPIAmt = model.ClosePIAmt = model.NewPIAmt = model.OverPIAmt = 0;
                foreach (var item in PiList)
                {
                    if (item.Status == Masters.StatusMaster.Open)
                    {
                        ++model.OpenPI;
                        model.OpenPIAmt += item.Total ?? 0;

                    }
                    if (item.Status == Masters.StatusMaster.Closed)
                    {
                        ++model.ClosePI;
                        model.ClosePIAmt += item.Total ?? 0;
                    }
                    if (item.Date >= dt)
                    {
                        ++model.NewPI;
                        model.NewPIAmt += item.Total ?? 0;
                    }
                    if (item.DueDate < item.ClosingDate)
                    {
                        ++model.OverPI;
                        model.OverPIAmt += item.Total ?? 0;
                    }
                }


                var salesList = connection.List<SalesRow>(q => q
                .SelectTableFields()
                .Select(s.Date)
                .Select(s.ClosingDate)
                .Select(s.DueDate)
                .Select(s.Status)
                .Select(s.Total)
                .Where(s.AssignedId == UserId)
                .Where(s.Date >= dt)
                .Where(s.Date <= dt1));

                model.OpenSales = model.CloseSales = model.NewSales = model.OverSales = 0;
                model.OpenSalesAmt = model.CloseSalesAmt = model.NewSalesAmt = model.OverSalesAmt = 0;
                foreach (var item in salesList)
                {
                    if (item.Status == Masters.StatusMaster.Open)
                    {
                        ++model.OpenSales;
                        model.OpenSalesAmt += item.Total ?? 0;

                    }
                    if (item.Status == Masters.StatusMaster.Closed)
                    {
                        ++model.CloseSales;
                        model.CloseSalesAmt += item.Total ?? 0;
                    }
                    if (item.Date >= dt)
                    {
                        ++model.NewSales;
                        model.NewSalesAmt += item.Total ?? 0;
                    }
                    if (item.DueDate < item.ClosingDate)
                    {
                        ++model.OverSales;
                        model.OverSalesAmt += item.Total ?? 0;
                    }
                }

                var tasksList = connection.List<TasksRow>(q => q
                .SelectTableFields()
                .Select(t.ExpectedCompletion)
                .Select(t.CreationDate)
                .Select(t.CompletionDate)
                .Select(t.StatusId)
                .Where(t.AssignedTo == UserId)
                .Where(t.CreationDate >= dt)
                .Where(t.CreationDate <= dt1));

                model.NewTask = tasksList.Count;
                foreach (var item in tasksList)
                {
                    if (item.StatusId == 2)
                        ++model.CloseTask;

                    if (item.StatusId != 2)
                        ++model.OpenTask;

                    if (item.CompletionDate > item.ExpectedCompletion)
                        ++model.OverTask;
                }

                var cmsList = connection.List<CMSRow>(q => q
                .SelectTableFields()
                .Select(c.ExpectedCompletion)
                .Select(c.Date)
                .Select(c.CompletionDate)
                .Select(c.Status)
                .Where(c.AssignedTo == UserId)
                .Where(c.ExpectedCompletion >= dt)
                .Where(c.ExpectedCompletion <= dt1));

                model.NewCMS = cmsList.Count;
                foreach (var item in cmsList)
                {
                    if (item.Status == Masters.CMSStatusMaster.Closed)
                        ++model.CloseCMS;

                    if (item.Status == Masters.CMSStatusMaster.Open)
                        ++model.OpenCMS;

                    if (item.CompletionDate > item.ExpectedCompletion)
                        ++model.OverCMS;
                }

                var ef = EnquiryFollowupsRow.Fields;
                var qf = QuotationFollowupsRow.Fields;
                var inv = InvoiceFollowupsRow.Fields;
                var tel = TeleCallingFollowupsRow.Fields;
                var cms = CMSFollowupsRow.Fields;

                criteria = "CAST(FollowupDate as DATE) >= " + dt.ToSqlDate() + " AND CAST(FollowupDate as DATE) <= " + dt1.ToSqlDate() + " AND ((t0.ClosingDate IS NULL AND CAST(FollowupDate as DATE) >= " + DateTime.Now.ToSqlDate() + " ) OR (CAST(FollowupDate as DATE) = CAST(t0.ClosingDate as DATE)))";
                model.EnqFollowups = connection.List<EnquiryFollowupsRow>(f => f
                         .SelectTableFields()
                         .Select(ef.FollowupNote)
                         .Select(ef.Details)
                         .Select(ef.Status)
                         .Select(ef.FollowupDate)
                         .Select(ef.ClosingDate)
                         .Select(ef.ContactName)
                         .Where(new Criteria(criteria))
                         .Where(ef.EnquiryAssignedId == UserId)
                         );

                model.QuotFollowups = connection.List<QuotationFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(qf.FollowupNote)
                         .Select(qf.Details)
                         .Select(qf.Status)
                         .Select(qf.FollowupDate)
                         .Select(qf.ClosingDate)
                         .Select(qf.ContactName)
                         .Where(new Criteria(criteria))
                        .Where(qf.QuotationAssignedId == UserId)
                 );

                model.InvoiceFollowups = connection.List<InvoiceFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(inv.FollowupNote)
                         .Select(inv.Details)
                         .Select(inv.Status)
                         .Select(inv.FollowupDate)
                         .Select(inv.ClosingDate)
                         .Select(inv.ContactName)
                         .Where(new Criteria(criteria))
                        .Where(inv.InvoiceAssignedId == UserId)
                 );

                model.TCFollowups = connection.List<TeleCallingFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(tel.FollowupNote)
                         .Select(tel.Details)
                         .Select(tel.Status)
                         .Select(tel.FollowupDate)
                         .Select(tel.ClosingDate)
                         .Select(tel.ContactName)
                         .Where(new Criteria(criteria))
                         .Where(tel.RepresentativeId == UserId)
                         );


                model.CMSFollowups = connection.List<CMSFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(cms.FollowupNote)
                         .Select(cms.Details)
                         .Select(cms.Status)
                         .Select(cms.FollowupDate)
                         .Select(cms.ClosingDate)
                         .Select(cms.ContactName)
                         .Where(new Criteria(criteria))
                         .Where(cms.RepresentativeId == UserId)
                         );

                criteria = "CAST(FollowupDate as DATE) >= " + dt.ToSqlDate() + " AND CAST(FollowupDate as DATE) <= " + dt1.ToSqlDate() + " AND ((t0.ClosingDate IS NULL AND CAST(FollowupDate as DATE) < " + DateTime.Now.ToSqlDate() + " ) OR (CAST(FollowupDate as DATE) < CAST(t0.ClosingDate as DATE)))";

                model.ODEnqFollowups = connection.List<EnquiryFollowupsRow>(f => f
                         .SelectTableFields()
                         .Select(ef.FollowupNote)
                         .Select(ef.Details)
                         .Select(ef.Status)
                         .Select(ef.FollowupDate)
                         .Select(ef.ClosingDate)
                         .Select(ef.ContactName)
                         .Where(new Criteria(criteria))
                        .Where(ef.EnquiryAssignedId == UserId)
                 );

                model.ODQuotFollowups = connection.List<QuotationFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(qf.FollowupNote)
                         .Select(qf.Details)
                         .Select(qf.Status)
                         .Select(qf.FollowupDate)
                         .Select(qf.ClosingDate)
                         .Select(qf.ContactName)
                         .Where(new Criteria(criteria))
                        .Where(qf.QuotationAssignedId == UserId)
                 );

                model.ODInvoiceFollowups = connection.List<InvoiceFollowupsRow>(f => f
                         .SelectTableFields()
                         .Select(inv.FollowupNote)
                         .Select(inv.Details)
                         .Select(inv.Status)
                         .Select(inv.FollowupDate)
                         .Select(inv.ClosingDate)
                         .Select(inv.ContactName)
                         .Where(new Criteria(criteria))
                        .Where(inv.InvoiceAssignedId == UserId)
                 );

                model.ODTCFollowups = connection.List<TeleCallingFollowupsRow>(f => f
                         .SelectTableFields()
                         .Select(tel.FollowupNote)
                         .Select(tel.Details)
                         .Select(tel.Status)
                         .Select(tel.FollowupDate)
                         .Select(tel.ClosingDate)
                         .Select(tel.ContactName)
                         .Where(new Criteria(criteria))
                        .Where(tel.RepresentativeId == UserId));

                model.ODCMSFollowups = connection.List<CMSFollowupsRow>(f => f
                         .SelectTableFields()
                         .Select(cms.FollowupNote)
                         .Select(cms.Details)
                         .Select(cms.Status)
                         .Select(cms.FollowupDate)
                         .Select(cms.ClosingDate)
                         .Select(cms.ContactName)
                         .Where(new Criteria(criteria))
                        .Where(cms.RepresentativeId == UserId)
                 );
            }
            return PartialView(MVC.Views.Common.Dashboard.DSRDashboardData, model);
        }
    }
}
