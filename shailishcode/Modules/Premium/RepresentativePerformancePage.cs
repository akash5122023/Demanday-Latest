using AdvanceCRM.Administration;
using AdvanceCRM.Web.Helpers;

using AdvanceCRM.Enquiry;
using AdvanceCRM.Quotation;
using AdvanceCRM.Sales;
using Newtonsoft.Json;
using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Modules.Premium
{
    [Route("RepresentativePerformance")]
    [ReadPermission("Premium:Dashboards")]
    public class RepresentativePerformanceController : Controller
    {
        private readonly ISqlConnections _connections;
        private readonly IRequestContext Context;

        public RepresentativePerformanceController(ISqlConnections connections, IRequestContext context)
        {
            _connections = connections ?? throw new ArgumentNullException(nameof(connections));
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet, Route("~/Premium/RepresentativePerformance")]
        public ActionResult Index()
        {
            var cachedModel = LocalCache.GetLocalStoreOnly("RepresentativePerformancePageModel", TimeSpan.FromSeconds(1),
                      SalesRow.Fields.GenerationKey, () =>
                      {
                          var model = new RepresentativePerformancePageModel();
                          var user = (UserDefinition)Context.User.ToUserDefinition();
                          var Sal = SalesRow.Fields;
                          var SalP = SalesProductsRow.Fields;
                          var Usr = UserRow.Fields;
                          var Enq = EnquiryRow.Fields;
                          var Quot = QuotationRow.Fields;

                          using (var connection = _connections.NewFor<EnquiryRow>())
                          {
                              model.EnquiryList = connection.List<EnquiryRow>(us => us
                              .SelectTableFields()
                              .Select(Enq.Id)
                              .Select(Enq.AssignedId)
                              .Select(Enq.AssignedUsername)
                              .Select(Enq.AssignedDisplayName)
                              .Where(Enq.Date >= DateTime.Now.AddMonths(-1))
                              .Where(Enq.Date <= DateTime.Now)
                              );

                              model.SalesList = connection.List<SalesRow>(us => us
                              .SelectTableFields()
                              .Select(Sal.Id)
                              .Select(Sal.AssignedId)
                              .Select(Sal.AssignedUsername)
                              .Select(Sal.AssignedDisplayName)
                              .Select(Sal.AssignedUserImage)
                              .Select(Sal.Total)
                              .Where(Sal.Date >= DateTime.Now.AddMonths(-1))
                              .Where(Sal.Date <= DateTime.Now)
                              );

                              model.QuotationList = connection.List<QuotationRow>(us => us
                              .SelectTableFields()
                              .Select(Quot.Id)
                              .Select(Quot.AssignedId)
                              .Select(Quot.AssignedUsername)
                              .Select(Quot.AssignedDisplayName)
                              .Select(Quot.AssignedUserImage)
                              .Where(Quot.Date >= DateTime.Now.AddMonths(-1))
                              .Where(Quot.Date <= DateTime.Now)
                              );

                          }
                          return model;
                      });
            return View(MVC.Views.Premium.RepresentativePerformanceDashboard, cachedModel);
        }

        //Representative Enquiry Chart on date change
        [HttpGet]
        public ActionResult RepresentativeEnquiryChart(string StartDate, string EndDate)
        {
            var model = new RepresentativePerformancePageModel();

            if (StartDate == null)
            {
                model.StartDate = DateTime.Now.AddMonths(-1);
                model.EndDate = DateTime.Now;
            }
            else
            {
                CultureInfo culture = new CultureInfo("en-US");
                model.StartDate = Convert.ToDateTime(StartDate, culture);
                model.EndDate = Convert.ToDateTime(EndDate, culture);
            }
            var user = (UserDefinition)Context.User.ToUserDefinition();
            var Enq = EnquiryRow.Fields;

            using (var connection = _connections.NewFor<EnquiryRow>())
            {

                model.EnquiryList = connection.List<EnquiryRow>(us => us
                           .SelectTableFields()
                           .Select(Enq.Id)
                           .Select(Enq.AssignedId)
                           .Select(Enq.AssignedUsername)
                           .Select(Enq.AssignedDisplayName)
                           .Where(Enq.Date >= model.StartDate)
                           .Where(Enq.Date <= model.EndDate)
                           );

            }
            List<RepresentativeEnquiryClass> lst = new List<RepresentativeEnquiryClass>();
            foreach (var item in model.EnquiryList.GroupBy(info => info.AssignedUsername).Select(group => new
            {
                Name = group.Key,
                Count = group.Count()
            }))
            {
                var y = new RepresentativeEnquiryClass { country = item.Name, visits = item.Count };
                lst.Add(y);
            }
            return Json(lst);
        }

        public class RepresentativeEnquiryClass
        {
            [JsonProperty("country")]
            public string country { get; set; }
            [JsonProperty("visits")]
            public int visits { get; set; }
        }

        //Representative Sales Chart on date change
        [HttpGet]
        public ActionResult RepresentativeSalesChart(string StartDate, string EndDate)
        {
            var model = new RepresentativePerformancePageModel();

            if (StartDate == null)
            {
                model.StartDate = DateTime.Now.AddMonths(-1);
                model.EndDate = DateTime.Now;
            }
            else
            {
                CultureInfo culture = new CultureInfo("en-US");
                model.StartDate = Convert.ToDateTime(StartDate, culture);
                model.EndDate = Convert.ToDateTime(EndDate, culture);
            }
            var user = (UserDefinition)Context.User.ToUserDefinition();
            var Sal = SalesRow.Fields;

            using (var connection = _connections.NewFor<SalesRow>())
            {

                model.SalesList = connection.List<SalesRow>(us => us
                           .SelectTableFields()
                           .Select(Sal.Id)
                           .Select(Sal.AssignedId)
                           .Select(Sal.AssignedUsername)
                           .Select(Sal.AssignedDisplayName)
                           .Select(Sal.AssignedUserImage)
                           .Select(Sal.Total)
                           .Where(Sal.Date >= model.StartDate)
                           .Where(Sal.Date <= model.EndDate)
                           );

            }
            List<RepresentativeSalesClass> lst = new List<RepresentativeSalesClass>();
            var s_total = model.SalesList.Sum(x => x.Total);
            foreach (var item in model.SalesList.GroupBy(info => info.AssignedUsername).Select(group => new
            {
                Name = group.Key,
                Count = group.Count(),
                Sum = (int)group.Sum(x => x.Total),
                //"actvalue": @line.Sum
                //Bullet= Server.MapPath("~/App_Data/upload/" + group.Select(x=>x.AssignedUserImage).FirstOrDefault())
            }))
            {
                //var y = new RepresentativeSalesClass { name = item.Name, points = item.Sum ,bullet=item.Bullet };

                var per = ((item.Sum / s_total) * 100) * 1000;

                var y = new RepresentativeSalesClass { name = item.Name, points = (int)per, actvalue = item.Sum };
                lst.Add(y);
            }
            return Json(lst);
        }

        public class RepresentativeSalesClass
        {
            [JsonProperty("name")]
            public string name { get; set; }
            [JsonProperty("points")]
            public int points { get; set; }
            [JsonProperty("actvalue")]
            public int actvalue { get; set; }
            //[JsonProperty("bullet")]
            //public string bullet { get; set; }
        }

        //"name": "John",
        //"points": 65456,
        //"color": "#7F8DA9",
        //"bullet": "https://www.amcharts.com/lib/images/faces/A04.png"


        //Representative Quotation Chart on date change
        [HttpGet]
        public ActionResult RepresentativeQuotationChart(string StartDate, string EndDate)
        {
            var model = new RepresentativePerformancePageModel();

            if (StartDate == null)
            {
                model.StartDate = DateTime.Now.AddMonths(-1);
                model.EndDate = DateTime.Now;
            }
            else
            {
                CultureInfo culture = new CultureInfo("en-US");
                model.StartDate = Convert.ToDateTime(StartDate, culture);
                model.EndDate = Convert.ToDateTime(EndDate, culture);
            }
            var user = (UserDefinition)Context.User.ToUserDefinition();
            var Quot = QuotationRow.Fields;

            using (var connection = _connections.NewFor<QuotationRow>())
            {

                model.QuotationList = connection.List<QuotationRow>(us => us
                            .SelectTableFields()
                            .Select(Quot.Id)
                            .Select(Quot.AssignedId)
                            .Select(Quot.AssignedUsername)
                            .Select(Quot.AssignedDisplayName)
                            .Select(Quot.AssignedUserImage)
                            .Where(Quot.Date >= model.StartDate)
                            .Where(Quot.Date <= model.EndDate)
                            );

            }
            List<RepresentativeQuotationClass> lst = new List<RepresentativeQuotationClass>();
            foreach (var item in model.QuotationList.GroupBy(info => info.AssignedUsername).Select(group => new
            {
                Name = group.Key,
                Count = group.Count()
            }))
            {
                var y = new RepresentativeQuotationClass { country = item.Name, visits = item.Count };
                lst.Add(y);
            }
            return Json(lst);
        }

        public class RepresentativeQuotationClass
        {
            [JsonProperty("country")]
            public string country { get; set; }
            [JsonProperty("visits")]
            public int visits { get; set; }
        }



    }
}