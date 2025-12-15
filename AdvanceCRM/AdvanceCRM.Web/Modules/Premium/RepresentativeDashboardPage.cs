using AdvanceCRM.Administration;
using AdvanceCRM.Web.Helpers;

using AdvanceCRM.Enquiry;
using AdvanceCRM.Masters;
using AdvanceCRM.Premium;
using AdvanceCRM.Products;
using AdvanceCRM.Quotation;
using AdvanceCRM.Sales;
using Newtonsoft.Json;
using Serenity;
using Serenity.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Serenity.Services;

namespace AdvanceCRM.Modules.Premium
{
    [Route("RepresentativeDashboard")]
    [ReadPermission("Premium:Dashboards")]
    public class RepresentativeDashboardController : Controller
    {
        private readonly ISqlConnections _connections;
        private readonly IRequestContext Context;

        public RepresentativeDashboardController(ISqlConnections connections,IRequestContext context)
        {
            _connections = connections ?? throw new ArgumentNullException(nameof(connections));
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet, Route("~/Premium/Representative")]
        public ActionResult Index()
        {
            var model = new RepresentativeDashboardPageModel();
            var cachedModel = LocalCache.GetLocalStoreOnly("RepresentativeDashboardPageModel", TimeSpan.FromSeconds(1),
                    SalesRow.Fields.GenerationKey, () =>
                    {

                        var user = (UserDefinition)Context.User.ToUserDefinition();
                        var Sal = SalesRow.Fields;
                        var SalP = SalesProductsRow.Fields;
                        var Usr = UserRow.Fields;
                        var enq = EnquiryRow.Fields;
                        var enqP = EnquiryProductsRow.Fields;
                        var product = ProductsRow.Fields;
                        var Quot = QuotationRow.Fields;
                        var QuotP = QuotationProductsRow.Fields;

                        using (var connection = _connections.NewFor<SalesRow>())
                        {

                            //Representative List
                            model.RepresentativeList = connection.List<UserRow>(us => us
                              .SelectTableFields()
                              .Select(Usr.UserId)
                              .Select(Usr.Username)
                              );
                            ViewBag.ListData = model.RepresentativeList;

                        }
                        return model;
                    });
            return View(MVC.Views.Premium.RepresentativeDashboard, model);
        }


        [HttpGet]
        public ActionResult EnquiryStagesFunnel(string StartDate, string EndDate, int ListId)
        {

            var model = new RepresentativeDashboardPageModel();

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
            var enq = EnquiryRow.Fields;
            using (var connection = _connections.NewFor<EnquiryRow>())
            {
                model.EnquiryStages = connection.List<EnquiryRow>(us => us
                   .SelectTableFields()
                   .Select(enq.Stage)
                   .Select(enq.Date)
                   .Select(enq.Total)
                   .Where(enq.AssignedId == ListId)
                   .Where(enq.Date >= model.StartDate)
                   .Where(enq.Date <= model.EndDate)
                   );

            }
            List<EnquiryStagesClass> lst = new List<EnquiryStagesClass>();

            foreach (var item in model.EnquiryStages.GroupBy(y => y.Stage).Select(y => new { Stage = y.Key, Count = y.Count(), Sum = ((int)y.Sum(x => x.Total)).ToString("0,0##.##") }).OrderByDescending(z => z.Count).ToList())
            {
                var y = new EnquiryStagesClass { title = item.Stage, value = item.Count, sum = item.Sum };

                lst.Add(y);
            }

            return Json(lst);
        }

        public class EnquiryStagesClass
        {
            [JsonProperty("title")]
            public string title { get; set; }
            [JsonProperty("value")]
            public int value { get; set; }
            [JsonProperty("sum")]
            public string sum { get; set; }
        }

        //Representative Quotation Stages FunnelChart On Date Change
        [HttpGet]
        public ActionResult QuotationStagesFunnelChart(string StartDate, string EndDate, int ListId)
        {

            var model = new RepresentativeDashboardPageModel();

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
            var enq = EnquiryRow.Fields;
            using (var connection = _connections.NewFor<QuotationRow>())
            {
                model.QuotationStages = connection.List<QuotationRow>(us => us
                  .SelectTableFields()
                  .Select(enq.Stage)
                  .Select(enq.Date)
                  .Select(enq.Total)
                  .Where(enq.AssignedId == ListId)
                  .Where(enq.Date >= model.StartDate)
                  .Where(enq.Date <= model.EndDate)
                  );

            }
            List<QuotationStagesClass> lst = new List<QuotationStagesClass>();

            foreach (var item in model.QuotationStages.GroupBy(y => y.Stage).Select(y => new { Stage = y.Key, Count = y.Count(), Sum = ((int)y.Sum(x => x.Total)).ToString("0,0##.##") }).OrderByDescending(z => z.Count).ToList())
            {
                var y = new QuotationStagesClass { title = item.Stage, value = item.Count, sum = item.Sum };

                lst.Add(y);
            }

            return Json(lst);
        }

