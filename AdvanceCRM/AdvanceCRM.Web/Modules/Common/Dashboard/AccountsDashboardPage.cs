using AdvanceCRM.Contacts;
using AdvanceCRM.Web.Helpers;
using AdvanceCRM.Contacts.Endpoints;
using AdvanceCRM.Contacts;
using AdvanceCRM.Contacts.Repositories;

using AdvanceCRM.Enquiry;
using AdvanceCRM.Quotation;
using AdvanceCRM.Sales;
using AdvanceCRM.Services;
using Serenity;
using Serenity.Data;
using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AdvanceCRM.Common.Calendar;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Serenity.Services;
namespace AdvanceCRM.Common.Pages
{
    [Route("AccountsDashboard")]
    public class AccountsDashboardController : Controller
    {
        private readonly ISqlConnections _connections;
        private readonly IMemoryCache _cache;
        private readonly ILogger<CalendarController> _logger;
        private readonly IRequestContext Context;
        private readonly IAuthenticationService _authenticationService;

        public AccountsDashboardController(ISqlConnections connections, IRequestContext context, IMemoryCache cache, ILogger<CalendarController> logger, IAuthenticationService authenticationService)
        {
            _connections = connections;
            _cache = cache;
            _logger = logger;
            Context = context ?? throw new ArgumentNullException(nameof(context));
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }
        [Authorize, HttpGet, Route("~/Accounts")]
        public ActionResult Index()
        {
            var cachedModel = LocalCache.GetLocalStoreOnly("AccountsDashboardModel", TimeSpan.FromSeconds(60),
                         ContactsRow.Fields.GenerationKey, () =>
                         {
                             var model = new AccountsDashboardPageModel();
                             //var user = (UserDefinition)Context.User.ToUserDefinition();
                             var user = Context.User.ToUserDefinition();
                             var Cont = ContactsRow.Fields;

                             using (var connection = _connections.NewFor<ContactsRow>())
                             {
                                 var c = ContactsRow.Fields;
                                 model.ContactList = connection.List<ContactsRow>(q => q
                                 .SelectTableFields()
                                 .Select(c.Name));
                             }
                             return model;
                         });
            ViewBag.ListData = cachedModel.ContactList;
            return View(MVC.Views.Common.Dashboard.AccountsDashboardIndex, cachedModel);
        }