        public class QuotationStagesClass
        {
            [JsonProperty("title")]
            public string title { get; set; }
            [JsonProperty("value")]
            public int value { get; set; }
            [JsonProperty("sum")]
            public string sum { get; set; }
        }

        //Representative Most Enquired Product(15)  On Date Change
        [HttpGet]
        public ActionResult MostEnquiredProductChart(string StartDate, string EndDate, int ListId)
        {
            var model = new RepresentativeDashboardPageModel();

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
            var enqP = EnquiryProductsRow.Fields;

            using (var connection = _connections.NewFor<EnquiryProductsRow>())
            {
                model.GroupMostEnquiryProduct = connection.List<EnquiryProductsRow>(us => us
                          .SelectTableFields()
                          .Select(enqP.ProductsName)
                          .Select(enqP.EnquiryDate)
                          .Where(enqP.EnquiryAssignedId == ListId)
                          .Where(enqP.EnquiryDate >= model.StartDate)
                          .Where(enqP.EnquiryDate <= model.EndDate)

                          );
            }
            List<MostEnquiryClass> lst = new List<MostEnquiryClass>();
            foreach (var item in model.GroupMostEnquiryProduct.GroupBy(info => info.ProductsName).Select(group => new
            {
                Name = group.Key,
                Count = group.Count(),
                Sum = group.Sum(x => x.Quantity)
            }).OrderByDescending(x => x.Count).Take(15))
            {
                var y = new MostEnquiryClass { country = item.Name, visits = item.Count };
                lst.Add(y);
            }
            return Json(lst);
        }

        public class MostEnquiryClass
        {
            [JsonProperty("country")]
            public string country { get; set; }
            [JsonProperty("visits")]
            public int visits { get; set; }
        }

        //Most 15 Sales Product Chart on Date Change
        [HttpGet]
        public ActionResult MostSalesProductChart(string StartDate, string EndDate, int ListId)
        {
            var model = new RepresentativeDashboardPageModel();

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
            var SalP = SalesProductsRow.Fields;

            using (var connection = _connections.NewFor<SalesProductsRow>())
            {
                model.GroupMostSalesProduct = connection.List<SalesProductsRow>(us => us
                          .SelectTableFields()
                          .Select(SalP.ProductsName)
                          .Select(SalP.SalesDate)
                          .Where(SalP.SalesAssignedId == ListId)
                          .Where(SalP.SalesDate >= model.StartDate)
                          .Where(SalP.SalesDate <= model.EndDate)
                          );
            }
            List<MostSalesClass> lst = new List<MostSalesClass>();
            foreach (var item in model.GroupMostSalesProduct.GroupBy(info => info.ProductsName).Select(group => new
            {
                Name = group.Key,
                Count = group.Count(),
                Sum = group.Sum(x => x.Quantity)
            }).OrderByDescending(x => x.Count).Take(15))
            {
                var y = new MostSalesClass { country = item.Name, visits = item.Count };
                lst.Add(y);
            }
            return Json(lst);
        }

        public class MostSalesClass
        {
            [JsonProperty("country")]
            public string country { get; set; }
            [JsonProperty("visits")]
            public int visits { get; set; }
        }

        //Representative EnquiryStatus on Date Change
        [HttpGet]
        public ActionResult EnquiryStatus(string StartDate, string EndDate, int ListId)
        {
            var model = new RepresentativeDashboardPageModel();

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
            var enq = EnquiryRow.Fields;

            using (var connection = _connections.NewFor<EnquiryRow>())
            {


                model.EnquiryStatusOpen = connection.Count<EnquiryRow>(
                   enq.Status == 1 &&
                   enq.Date >= model.StartDate &&
                   enq.Date <= model.EndDate && enq.AssignedId == ListId
                   );

                model.EnquiryStatusClosed = connection.Count<EnquiryRow>(
                    enq.Status == 2 &&
                    enq.Date >= model.StartDate &&
                    enq.Date <= model.EndDate && enq.AssignedId == ListId
                    );

                model.EnquiryStatusPending = connection.Count<EnquiryRow>(
                    enq.Status == 3 &&
                    enq.Date >= model.StartDate &&
                    enq.Date <= model.EndDate && enq.AssignedId == ListId
                    );

            }
            List<EnquiryStatusClass> lstEnquiryStatus = new List<EnquiryStatusClass>();

            var EnquiryStatus = new EnquiryStatusClass { title = "Open", value = model.EnquiryStatusOpen };
            lstEnquiryStatus.Add(EnquiryStatus);

            EnquiryStatus = new EnquiryStatusClass { title = "Closed", value = model.EnquiryStatusClosed };
            lstEnquiryStatus.Add(EnquiryStatus);

            EnquiryStatus = new EnquiryStatusClass { title = "Pending", value = model.EnquiryStatusPending };
            lstEnquiryStatus.Add(EnquiryStatus);

            if ((model.EnquiryStatusOpen == 0) && (model.EnquiryStatusClosed == 0) && (model.EnquiryStatusPending == 0))
            {
                return Json("");
            }
            else
            {
                return Json(lstEnquiryStatus);
            }
        }

        public class EnquiryStatusClass
        {
            [JsonProperty("title")]
            public string title { get; set; }
            [JsonProperty("value")]
            public int value { get; set; }
        }


        //Representative QuotationStatus on Date Change
        [HttpGet]
        public ActionResult QuotationStatusChart(string StartDate, string EndDate, int ListId)
        {
            var model = new RepresentativeDashboardPageModel();

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
                model.QuotationStatusOpen = connection.Count<QuotationRow>(
                   Quot.Status == 1 &&
                   Quot.Date >= model.StartDate &&
                   Quot.Date <= model.EndDate && Quot.AssignedId == ListId
                   );

                model.QuotationStatusClosed = connection.Count<QuotationRow>(
                    Quot.Status == 2 &&
                    Quot.Date >= model.StartDate &&
                    Quot.Date <= model.EndDate && Quot.AssignedId == ListId
                    );

                model.QuotationStatusPending = connection.Count<QuotationRow>(
                    Quot.Status == 3 &&
                    Quot.Date >= model.StartDate &&
                    Quot.Date <= model.EndDate && Quot.AssignedId == ListId
                    );

            }
            List<QuotationStatusClass> lstQuotationStatusClass = new List<QuotationStatusClass>();

            var QuotationStatus = new QuotationStatusClass { title = "Open", value = model.QuotationStatusOpen };
            lstQuotationStatusClass.Add(QuotationStatus);

            QuotationStatus = new QuotationStatusClass { title = "Closed", value = model.QuotationStatusClosed };
            lstQuotationStatusClass.Add(QuotationStatus);

            QuotationStatus = new QuotationStatusClass { title = "Pending", value = model.QuotationStatusPending };
            lstQuotationStatusClass.Add(QuotationStatus);

            if ((model.QuotationStatusOpen == 0) && (model.QuotationStatusClosed == 0) && (model.QuotationStatusPending == 0))
            {
                return Json("");
            }
            else
            {
                return Json(lstQuotationStatusClass);
            }
        }

        public class QuotationStatusClass
        {
            [JsonProperty("title")]
            public string title { get; set; }
            [JsonProperty("value")]
            public int value { get; set; }
        }

        //Representative  SalesStatus on Date Change
        [HttpGet]
        public ActionResult SalesStatusChart(string StartDate, string EndDate, int ListId)
        {
            var model = new RepresentativeDashboardPageModel();

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
                model.SalesStatusOpen = connection.Count<SalesRow>(
                   Sal.Status == 1 &&
                   Sal.Date >= model.StartDate &&
                   Sal.Date <= model.EndDate &&
                   Sal.AssignedId == ListId
                   );

                model.SalesStatusClosed = connection.Count<SalesRow>(
                    Sal.Status == 2 &&
                    Sal.Date >= model.StartDate &&
                    Sal.Date <= model.EndDate &&
                    Sal.AssignedId == ListId
                    );

                model.SalesStatusPending = connection.Count<SalesRow>(
                    Sal.Status == 3 &&
                    Sal.Date >= model.StartDate &&
                    Sal.Date <= model.EndDate &&
                    Sal.AssignedId == ListId
                    );

            }
            List<SalesStatusClass> lstSalesStatus = new List<SalesStatusClass>();

            var SalesStatus = new SalesStatusClass { title = "Open", value = model.SalesStatusOpen };
            lstSalesStatus.Add(SalesStatus);

            SalesStatus = new SalesStatusClass { title = "Closed", value = model.SalesStatusClosed };
            lstSalesStatus.Add(SalesStatus);

            SalesStatus = new SalesStatusClass { title = "Pending", value = model.SalesStatusPending };
            lstSalesStatus.Add(SalesStatus);