        public ActionResult LoadData(int ContactId)
        {
            var model = new AccountsDashboardPageModel();
            var enq = EnquiryRow.Fields;
            var quot = QuotationRow.Fields;
            var Sal = SalesRow.Fields;
            var cms = CMSRow.Fields;
            var amc = AMCVisitPlannerRow.Fields;

            using (var connection = _connections.NewFor<EnquiryRow>())
            {

                //Enquiry
                model.EnquiryWon = connection.Count<EnquiryRow>(
                enq.ClosingType == 1 &&


                enq.ContactsId == ContactId
                );

                model.EnquiryLost = connection.Count<EnquiryRow>(
                enq.ClosingType == 2 &&


                enq.ContactsId == ContactId
                );

                model.EnquiryStatusOpen = connection.Count<EnquiryRow>(
                  enq.Status == 1
                  );

                model.EnquiryStatusClosed = connection.Count<EnquiryRow>(
                    enq.Status == 2
                    );

                model.EnquiryStatusPending = connection.Count<EnquiryRow>(
                    enq.Status == 3
                    );

                model.TotalEnquiry = connection.Count<EnquiryRow>(


                 enq.ContactsId == ContactId
                );

                model.TotalEnquiryList = connection.List<EnquiryRow>(us => us
                         .SelectTableFields()
                         .Select(enq.Id)
                         .Select(enq.Date)
                         .Select(enq.AssignedUsername)
                         .Select(enq.Total)
                         .Where(enq.ContactsId == ContactId)
                         );

                model.EnquiryWonList = connection.List<EnquiryRow>(us => us
                        .SelectTableFields()
                        .Select(enq.Id)
                        .Select(enq.Date)
                        .Select(enq.AssignedUsername)
                        .Select(enq.Total)
                        .Where(enq.ContactsId == ContactId)
                        .Where(enq.ClosingType == 1)
                        );

                model.EnquiryLostList = connection.List<EnquiryRow>(us => us
                       .SelectTableFields()
                       .Select(enq.Id)
                       .Select(enq.Date)
                       .Select(enq.AssignedUsername)
                       .Select(enq.Total)
                       .Where(enq.ContactsId == ContactId)
                       .Where(enq.ClosingType == 2)
                       );

                model.EnquiryStatusOpenList = connection.List<EnquiryRow>(us => us
                       .SelectTableFields()
                       .Select(enq.Id)
                       .Select(enq.Date)
                       .Select(enq.AssignedUsername)
                       .Select(enq.Total)
                       .Where(enq.ContactsId == ContactId)
                       .Where(enq.Status == 1)
                       );

                model.EnquiryStatusClosedList = connection.List<EnquiryRow>(us => us
                       .SelectTableFields()
                       .Select(enq.Id)
                       .Select(enq.Date)
                       .Select(enq.AssignedUsername)
                       .Select(enq.Total)
                       .Where(enq.ContactsId == ContactId)
                       .Where(enq.Status == 2)
                       );

                model.EnquiryStatusPendingList = connection.List<EnquiryRow>(us => us
                       .SelectTableFields()
                       .Select(enq.Id)
                       .Select(enq.Date)
                       .Select(enq.AssignedUsername)
                       .Select(enq.Total)
                       .Where(enq.ContactsId == ContactId)
                       .Where(enq.Status == 3)
                       );

                //Quotation

                model.QuotationWon = connection.Count<QuotationRow>(
                quot.ClosingType == 1 &&


                quot.ContactsId == ContactId
                );

                model.QuotationLost = connection.Count<QuotationRow>(
                quot.ClosingType == 2 &&


                quot.ContactsId == ContactId
                );

                model.QuotationStatusOpen = connection.Count<QuotationRow>(
                  quot.Status == 1
                  );

                model.QuotationStatusClosed = connection.Count<QuotationRow>(
                    quot.Status == 2
                    );

                model.QuotationStatusPending = connection.Count<QuotationRow>(
                    quot.Status == 3
                    );

                model.TotalQuotation = connection.Count<QuotationRow>(


                 quot.ContactsId == ContactId
                );

                model.TotalQuotationList = connection.List<QuotationRow>(us => us
                         .SelectTableFields()
                         .Select(quot.Id)
                         .Select(quot.Date)
                         .Select(quot.AssignedUsername)
                         .Select(quot.Total)
                         .Where(quot.ContactsId == ContactId)
                         );

                model.QuotationWonList = connection.List<QuotationRow>(us => us
                        .SelectTableFields()
                        .Select(quot.Id)
                        .Select(quot.Date)
                        .Select(quot.AssignedUsername)
                        .Select(quot.Total)
                        .Where(quot.ContactsId == ContactId)
                        .Where(quot.ClosingType == 1)
                        );

                model.QuotationLostList = connection.List<QuotationRow>(us => us
                       .SelectTableFields()
                       .Select(quot.Id)
                       .Select(quot.Date)
                       .Select(quot.AssignedUsername)
                       .Select(quot.Total)
                       .Where(quot.ContactsId == ContactId)
                       .Where(quot.ClosingType == 2)
                       );

                model.QuotationStatusOpenList = connection.List<QuotationRow>(us => us
                       .SelectTableFields()
                       .Select(quot.Id)
                       .Select(quot.Date)
                       .Select(quot.AssignedUsername)
                       .Select(quot.Total)
                       .Where(quot.ContactsId == ContactId)
                       .Where(quot.Status == 1)
                       );

                model.QuotationStatusClosedList = connection.List<QuotationRow>(us => us
                       .SelectTableFields()
                       .Select(quot.Id)
                       .Select(quot.Date)
                       .Select(quot.AssignedUsername)
                       .Select(quot.Total)
                       .Where(quot.ContactsId == ContactId)
                       .Where(quot.Status == 2)
                       );

                model.QuotationStatusPendingList = connection.List<QuotationRow>(us => us
                       .SelectTableFields()
                       .Select(quot.Id)
                       .Select(quot.Date)
                       .Select(quot.AssignedUsername)
                       .Select(quot.Total)
                       .Where(quot.ContactsId == ContactId)
                       .Where(quot.Status == 3)
                       );

                //Sales

                //Total Sales Count
                model.TotalSales = connection.Count<SalesRow>(


                    Sal.ContactsId == ContactId
                    );

                //Sales Status Open   
                model.SalesStatusOpen = connection.Count<SalesRow>(
                    Sal.Status == 1 &&


                    Sal.ContactsId == ContactId
                    );

                //Sales Status Close
                model.SalesStatusClosed = connection.Count<SalesRow>(
                    Sal.Status == 2 &&


                    Sal.ContactsId == ContactId
                    );

                //Sales Status Pending
                model.SalesStatusPending = connection.Count<SalesRow>(
                    Sal.Status == 3 &&


                    Sal.ContactsId == ContactId
                    );

                model.SalesCashCount = connection.Count<SalesRow>(
                    Sal.Type == 1 &&


                    Sal.ContactsId == ContactId
                    );

                model.SalesCreditCount = connection.Count<SalesRow>(
                   Sal.Type == 2 &&


                   Sal.ContactsId == ContactId
                   );

                model.TotalSalesList = connection.List<SalesRow>(us => us
                        .SelectTableFields()
                        .Select(Sal.Id)
                        .Select(Sal.Date)
                        .Select(Sal.AssignedUsername)
                        .Select(Sal.Total)
                        .Where(Sal.ContactsId == ContactId)
                        );

                model.SalesStatusOpenList = connection.List<SalesRow>(us => us
                      .SelectTableFields()
                      .Select(Sal.Id)
                      .Select(Sal.Date)
                      .Select(Sal.AssignedUsername)
                      .Select(Sal.Total)
                      .Where(Sal.ContactsId == ContactId)
                      .Where(Sal.Status == 1)
                      );

                model.SalesStatusClosedList = connection.List<SalesRow>(us => us
                       .SelectTableFields()
                       .Select(Sal.Id)
                       .Select(Sal.Date)
                       .Select(Sal.AssignedUsername)
                       .Select(Sal.Total)
                       .Where(Sal.ContactsId == ContactId)
                       .Where(Sal.Status == 2)
                       );

                model.SalesStatusPendingList = connection.List<SalesRow>(us => us
                       .SelectTableFields()
                       .Select(Sal.Id)
                       .Select(Sal.Date)
                       .Select(Sal.AssignedUsername)
                       .Select(Sal.Total)
                       .Where(Sal.ContactsId == ContactId)
                       .Where(Sal.Status == 3)
                       );

                model.SalesCashList = connection.List<SalesRow>(us => us
                      .SelectTableFields()
                      .Select(Sal.Id)
                      .Select(Sal.Date)
                      .Select(Sal.AssignedUsername)
                      .Select(Sal.Total)
                      .Where(Sal.ContactsId == ContactId)
                      .Where(Sal.Type == 1)
                      );
                model.SalesCreditList = connection.List<SalesRow>(us => us
                      .SelectTableFields()
                      .Select(Sal.Id)
                      .Select(Sal.Date)
                      .Select(Sal.AssignedUsername)
                      .Select(Sal.Total)
                      .Where(Sal.ContactsId == ContactId)
                      .Where(Sal.Type == 2)
                      );

                //CMS
                model.TotalCMSList = connection.List<CMSRow>(us => us
                       .SelectTableFields()
                       .Select(cms.Id)
                       .Select(cms.Date)
                       .Select(cms.AssignedToUsername)
                       .Select(cms.ProductsName)
                       .Where(cms.ContactsId == ContactId)
                       );

                model.CMSStatusOpenList = connection.List<CMSRow>(us => us
                      .SelectTableFields()
                      .Select(cms.Id)
                      .Select(cms.Date)
                      .Select(cms.AssignedToUsername)
                      .Select(cms.ProductsName)
                      .Where(cms.ContactsId == ContactId)
                      .Where(cms.Status == 1)
                      );

                model.CMSStatusClosedList = connection.List<CMSRow>(us => us
                       .SelectTableFields()
                       .Select(cms.Id)
                       .Select(cms.Date)
                       .Select(cms.AssignedToUsername)
                       .Select(cms.ProductsName)
                       .Where(cms.ContactsId == ContactId)
                       .Where(cms.Status == 2)
                       );

                model.CMSStatusPendingList = connection.List<CMSRow>(us => us
                       .SelectTableFields()
                       .Select(cms.Id)
                       .Select(cms.Date)
                       .Select(cms.AssignedToUsername)
                       .Select(cms.ProductsName)
                       .Where(cms.ContactsId == ContactId)
                       .Where(cms.Status == 3)
                       );

                //AMC

                model.TotalAMCList = connection.List<AMCVisitPlannerRow>(us => us
                       .SelectTableFields()
                       .Select(amc.AMCId)
                       .Select(amc.VisitDate)
                       .Select(amc.AssignedToUsername)
                       .Where(amc.AMCContactsId == ContactId)
                       );

                model.AMCVisitsOpenList = connection.List<AMCVisitPlannerRow>(us => us
                      .SelectTableFields()
                      .Select(amc.AMCId)
                      .Select(amc.VisitDate)
                      .Select(amc.AssignedToUsername)
                      .Select(amc.VisitDetails)
                      .Where(amc.Status == 1)
                      .Where(amc.AMCContactsId == ContactId)
                      );

                model.AMCVisitsClosedList = connection.List<AMCVisitPlannerRow>(us => us
                      .SelectTableFields()
                      .Select(amc.AMCId)
                      .Select(amc.VisitDate)
                      .Select(amc.AssignedToUsername)
                      .Select(amc.VisitDetails)
                      .Select(amc.CompletionDate)
                      .Where(amc.Status == 2)
                      .Where(amc.AMCContactsId == ContactId)
                      );

                model.AMCVisitsPendingList = connection.List<AMCVisitPlannerRow>(us => us
                    .SelectTableFields()
                    .Select(amc.AMCId)
                    .Select(amc.VisitDate)
                    .Select(amc.AssignedToUsername)
                    .Select(amc.VisitDetails)
                    .Where(amc.Status == 3)
                    .Where(amc.AMCContactsId == ContactId)
                    );

            }

            return PartialView(MVC.Views.Common.Dashboard.AccountsDashboardPartialView, model);
        }
    }
}