            if ((model.SalesStatusOpen == 0) && (model.SalesStatusClosed == 0) && (model.SalesStatusPending == 0))
            {
                return Json("");
            }
            else
            {
                return Json(lstSalesStatus);
            }

        }

        public class SalesStatusClass
        {
            [JsonProperty("title")]
            public string title { get; set; }
            [JsonProperty("value")]
            public int value { get; set; }
        }


        //Representative  DatewiseEQSQuantity on Date Change
        [HttpGet]
        public ActionResult DatewiseEQSQuantityChart(string StartDate, string EndDate, int ListId)
        {
            var model = new RepresentativeDashboardPageModel();

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
            var enq = EnquiryRow.Fields;
            var Quot = QuotationRow.Fields;

            using (var connection = _connections.NewFor<SalesRow>())
            {

                //Datewise Total Enquiry  Quantity
                model.TotalEnquiryQuantiy = connection.Count<EnquiryRow>(
                 enq.Date >= model.StartDate &&
                 enq.Date <= model.EndDate &&
                 enq.AssignedId == ListId
                 );

                //Datewise Total Quotation  Quantity
                model.TotalQuotationQuantity = connection.Count<QuotationRow>(
                Quot.Date >= model.StartDate &&
                Quot.Date <= model.EndDate &&
                Quot.AssignedId == ListId
                );

                //Datewise Total Sales  Quantity
                model.TotalSalesQuantity = connection.Count<SalesRow>(
                Sal.Date >= model.StartDate &&
                Sal.Date <= model.EndDate &&
                Sal.AssignedId == ListId
                );

            }
            List<DatewiseEQSQuantityClass> lstEQSQuantity = new List<DatewiseEQSQuantityClass>();

            var EQSQuantity = new DatewiseEQSQuantityClass { title = "Enquiry", value = model.TotalEnquiryQuantiy };
            lstEQSQuantity.Add(EQSQuantity);

            EQSQuantity = new DatewiseEQSQuantityClass { title = "Quotation", value = model.TotalQuotationQuantity };
            lstEQSQuantity.Add(EQSQuantity);

            EQSQuantity = new DatewiseEQSQuantityClass { title = "Sales", value = model.TotalSalesQuantity };
            lstEQSQuantity.Add(EQSQuantity);
            return Json(lstEQSQuantity);
        }

        public class DatewiseEQSQuantityClass
        {
            [JsonProperty("title")]
            public string title { get; set; }
            [JsonProperty("value")]
            public int value { get; set; }
        }

        //Representative  DatewiseEQSAmount on Date Change
        [HttpGet]
        public ActionResult DatewiseEQSAmountChart(string StartDate, string EndDate, int ListId)
        {
            var model = new RepresentativeDashboardPageModel();

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
            var enq = EnquiryRow.Fields;
            var Quot = QuotationRow.Fields;

            using (var connection = _connections.NewFor<SalesRow>())
            {

                //Representative TotalEnquiryAmount List 
                model.TotalEnquiryAmount = connection.List<EnquiryRow>(us => us
                .SelectTableFields()
                .Select(enq.Total)
                .Select(enq.Date)
                .Where(enq.AssignedId == ListId)
                .Where(enq.Date >= model.StartDate)
                .Where(enq.Date <= model.EndDate)
                );

                //Representative TotalQuotationAmount List
                model.TotalQuotationAmount = connection.List<QuotationRow>(us => us
                .SelectTableFields()
                .Select(Quot.Total)
                .Select(Quot.Date)
                .Where(Quot.AssignedId == ListId)
                .Where(Quot.Date >= model.StartDate)
                .Where(Quot.Date <= model.EndDate)
                );

                //Representative TotalSalesAmount List
                model.TotalSalesAmount = connection.List<SalesRow>(us => us
                .SelectTableFields()
                .Select(Sal.Total)
                .Select(Sal.Date)
                .Where(Sal.AssignedId == ListId)
                .Where(Sal.Date >= model.StartDate)
                .Where(Sal.Date <= model.EndDate)
                );

            }
            List<DatewiseEQSAmountClass> lstEQSAmount = new List<DatewiseEQSAmountClass>();

            var EQSAmount = new DatewiseEQSAmountClass { title = "Enquiry", value = (int)model.TotalEnquiryAmount.Sum(x => x.Total) };
            lstEQSAmount.Add(EQSAmount);
            EQSAmount = new DatewiseEQSAmountClass { title = "Quotation", value = (int)model.TotalQuotationAmount.Sum(x => x.Total) };
            lstEQSAmount.Add(EQSAmount);

            EQSAmount = new DatewiseEQSAmountClass { title = "Sales", value = (int)model.TotalSalesAmount.Sum(x => x.Total) };
            lstEQSAmount.Add(EQSAmount);
            return Json(lstEQSAmount);
        }

        public class DatewiseEQSAmountClass
        {
            [JsonProperty("title")]
            public string title { get; set; }
            [JsonProperty("value")]
            public int value { get; set; }
        }

        //Representative Enquiry Ratio on date change
        [HttpGet]
        public ActionResult RepresentativeEnquiryRatioChart(string StartDate, string EndDate, int ListId)
        {

            var model = new RepresentativeDashboardPageModel();

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
            var enq = EnquiryRow.Fields;

            using (var connection = _connections.NewFor<EnquiryRow>())
            {
                //Representatives Total Enquires
                model.TotalEnquiry = connection.Count<EnquiryRow>(
                 enq.Date >= model.StartDate &&
                 enq.Date <= model.EndDate &&
                 enq.AssignedId == ListId
                );

                //Representative Won Enquires               
                model.EnquiryWon = connection.Count<EnquiryRow>(
                enq.ClosingType == 1 &&
                enq.Date >= model.StartDate &&
                enq.Date <= model.EndDate &&
                enq.AssignedId == ListId
                );
            }
            if (model.TotalEnquiry > 0)
            {
                model.EnquiryRatioVal = (int)Math.Round((double)(100 * model.EnquiryWon) / model.TotalEnquiry);
                //model.EnquiryRatioVal = (int)((double)(100 * model.EnquiryWon) / model.TotalEnquiry);
            }
            else
            {
                model.EnquiryRatioVal = 0;
            }
            return Json(model.EnquiryRatioVal);
        }

        //Representative Quotation Ratio on date change
        [HttpGet]
        public ActionResult RepresentativeQuotationRatioChart(string StartDate, string EndDate, int ListId)
        {

            var model = new RepresentativeDashboardPageModel();

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
                //Representatives Total Quotation
                model.TotalQuotation = connection.Count<QuotationRow>(
                 Quot.Date >= model.StartDate &&
                 Quot.Date <= model.EndDate &&
                 Quot.AssignedId == ListId
                );

                //Representative Won Enquires               
                model.QuotationWon = connection.Count<QuotationRow>(
                Quot.ClosingType == 1 &&
                Quot.Date >= model.StartDate &&
                Quot.Date <= model.EndDate &&
                Quot.AssignedId == ListId
                );
            }
            if (model.TotalQuotation > 0)
            {
                model.QuotationRatioVal = (int)Math.Round((double)(100 * model.QuotationWon) / model.TotalQuotation);
                //model.QuotationRatioVal = (int)(100 * model.QuotationWon) / model.TotalQuotation;
            }
            else
            {
                model.QuotationRatioVal = 0;
            }
            //model.QuotationRatioVal = (int)(100 * model.QuotationWon) / model.TotalQuotation;
            return Json(model.QuotationRatioVal);

        }


        //Representative Citywise EnquiryChart on Date Change
        [HttpGet]
        public ActionResult CitywiseEnquiryChart(string StartDate, string EndDate, int ListId)
        {
            var model = new RepresentativeDashboardPageModel();

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
            var enq = EnquiryRow.Fields;

            using (var connection = _connections.NewFor<EnquiryRow>())
            {
                model.CitywiseEnquiry = connection.List<EnquiryRow>(us => us
                          .SelectTableFields()
                          .Select(enq.ContactsCityId)
                          .Select(enq.Date)
                          .Select(enq.Total)
                          .Where(enq.AssignedId == ListId)
                          .Where(enq.Date >= model.StartDate)
                          .Where(enq.Date <= model.EndDate)
                          );

                var c = CityRow.Fields;
                model.EnqCity = connection.List<CityRow>(us => us
                  .SelectTableFields()
                  .Select(c.City)
                 );

            }

            List<CitywiseEnquiryClass> lst = new List<CitywiseEnquiryClass>();

            foreach (var item in model.CitywiseEnquiry.GroupBy(y =>
            y.ContactsCityId).Select(y => new
            {
                CityId = y.Key,
                Count = y.Count(),
                Sum = (int)y.Sum(x => x.Total)
            }).OrderByDescending(z => z.Count).ToList())
            {
                var cityid = "City not selected";
                if (item.CityId.HasValue == true)
                {
                    cityid = model.EnqCity.SingleOrDefault(x => x.Id == item.CityId).City;
                }

                var y = new CitywiseEnquiryClass { country = cityid, litres = item.Count, sum = item.Sum };
                lst.Add(y);
            }
            return Json(lst);
        }

        public class CitywiseEnquiryClass
        {
            [JsonProperty("country")]
            public string country { get; set; }
            [JsonProperty("litres")]
            public int litres { get; set; }
            [JsonProperty("sum")]
            public int sum { get; set; }
        }


        //Citywise Quotation Chart on Date Change
        [HttpGet]
        public ActionResult CitywiseQuotationChart(string StartDate, string EndDate, int ListId)
        {
            var model = new RepresentativeDashboardPageModel();

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
                model.CitywiseQuotationList = connection.List<QuotationRow>(us => us
                          .SelectTableFields()
                          .Select(Quot.ContactsCityId)
                          .Select(Quot.Date)
                          .Select(Quot.Total)
                          .Where(Quot.Date >= model.StartDate)
                          .Where(Quot.Date <= model.EndDate)
                          );

                var c = CityRow.Fields;
                model.QuotCity = connection.List<CityRow>(us => us
                  .SelectTableFields()
                  .Select(c.City)
                 );

            }

            List<CitywiseQuotationClass> lst = new List<CitywiseQuotationClass>();

            foreach (var item in model.CitywiseQuotationList.GroupBy(y =>
            y.ContactsCityId).Select(y => new
            {
                CityId = y.Key,
                Count = y.Count(),
                Sum = (int)y.Sum(x => x.Total)
            }).OrderByDescending(z => z.Count).ToList())
            {
                var cityid = "City not selected";
                if (item.CityId.HasValue == true)
                {
                    cityid = model.QuotCity.SingleOrDefault(x => x.Id == item.CityId).City;
                }

                var y = new CitywiseQuotationClass { country = cityid, litres = item.Count, sum = item.Sum };
                lst.Add(y);
            }
            return Json(lst);
        }

        public class CitywiseQuotationClass
        {
            [JsonProperty("country")]
            public string country { get; set; }
            [JsonProperty("litres")]
            public int litres { get; set; }
            [JsonProperty("sum")]
            public int sum { get; set; }
        }


        //Representative Citywise Sales Chart on Date Change
        [HttpGet]
        public ActionResult CitywiseSalesChart(string StartDate, string EndDate, int ListId)
        {
            var model = new RepresentativeDashboardPageModel();

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
                model.CitywiseSalesList = connection.List<SalesRow>(us => us
                          .SelectTableFields()
                          .Select(Sal.ContactsCityId)
                          .Select(Sal.Date)
                          .Select(Sal.Total)
                          .Where(Sal.Date >= model.StartDate)
                          .Where(Sal.Date <= model.EndDate)
                          );

                var c = CityRow.Fields;
                model.SalCity = connection.List<CityRow>(us => us
                  .SelectTableFields()
                  .Select(c.City)
                 );

            }

            List<CitywiseSalesClass> lst = new List<CitywiseSalesClass>();

            foreach (var item in model.CitywiseSalesList.GroupBy(y =>
            y.ContactsCityId).Select(y => new
            {
                CityId = y.Key,
                Count = y.Count(),
                Sum = (int)y.Sum(x => x.Total)
            }).OrderByDescending(z => z.Count).ToList())
            {
                var cityid = "City not selected";
                if (item.CityId.HasValue == true)
                {
                    cityid = model.SalCity.SingleOrDefault(x => x.Id == item.CityId).City;
                }

                var y = new CitywiseSalesClass { country = cityid, litres = item.Count, sum = item.Sum };
                lst.Add(y);
            }
            return Json(lst);
        }

        public class CitywiseSalesClass
        {
            [JsonProperty("country")]
            public string country { get; set; }
            [JsonProperty("litres")]
            public int litres { get; set; }
            [JsonProperty("sum")]
            public int sum { get; set; }
        }


        //OverallTarget EnquiryChart  with respect to Count on Date Change
        [HttpGet]
        public ActionResult OverallTargetEnquiryChart(string StartDate, string EndDate, int ListId)
        {
            var model = new RepresentativeDashboardPageModel();

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
            var enq = EnquiryRow.Fields;
            var ts = TargetSettingRow.Fields;


            using (var connection = _connections.NewFor<EnquiryRow>())
            {
                model.TargetEnquiry = connection.List<TargetSettingRow>(us => us
                           .SelectTableFields()
                           .Select(ts.MonthlyTarget)
                           .Select(ts.MonthlyTargetAmount)
                           .Where(ts.Representative == ListId)
                           .Where(ts.Type == 1)
                         );

                model.TargetEnquiryAchieved = connection.List<EnquiryRow>(us => us
                           .SelectTableFields()
                           .Select(enq.Id)
                           .Select(enq.Total)
                           .Where(enq.AssignedId == ListId)
                           .Where(enq.ClosingDate >= model.StartDate)
                           .Where(enq.ClosingDate <= model.EndDate)
                         );

                //This will be static

                var sum_of_count = model.TargetEnquiry.Select(x => x.MonthlyTarget).Sum();

                double per_day_count_target = (double)sum_of_count / 30;

                //Days in date range picker     

                int v2 = (model.EndDate - model.StartDate).Days;

                var divisor = ((double)per_day_count_target * v2);

                //Achieved Target with respect to Count
                model.Achieved_Target_Count_Enq = (double)Math.Ceiling((double)model.TargetEnquiryAchieved.Select(x => x.Id).Count());

                if (divisor == 0)
                {
                    if (model.Achieved_Target_Count_Enq == 0)
                    {
                        model.EnquiryTargetCount = 0;
                    }
                    else
                    {
                        model.EnquiryTargetCount = 100;
                    }

                }
                else
                {
                    model.EnquiryTargetCount = Math.Ceiling(((model.TargetEnquiryAchieved.Count()) / divisor) * 100);
                }


                //Required Target with respect to Count
                model.Required_Target_Count_Enq = (double)Math.Ceiling(per_day_count_target * v2);

            }
            return Json(new { Percent = model.EnquiryTargetCount, RCnt = model.Required_Target_Count_Enq, ACnt = model.Achieved_Target_Count_Enq });

        }


        //OverallTarget EnquiryChart with respect to Amount on Date Change
        [HttpGet]
        public ActionResult OverallTargetEnquiryAmtChart(string StartDate, string EndDate, int ListId)
        {
            var model = new RepresentativeDashboardPageModel();

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
            var enq = EnquiryRow.Fields;
            var ts = TargetSettingRow.Fields;


            using (var connection = _connections.NewFor<EnquiryRow>())
            {
                model.TargetEnquiry = connection.List<TargetSettingRow>(us => us
                           .SelectTableFields()
                           .Select(ts.MonthlyTarget)
                           .Select(ts.MonthlyTargetAmount)
                           .Where(ts.Representative == ListId)
                           .Where(ts.Type == 1)
                         );

                model.TargetEnquiryAchieved = connection.List<EnquiryRow>(us => us
                           .SelectTableFields()
                           .Select(enq.Id)
                           .Select(enq.Total)
                           .Where(enq.AssignedId == ListId)
                           .Where(enq.ClosingDate >= model.StartDate)
                           .Where(enq.ClosingDate <= model.EndDate)
                         );

                //Amount
                var sum_of_amt = model.TargetEnquiry.Select(x => x.MonthlyTargetAmount).Sum();

                double per_day_amt_target = (double)sum_of_amt / 30;

                //Days in date range picker

                int v2 = (model.EndDate - model.StartDate).Days;


                var divisor = ((double)per_day_amt_target * v2);

                //Achieved Target with respect to Amount

                model.Achieved_Target_Amount_Enq = (double)Math.Ceiling((double)model.TargetEnquiryAchieved.Select(x => x.Total).Sum());

                if (divisor == 0)
                {
                    if (model.Achieved_Target_Amount_Enq == 0)
                    {
                        model.EnquiryTargetAmount = 0;
                    }
                    else
                    {
                        model.EnquiryTargetAmount = 100;
                    }

                }
                else
                {
                    model.EnquiryTargetAmount = Math.Ceiling(((double)(model.TargetEnquiryAchieved.Select(x => x.Total).Sum()) / divisor) * 100);
                }

                model.Required_Target_Amount_Enq = (double)Math.Ceiling(per_day_amt_target * v2);

            }
            return Json(new { Per = model.EnquiryTargetAmount, RAmt = model.Required_Target_Amount_Enq, AAmt = model.Achieved_Target_Amount_Enq });
        }


        //OverallTarget QuotationChart  with respect to Count on Date Change
        [HttpGet]
        public ActionResult OverallTargetQuotationChart(string StartDate, string EndDate, int ListId)
        {
            var model = new RepresentativeDashboardPageModel();

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
            var ts = TargetSettingRow.Fields;


            using (var connection = _connections.NewFor<QuotationRow>())
            {
                model.TargetQuotation = connection.List<TargetSettingRow>(us => us
                           .SelectTableFields()
                           .Select(ts.MonthlyTarget)
                           .Select(ts.MonthlyTargetAmount)
                           .Where(ts.Representative == ListId)
                           .Where(ts.Type == 2)
                         );

                model.TargetQuotationAchieved = connection.List<QuotationRow>(us => us
                   .SelectTableFields()
                   .Select(Quot.Id)
                   .Select(Quot.Total)
                   .Where(Quot.AssignedId == ListId)
                   .Where(Quot.ClosingDate >= model.StartDate)
                   .Where(Quot.ClosingDate <= model.EndDate)
                 );

                //This will be static

                var sum_of_count = model.TargetQuotation.Select(x => x.MonthlyTarget).Sum();

                double per_day_count_target = (double)sum_of_count / 30;

                //Days in date range picker     

                int v2 = (model.EndDate - model.StartDate).Days;

                var divisor = ((double)per_day_count_target * v2);

                //Achieved Target with respect to Count
                model.Achieved_Target_Count_Quot = (double)Math.Ceiling((double)model.TargetQuotationAchieved.Select(x => x.Id).Count());

                if (divisor == 0)
                {
                    if (model.Achieved_Target_Count_Quot == 0)
                    {
                        model.QuotationTargetCount = 0;
                    }
                    else
                    {
                        model.QuotationTargetCount = 100;
                    }

                }
                else
                {
                    model.QuotationTargetCount = Math.Ceiling(((model.TargetQuotationAchieved.Count()) / divisor) * 100);
                }

                //Required Target with respect to Count

                model.Required_Target_Count_Quot = (double)Math.Ceiling(per_day_count_target * v2);
                //Achieved Target with respect to Count

            }
            return Json(new { Percent = model.QuotationTargetCount, RCnt = model.Required_Target_Count_Quot, ACnt = model.Achieved_Target_Count_Quot });

        }


        //OverallTarget QuotationChart with respect to Amount on Date Change
        [HttpGet]
        public ActionResult OverallTargetQuotationAmtChart(string StartDate, string EndDate, int ListId)
        {
            var model = new RepresentativeDashboardPageModel();

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
            var ts = TargetSettingRow.Fields;


            using (var connection = _connections.NewFor<EnquiryRow>())
            {
                model.TargetQuotation = connection.List<TargetSettingRow>(us => us
                           .SelectTableFields()
                           .Select(ts.MonthlyTarget)
                           .Select(ts.MonthlyTargetAmount)
                           .Where(ts.Representative == ListId)
                           .Where(ts.Type == 2)
                         );

                model.TargetQuotationAchieved = connection.List<QuotationRow>(us => us
                   .SelectTableFields()
                   .Select(Quot.Id)
                   .Select(Quot.Total)
                   .Where(Quot.AssignedId == ListId)
                   .Where(Quot.ClosingDate >= model.StartDate)
                   .Where(Quot.ClosingDate <= model.EndDate)
                 );

                //Amount
                var sum_of_amt = model.TargetQuotation.Select(x => x.MonthlyTargetAmount).Sum();

                double per_day_amt_target = (double)sum_of_amt / 30;

                //Days in date range picker

                int v2 = (model.EndDate - model.StartDate).Days;


                var divisor = ((double)per_day_amt_target * v2);

                //Achieved Target with respect to Amount

                model.Achieved_Target_Amount_Quot = (double)Math.Ceiling((double)model.TargetQuotationAchieved.Select(x => x.Total).Sum());

                if (divisor == 0)
                {
                    if (model.Achieved_Target_Amount_Quot == 0)
                    {
                        model.QuotationTargetAmount = 0;
                    }
                    else
                    {
                        model.QuotationTargetAmount = 100;
                    }

                }
                else
                {
                    model.QuotationTargetAmount = Math.Ceiling(((double)(model.TargetQuotationAchieved.Select(x => x.Total).Sum()) / divisor) * 100);
                }

                model.Required_Target_Amount_Quot = (double)Math.Ceiling(per_day_amt_target * v2);



            }
            return Json(new { Per = model.QuotationTargetAmount, RAmt = model.Required_Target_Amount_Quot, AAmt = model.Achieved_Target_Amount_Quot });
        }

        //OverallTarget SalesChart  with respect to Count on Date Change
        [HttpGet]
        public ActionResult OverallTargetSalesChart(string StartDate, string EndDate, int ListId)
        {
            var model = new RepresentativeDashboardPageModel();

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
            var ts = TargetSettingRow.Fields;


            using (var connection = _connections.NewFor<SalesRow>())
            {
                model.TargetSales = connection.List<TargetSettingRow>(us => us
                           .SelectTableFields()
                           .Select(ts.MonthlyTarget)
                           .Select(ts.MonthlyTargetAmount)
                           .Where(ts.Representative == ListId)
                           .Where(ts.Type == 3)
                         );

                model.TargetSalesAchieved = connection.List<SalesRow>(us => us
                   .SelectTableFields()
                   .Select(Sal.Id)
                   .Select(Sal.Total)
                   .Where(Sal.AssignedId == ListId)
                   .Where(Sal.Date >= model.StartDate)
                   .Where(Sal.Date <= model.EndDate)
                 );

                //This will be static

                var sum_of_count = model.TargetSales.Select(x => x.MonthlyTarget).Sum();

                double per_day_count_target = (double)sum_of_count / 30;

                //Days in date range picker     

                int v2 = (model.EndDate - model.StartDate).Days;

                var divisor = ((double)per_day_count_target * v2);


                //Achieved Target with respect to Count
                model.Achieved_Target_Count_Sal = (double)Math.Ceiling((double)model.TargetSalesAchieved.Select(x => x.Id).Count());

                if (divisor == 0)
                {
                    if (model.Achieved_Target_Count_Sal == 0)
                    {
                        model.SalesTargetCount = 0;
                    }
                    else
                    {
                        model.SalesTargetCount = 100;
                    }

                }
                else
                {
                    model.SalesTargetCount = Math.Ceiling(((model.TargetSalesAchieved.Count()) / divisor) * 100);
                }

                //Required Target with respect to Count

                model.Required_Target_Count_Sal = (double)Math.Ceiling(per_day_count_target * v2);

            }
            return Json(new { Percent = model.SalesTargetCount, RCnt = model.Required_Target_Count_Sal, ACnt = model.Achieved_Target_Count_Sal });

        }


        //OverallTarget SalesChart with respect to Amount on Date Change
        [HttpGet]
        public ActionResult OverallTargetSalesAmtChart(string StartDate, string EndDate, int ListId)
        {
            var model = new RepresentativeDashboardPageModel();

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
            var ts = TargetSettingRow.Fields;


            using (var connection = _connections.NewFor<SalesRow>())
            {
                model.TargetSales = connection.List<TargetSettingRow>(us => us
                           .SelectTableFields()
                           .Select(ts.MonthlyTarget)
                           .Select(ts.MonthlyTargetAmount)
                            .Where(ts.Representative == ListId)
                           .Where(ts.Type == 3)
                         );

                model.TargetSalesAchieved = connection.List<SalesRow>(us => us
                   .SelectTableFields()
                   .Select(Sal.Id)
                   .Select(Sal.Total)
                   .Where(Sal.AssignedId == ListId)
                   .Where(Sal.Date >= model.StartDate)
                   .Where(Sal.Date <= model.EndDate)
                 );

                //Amount
                var sum_of_amt = model.TargetSales.Select(x => x.MonthlyTargetAmount).Sum();

                double per_day_amt_target = (double)sum_of_amt / 30;

                //Days in date range picker

                int v2 = (model.EndDate - model.StartDate).Days;

                var divisor = ((double)per_day_amt_target * v2);

                //Achieved Target with respect to Amount

                model.Achieved_Target_Amount_Sal = (double)Math.Ceiling((double)model.TargetSalesAchieved.Select(x => x.Total).Sum());

                if (divisor == 0)
                {
                    if (model.Achieved_Target_Amount_Sal == 0)
                    {
                        model.SalesTargetAmount = 0;
                    }
                    else
                    {
                        model.SalesTargetAmount = 100;
                    }

                }
                else
                {
                    model.SalesTargetAmount = Math.Ceiling(((double)(model.TargetSalesAchieved.Select(x => x.Total).Sum()) / divisor) * 100);
                }

                model.Required_Target_Amount_Sal = (double)Math.Ceiling(per_day_amt_target * v2);
            }
            return Json(new { Per = model.SalesTargetAmount, RAmt = model.Required_Target_Amount_Sal, AAmt = model.Achieved_Target_Amount_Sal });
        }




    }
}