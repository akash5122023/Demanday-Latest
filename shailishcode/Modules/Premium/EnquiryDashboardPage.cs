
namespace AdvanceCRM.Modules
{
    using Serenity;
using AdvanceCRM.Web.Helpers;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    
    
    using AdvanceCRM;
    using System.Linq;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using AdvanceCRM.Masters;
    using System.Globalization;
    using AdvanceCRM.Products;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Premium;
    using AdvanceCRM.Administration;
    using Serenity.Services;

    [Route("EnquiryDashboard")]
    [ReadPermission("Premium:Dashboards")]
    public class EnquiryDashboardController : Controller
    {
        private readonly ISqlConnections _connections;
        private readonly IRequestContext Context;

        public EnquiryDashboardController(ISqlConnections connections,IRequestContext context)
        {
            _connections = connections;
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
        [HttpGet, Route("~/Premium/Enquiry")]
        public ActionResult Index()
        {
            try
            {
                var cachedModel = LocalCache.GetLocalStoreOnly("EnquiryDashboardPageModel", TimeSpan.FromSeconds(1),
                EnquiryRow.Fields.GenerationKey, () =>
                {

                    var model = new EnquiryDashboardPageModel();
                    var user = (UserDefinition) Context.User.ToUserDefinition();
                    var enq = EnquiryRow.Fields;
                    var enqP = EnquiryProductsRow.Fields;
                    var product = ProductsRow.Fields;
                    var c = CityRow.Fields;
                    var b = BranchRow.Fields;
                    var ts = TargetSettingRow.Fields;

                    using (var connection = _connections.NewFor<EnquiryRow>())
                    {
                        //Enquiry Stages List
                        model.EnquiryStages = connection.List<EnquiryRow>(us => us
                           .SelectTableFields()
                           .Select(enq.Stage)
                           .Select(enq.Total)
                           .Select(enq.Date)
                           .Where(enq.Date >= DateTime.Now.AddMonths(-1))
                           .Where(enq.Date <= DateTime.Now)
                           );

                        //Enquiry Status Open Count
                        model.EnquiryStatusOpen = connection.Count<EnquiryRow>(
                            enq.Status == 1 &&
                            enq.Date >= DateTime.Now.AddMonths(-1) &&
                            enq.Date <= DateTime.Now
                            );

                        //Enquiry Status Closed Count
                        model.EnquiryStatusClosed = connection.Count<EnquiryRow>(
                            enq.Status == 2 &&
                            enq.Date >= DateTime.Now.AddMonths(-1) &&
                            enq.Date <= DateTime.Now
                            );

                        //Enquiry Status Pending Count
                        model.EnquiryStatusPending = connection.Count<EnquiryRow>(
                            enq.Status == 3 &&
                            enq.Date >= DateTime.Now.AddMonths(-1) &&
                            enq.Date <= DateTime.Now
                            );

                        //EnquiryWonLost List
                        model.EnquiryWonLost = connection.List<EnquiryRow>(us => us
                           .SelectTableFields()
                           .Select(enq.Id)
                           .Select(enq.ClosingType)
                           .Select(enq.Date)
                           .Where(enq.Date >= DateTime.Now.AddMonths(-1))
                           .Where(enq.Date <= DateTime.Now)
                           );

                        //Hot Enquiry Count
                        model.HotEnquiry = connection.Count<EnquiryRow>(
                            enq.Type == 1 &&
                            enq.Date >= DateTime.Now.AddMonths(-1) &&
                            enq.Date <= DateTime.Now
                            );

                        //Warm Enquiry Count
                        model.WarmEnquiry = connection.Count<EnquiryRow>(
                            enq.Type == 2 &&
                            enq.Date >= DateTime.Now.AddMonths(-1) &&
                            enq.Date <= DateTime.Now
                            );

                        //Cold Enquiry Count
                        model.ColdEnquiry = connection.Count<EnquiryRow>(
                            enq.Type == 3 &&
                            enq.Date >= DateTime.Now.AddMonths(-1) &&
                            enq.Date <= DateTime.Now
                            );

                        //Total Enquiry Types
                        model.TotalEnquiryTypes = connection.Count<EnquiryRow>(
                            enq.Date >= DateTime.Now.AddMonths(-1) &&
                            enq.Date <= DateTime.Now
                            );

                        //Percentage Calculation HotEnquiry,WarmEnquiry and ColdEnquiry
                        if (model.HotEnquiry > 0)
                        {
                            model.HotEndvalue = (int)Math.Round((double)(100 * model.HotEnquiry) / model.TotalEnquiryTypes);
                        }
                        else
                        {
                            model.HotEndvalue = 0;
                        }
                        if (model.WarmEnquiry > 0)
                        {
                            model.WarmEndvalue = (int)Math.Round((double)(100 * model.WarmEnquiry) / model.TotalEnquiryTypes);
                        }
                        else
                        {
                            model.WarmEndvalue = 0;
                        }
                        if (model.ColdEnquiry > 0)
                        {
                            model.ColdEndvalue = (int)Math.Round((double)(100 * model.ColdEnquiry) / model.TotalEnquiryTypes);
                        }
                        else
                        {
                            model.ColdEndvalue = 0;
                        }

                        //Mediawise Enquiry
                        model.EnquirySource = connection.List<EnquiryRow>(us => us
                          .SelectTableFields()
                          .Select(enq.Source)
                          .Select(enq.Date)
                          .Select(enq.Total)
                          .Where(enq.Date >= DateTime.Now.AddMonths(-1))
                          .Where(enq.Date <= DateTime.Now)
                          );

                        //Enquiry Won Count
                        model.EnquiryWon = model.EnquiryWonLost.Where(x => x.Date >= DateTime.Now.AddMonths(-1) && x.Date <= DateTime.Now).Where(z => z.ClosingType == (Masters.ClosingTypeMaster)1).Count();

                        //Enquiry Lost Count
                        model.EnquiryLost = model.EnquiryWonLost.Where(x => x.Date >= DateTime.Now.AddMonths(-1) && x.Date <= DateTime.Now).Where(z => z.ClosingType == (Masters.ClosingTypeMaster)2).Count();

                        //Most Enquiry Product List
                        model.MostEnquiryProduct = connection.List<EnquiryProductsRow>(us => us
                           .SelectTableFields()
                           .Select(enqP.ProductsName)
                           .Select(enqP.EnquiryDate)
                           .Where(enqP.EnquiryDate >= DateTime.Now.AddMonths(-1))
                           .Where(enqP.EnquiryDate <= DateTime.Now)
                           );

                        //Least Enquiry Product List
                        model.LeastEnquiryProduct = connection.List<EnquiryProductsRow>(us => us
                          .SelectTableFields()
                          .Select(enqP.ProductsName)
                          .Select(enqP.EnquiryDate)
                          .Where(enqP.EnquiryDate >= DateTime.Now.AddMonths(-1))
                          .Where(enqP.EnquiryDate <= DateTime.Now)
                          );

                        //Productwise Division Enquiry List
                        model.ProductwiseDivisionEnquiry = connection.List<EnquiryProductsRow>(us => us
                          .SelectTableFields()
                          .Select(enqP.ProductsDivisionId)
                          .Select(enqP.ProductsName)
                          .Select(enqP.EnquiryDate)
                          .Where(enqP.EnquiryDate >= DateTime.Now.AddMonths(-1))
                          .Where(enqP.EnquiryDate <= DateTime.Now)
                         );

                        //Division List
                        var div = ProductsDivisionRow.Fields;
                        model.DivisionList = connection.List<ProductsDivisionRow>(us => us
                          .SelectTableFields()
                          .Select(div.Id)
                          .Select(div.ProductsDivision)
                         );

                        //Enquiry Analysis List
                        model.EnquiryAnalysisList = connection.List<EnquiryRow>(us => us
                         .SelectTableFields()
                         .Select(enq.Id)
                         .Select(enq.Date)
                         .Where(enq.Date >= DateTime.Now.AddMonths(-2))
                         .Where(enq.Date <= DateTime.Now)
                         );

                        //Citywise Enquiry Analysis

                        model.CitywiseEnquiry = connection.List<EnquiryRow>(us => us
                         .SelectTableFields()
                         .Select(enq.ContactsCityId)
                         .Select(enq.Date)
                         .Select(enq.Total)
                         .Where(enq.Date >= DateTime.Now.AddMonths(-1))
                         .Where(enq.Date <= DateTime.Now)
                         );

                        model.City = connection.List<CityRow>(us => us
                         .SelectTableFields()
                         .Select(c.City)
                         );

                        //Branchwise Enquiry Analysis

                        model.BranchwiseEnquiry = connection.List<EnquiryRow>(us => us
                         .SelectTableFields()
                         .Select(enq.BranchId)
                         .Select(enq.Date)
                         .Select(enq.Total)
                         .Where(enq.Date >= DateTime.Now.AddMonths(-1))
                         .Where(enq.Date <= DateTime.Now)
                         );

                        model.Branch = connection.List<BranchRow>(us => us
                         .SelectTableFields()
                         .Select(b.Branch)
                         );

                        model.TargetEnquiry = connection.List<TargetSettingRow>(us => us
                           .SelectTableFields()
                           .Select(ts.MonthlyTarget)
                           .Select(ts.MonthlyTargetAmount)
                           .Where(ts.Type == 1)
                         );


                        model.TargetEnquiryAchieved = connection.List<EnquiryRow>(us => us
                           .SelectTableFields()
                           .Select(enq.Id)
                           .Select(enq.Total)
                           .Where(enq.ClosingDate >= DateTime.Now.AddMonths(-1))
                           .Where(enq.ClosingDate <= DateTime.Now)
                         );


                        //Days in date range picker
                        var v2 = DateTime.DaysInMonth(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month);



                        #region Enquiry Target Achievement with respect to count

                        //Count
                        var sum_of_count = model.TargetEnquiry.Select(x => x.MonthlyTarget).Sum();

                        double per_day_count_target = (double)sum_of_count / 30;

                        var divisorCnt = ((double)per_day_count_target * v2);

                        //Achieved Target with respect to Count
                        model.AchievedTargetCount = (double)Math.Ceiling((double)model.TargetEnquiryAchieved.Select(x => x.Id).Count());



                        if (divisorCnt == 0)
                        {
                            if (model.AchievedTargetCount == 0)
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
                            model.EnquiryTargetCount = Math.Ceiling(((model.TargetEnquiryAchieved.Count()) / divisorCnt) * 100);
                        }


                        //Required Target with respect to Count 

                        model.RequiredTargetCount = (double)Math.Ceiling(per_day_count_target * v2);

                        #endregion


                        #region Enquiry Target Achievement with respect to Amount 


                        //Amount
                        var sum_of_amt = model.TargetEnquiry.Select(x => x.MonthlyTargetAmount).Sum();

                        double per_day_amt_target = (double)sum_of_amt / 30;

                        var divisorAmt = ((double)per_day_amt_target * v2);

                        //Achieved Target with respect to Amount

                        model.AchievedTargetAmount = (double)Math.Ceiling((double)model.TargetEnquiryAchieved.Select(x => x.Total).Sum());



                        if (divisorAmt == 0)
                        {
                            if (model.AchievedTargetAmount == 0)
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
                            model.EnquiryTargetAmount = Math.Ceiling(((double)(model.TargetEnquiryAchieved.Select(x => x.Total).Sum()) / divisorAmt) * 100);
                        }


                        //Required Target with respect to Amount

                        model.RequiredTargetAmount = (double)Math.Ceiling(per_day_amt_target * v2);
                        #endregion

                    }
                    return model;
                });

                return View(MVC.Views.Premium.EnquiryDashboard, cachedModel);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return View(MVC.Views.MailChimp.Views.CreateList);
                //return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable, ex.Message);
            }
        }


        //Enquiry Stages FunnelChart On Date Change
        [HttpGet]
        public ActionResult EnquiryStagesFunnel(string StartDate, string EndDate)
        {
            try
            {
                var model = new EnquiryDashboardPageModel();

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
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return Json("");

            }
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



        //Enquiry Status On Date Change
        [HttpGet]
        public ActionResult EnquiryStatus(string StartDate, string EndDate)
        {
            try
            {

                var model = new EnquiryDashboardPageModel();

                if (StartDate == null)
                {
                    model.StartDate = DateTime.Now;
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
                        enq.Date <= model.EndDate
                        );

                    model.EnquiryStatusClosed = connection.Count<EnquiryRow>(
                        enq.Status == 2 &&
                        enq.Date >= model.StartDate &&
                        enq.Date <= model.EndDate
                        );

                    model.EnquiryStatusPending = connection.Count<EnquiryRow>(
                        enq.Status == 3 &&
                        enq.Date >= model.StartDate &&
                        enq.Date <= model.EndDate
                        );

                }
                List<EnquiryStatusClass> lst = new List<EnquiryStatusClass>();

                var y = new EnquiryStatusClass { country = "Open", value = model.EnquiryStatusOpen };
                lst.Add(y);

                y = new EnquiryStatusClass { country = "Closed", value = model.EnquiryStatusClosed };
                lst.Add(y);

                y = new EnquiryStatusClass { country = "Pending", value = model.EnquiryStatusPending };
                lst.Add(y);

                return Json(lst);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return Json("");

            }
        }

        public class EnquiryStatusClass
        {
            [JsonProperty("country")]
            public string country { get; set; }
            [JsonProperty("value")]
            public int value { get; set; }
        }


        //Enquiry WonLoss on Date Change
        [HttpGet]
        public ActionResult EnquiryWonLoss(string StartDate, string EndDate)
        {
            try
            {

                var model = new EnquiryDashboardPageModel();

                if (StartDate == null)
                {
                    model.StartDate = DateTime.Now;
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
                    model.EnquiryWonLost = connection.List<EnquiryRow>(us => us
                               .SelectTableFields()
                               .Select(enq.Id)
                               .Select(enq.ClosingType)
                               .Select(enq.Date)
                               .Where(enq.Date >= model.StartDate)
                               .Where(enq.Date <= model.EndDate)
                            );
                }

                List<EnquiryWonLossClass> lst = new List<EnquiryWonLossClass>();

                var dtlst = model.EnquiryWonLost.Where(x => x.ClosingType != null).Select(x => x.Date).Distinct();

                foreach (var itm in dtlst)
                {
                    var y = new EnquiryWonLossClass
                    {
                        day = itm.Value.ToString("dd/MM"),
                        won = model.EnquiryWonLost.Where(x => x.Date == itm).Where(z => z.ClosingType == (Masters.ClosingTypeMaster)1).Count(),
                        lost = model.EnquiryWonLost.Where(x => x.Date == itm).Where(z => z.ClosingType == (Masters.ClosingTypeMaster)2).Count()
                    };

                    lst.Add(y);
                }

                return Json(lst);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return Json("");

            }
        }

        public class EnquiryWonLossClass
        {
            [JsonProperty("day")]
            public string day { get; set; }
            [JsonProperty("won")]
            public int won { get; set; }
            [JsonProperty("lost")]
            public int lost { get; set; }
        }


        //Enquiry Types Chart on Date Change
        [HttpGet]
        public ActionResult EnquiryTypesChart(string StartDate, string EndDate)
        {
            try
            {
                var model = new EnquiryDashboardPageModel();

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
                    model.HotEnquiry = connection.Count<EnquiryRow>(
                                enq.Type == 1 &&
                                enq.Date >= model.StartDate &&
                                enq.Date <= model.EndDate
                                );

                    model.WarmEnquiry = connection.Count<EnquiryRow>(
                        enq.Type == 2 &&
                        enq.Date >= model.StartDate &&
                        enq.Date <= model.EndDate
                        );

                    model.ColdEnquiry = connection.Count<EnquiryRow>(
                        enq.Type == 3 &&
                        enq.Date >= model.StartDate &&
                        enq.Date <= model.EndDate
                        );

                    model.TotalEnquiryTypes = connection.Count<EnquiryRow>(
                        enq.Date >= model.StartDate &&
                        enq.Date <= model.EndDate
                        );
                    if (model.HotEnquiry > 0)
                    {
                        model.HotEndvalue = (int)Math.Round((double)(100 * model.HotEnquiry) / model.TotalEnquiryTypes);
                    }
                    else
                    {
                        model.HotEndvalue = 0;
                    }
                    if (model.WarmEnquiry > 0)
                    {
                        model.WarmEndvalue = (int)Math.Round((double)(100 * model.WarmEnquiry) / model.TotalEnquiryTypes);
                    }
                    else
                    {
                        model.WarmEndvalue = 0;
                    }
                    if (model.ColdEnquiry > 0)
                    {
                        model.ColdEndvalue = (int)Math.Round((double)(100 * model.ColdEnquiry) / model.TotalEnquiryTypes);
                    }
                    else
                    {
                        model.ColdEndvalue = 0;
                    }

                    var jsonString = "{" +
                        "\"color\": \"#eee\"," +
                        "\"startValue\": 0, " +
                        "\"endValue\": 100, " +
                        "\"radius\": \"80%\", " +
                        "\"innerRadius\": \"65%\" " +
                        "}," +
                        " { " +
                        "\"color\": \"#fdd400\"," +
                        "\"startValue\": 0, " +
                        "\"endValue\": " + model.HotEndvalue + ", " +
                        "\"radius\": \"80%\"," +
                        "\"innerRadius\": \"65%\"," +
                        "\"balloonText\": \"" + model.HotEndvalue.ToString() + "% (Count:" + model.HotEnquiry + ")\"}," +
                        "{ " +
                        "\"color\": \"#eee\"," +
                        "\"startValue\": 0, " +
                        "\"endValue\": 100," +
                        "\"radius\": \"60%\"," +
                        "\"innerRadius\": \"45%\"" +
                        "}," +
                        " {" +
                        "\"color\": \"#cc4748\"," +
                        "\"startValue\": 0, " +
                        "\"endValue\": " + model.WarmEndvalue + "," +
                        "\"radius\": \"60%\"," +
                        "\"innerRadius\": \"45%\"," +
                        "\"balloonText\": \"" + model.WarmEndvalue.ToString() + "% (Count:" + model.WarmEnquiry + ")\"}," +
                        " {" +
                        "\"color\": \"#eee\"," +
                        "\"startValue\": 0," +
                        "\"endValue\": 100," +
                        "\"radius\": \"40%\"," +
                        "\"innerRadius\": \"25%\"" +
                        "}," +
                        "{" +
                        "\"color\": \"#67b7dc\"," +
                        "\"startValue\": 0," +
                        "\"endValue\": " + model.ColdEndvalue + "," +
                        "\"radius\": \"40%\"," +
                        "\"innerRadius\": \"25%\"," +
                        "\"balloonText\": \"" + model.ColdEndvalue.ToString() + "% (Count:" + model.ColdEnquiry + ")\"" +
                        "}";
                    return Json(jsonString);
                }
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return Json("");

            }

        }


        //Mediawise Enquiry Chart on Date Change
        [HttpGet]
        public ActionResult MediawiseEnquiryChart(string StartDate, string EndDate)
        {
            try
            {
                var model = new EnquiryDashboardPageModel();

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
                    model.EnquirySource = connection.List<EnquiryRow>(us => us
                             .SelectTableFields()
                             .Select(enq.Source)
                             .Select(enq.Date)
                             .Select(enq.Total)
                             .Where(enq.Date >= model.StartDate)
                             .Where(enq.Date <= model.EndDate)
                             );

                }
                List<MediawiseEnquiryClass> lst = new List<MediawiseEnquiryClass>();
                foreach (var item in model.EnquirySource.GroupBy(y => y.Source).Select(y => new { Source = y.Key, Count = y.Count(), Sum = (int)y.Sum(x => x.Total) }).OrderByDescending(z => z.Count).ToList())
                {
                    var y = new MediawiseEnquiryClass { title = item.Source, value = item.Count, sum = item.Sum };
                    lst.Add(y);
                }
                return Json(lst);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return Json("");
            }

        }

        public class MediawiseEnquiryClass
        {
            [JsonProperty("title")]
            public string title { get; set; }
            [JsonProperty("value")]
            public int value { get; set; }
            [JsonProperty("sum")]
            public int sum { get; set; }
        }



        //EnquiryAnalysis on Date Change
        [HttpGet]
        public ActionResult EnquiryAnalysis(string StartDate, string EndDate)
        {
            try
            {
                var model = new EnquiryDashboardPageModel();

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
                    model.EnquiryStatusClosed = connection.Count<EnquiryRow>(
                             enq.Status == 2 &&
                             enq.Date >= model.StartDate &&
                             enq.Date <= model.EndDate
                             );
                    model.EnquiryStatusOpen = connection.Count<EnquiryRow>(
                              enq.Status == 1 &&
                              enq.Date >= model.StartDate &&
                              enq.Date <= model.EndDate
                              );

                    model.TotalEnquiryTypes = connection.Count<EnquiryRow>(
                               enq.Date >= model.StartDate &&
                               enq.Date <= model.EndDate
                               );
                    model.EnquiryStatusPending = connection.Count<EnquiryRow>(
                               enq.Status == 3 &&
                               enq.Date >= model.StartDate &&
                               enq.Date <= model.EndDate
                               );

                    model.EnquiryWon = connection.Count<EnquiryRow>(
                               enq.ClosingType == 1 &&
                               enq.Date >= model.StartDate &&
                               enq.Date <= model.EndDate
                               );


                    model.EnquiryLost = connection.Count<EnquiryRow>(
                               enq.ClosingType == 2 &&
                               enq.Date >= model.StartDate &&
                               enq.Date <= model.EndDate
                               );
                }

                List<EnquiryAnalysisClass> lstEnquiryAnalysis = new List<EnquiryAnalysisClass>();

                var EnquiryAnalysis = new EnquiryAnalysisClass { title = "Total Enquiry", value = model.TotalEnquiryTypes };
                lstEnquiryAnalysis.Add(EnquiryAnalysis);

                EnquiryAnalysis = new EnquiryAnalysisClass { title = "Open", value = model.EnquiryStatusOpen };
                lstEnquiryAnalysis.Add(EnquiryAnalysis);

                EnquiryAnalysis = new EnquiryAnalysisClass { title = "Close", value = model.EnquiryStatusClosed };
                lstEnquiryAnalysis.Add(EnquiryAnalysis);

                EnquiryAnalysis = new EnquiryAnalysisClass { title = "Pending", value = model.EnquiryStatusPending };
                lstEnquiryAnalysis.Add(EnquiryAnalysis);

                EnquiryAnalysis = new EnquiryAnalysisClass { title = "Won", value = model.EnquiryWon };
                lstEnquiryAnalysis.Add(EnquiryAnalysis);

                EnquiryAnalysis = new EnquiryAnalysisClass { title = "Lost", value = model.EnquiryLost };
                lstEnquiryAnalysis.Add(EnquiryAnalysis);

                if ((model.TotalEnquiryTypes == 0) && (model.EnquiryStatusOpen == 0) && (model.EnquiryStatusClosed == 0) && (model.EnquiryStatusPending == 0) && (model.EnquiryWon == 0) && (model.EnquiryLost == 0))
                {
                    return Json("");
                }
                else
                {
                    return Json(lstEnquiryAnalysis);
                }
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return Json("");

            }
        }

        public class EnquiryAnalysisClass
        {
            [JsonProperty("title")]
            public string title { get; set; }
            [JsonProperty("value")]
            public int value { get; set; }
        }



        //Most Enquired ProductChart on Date Change
        [HttpGet]
        public ActionResult MostEnquiredProductChart(string StartDate, string EndDate)
        {
            try
            {
                var model = new EnquiryDashboardPageModel();

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
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return Json("");
            }
        }

        public class MostEnquiryClass
        {
            [JsonProperty("country")]
            public string country { get; set; }
            [JsonProperty("visits")]
            public int visits { get; set; }
        }


        //Least Enquired ProductChart on Date Change
        [HttpGet]
        public ActionResult LeastEnquiredProductChart(string StartDate, string EndDate)
        {
            try
            {
                var model = new EnquiryDashboardPageModel();

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
                    model.GroupLeastEnquiryProduct = connection.List<EnquiryProductsRow>(us => us
                              .SelectTableFields()
                              .Select(enqP.ProductsName)
                              .Select(enqP.EnquiryDate)
                              .Where(enqP.EnquiryDate >= model.StartDate)
                              .Where(enqP.EnquiryDate <= model.EndDate)
                              );
                }
                List<LeastEnquiryClass> lst = new List<LeastEnquiryClass>();
                foreach (var item in model.GroupLeastEnquiryProduct.GroupBy(info => info.ProductsName).Select(group => new
                {
                    Name = group.Key,
                    Count = group.Count(),
                    Sum = group.Sum(x => x.Quantity)
                }).OrderBy(x => x.Count).Take(15))
                {
                    var y = new LeastEnquiryClass { country = item.Name, visits = item.Count };
                    lst.Add(y);
                }
                lst.Reverse();
                return Json(lst);
            }

            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return Json("");
            }

        }

        public class LeastEnquiryClass
        {
            [JsonProperty("country")]
            public string country { get; set; }
            [JsonProperty("visits")]
            public int visits { get; set; }
        }


        //Productwise Division EnquiryChart on Date Change
        [HttpGet]
        public ActionResult ProductwiseDivisionEnquiryChart(string StartDate, string EndDate)
        {

            try
            {
                var model = new EnquiryDashboardPageModel();

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
                    model.ProductwiseDivisionEnquiry = connection.List<EnquiryProductsRow>(us => us
                              .SelectTableFields()
                              .Select(enqP.ProductsDivisionId)
                              .Select(enqP.ProductsName)
                              .Select(enqP.EnquiryDate)
                              .Where(enqP.EnquiryDate >= model.StartDate)
                              .Where(enqP.EnquiryDate <= model.EndDate)
                              );

                    var div = ProductsDivisionRow.Fields;
                    model.DivisionList = connection.List<ProductsDivisionRow>(us => us
                      .SelectTableFields()
                      .Select(div.Id)
                      .Select(div.ProductsDivision)
                     );

                }

                List<ProductwiseDivisionEnquiryClass> lst = new List<ProductwiseDivisionEnquiryClass>();
                foreach (var item in model.ProductwiseDivisionEnquiry.GroupBy(info => info.ProductsDivisionId).Select(group => new
                {
                    DivId = group.Key,
                    Count = group.Count(),
                }))
                {
                    var y = new ProductwiseDivisionEnquiryClass { country = model.DivisionList.SingleOrDefault(x => x.Id == item.DivId).ProductsDivision, visits = item.Count };
                    lst.Add(y);
                }
                return Json(lst);
            }

            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return Json("");
            }
        }

        public class ProductwiseDivisionEnquiryClass
        {
            [JsonProperty("country")]
            public string country { get; set; }
            [JsonProperty("visits")]
            public int visits { get; set; }
        }




        //Citywise EnquiryChart on Date Change
        [HttpGet]
        public ActionResult CitywiseEnquiryChart(string StartDate, string EndDate)
        {
            try
            {
                var model = new EnquiryDashboardPageModel();

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
                              .Where(enq.Date >= model.StartDate)
                              .Where(enq.Date <= model.EndDate)
                              );

                    var c = CityRow.Fields;
                    model.City = connection.List<CityRow>(us => us
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
                        cityid = model.City.SingleOrDefault(x => x.Id == item.CityId).City;
                    }

                    var y = new CitywiseEnquiryClass { country = cityid, litres = item.Count, sum = item.Sum };
                    lst.Add(y);
                }
                return Json(lst);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return Json("");
            }
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

        //Branchwise EnquiryChart on Date Change
        [HttpGet]
        public ActionResult BranchwiseEnquiryChart(string StartDate, string EndDate)
        {

            try
            {
                var model = new EnquiryDashboardPageModel();

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
                    model.BranchwiseEnquiry = connection.List<EnquiryRow>(us => us
                              .SelectTableFields()
                              .Select(enq.BranchId)
                              .Select(enq.Date)
                              .Select(enq.Total)
                              .Where(enq.Date >= model.StartDate)
                              .Where(enq.Date <= model.EndDate)
                              );

                    var b = BranchRow.Fields;
                    model.Branch = connection.List<BranchRow>(us => us
                      .SelectTableFields()
                      .Select(b.Branch)
                     );

                }

                List<BranchwiseEnquiryClass> lst = new List<BranchwiseEnquiryClass>();

                foreach (var item in model.BranchwiseEnquiry.GroupBy(y =>
                y.BranchId).Select(y => new
                {
                    BranchID = y.Key,
                    Count = y.Count(),
                    Sum = (int)y.Sum(x => x.Total)
                }).OrderByDescending(z => z.Count).ToList())
                {
                    var branchid = "Branch N/A";
                    if (item.BranchID.HasValue == true)
                    {
                        branchid = model.Branch.SingleOrDefault(x => x.Id == item.BranchID).Branch;
                    }

                    var y = new BranchwiseEnquiryClass { country = branchid + "\n" + "(Amt:" + item.Sum + ")", visits = item.Count, sum = item.Sum };
                    lst.Add(y);
                }
                return Json(lst);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return Json("");
            }
        }

        public class BranchwiseEnquiryClass
        {
            [JsonProperty("country")]
            public string country { get; set; }
            [JsonProperty("visits")]
            public int visits { get; set; }
            [JsonProperty("sum")]
            public int sum { get; set; }
        }


        //OverallTarget EnquiryChart  with respect to Count on Date Change
        [HttpGet]
        public ActionResult OverallTargetEnquiryChart(string StartDate, string EndDate)
        {
            try
            {
                var model = new EnquiryDashboardPageModel();

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
                               .Where(ts.Type == 1)
                             );

                    model.TargetEnquiryAchieved = connection.List<EnquiryRow>(us => us
                       .SelectTableFields()
                       .Select(enq.Id)
                       .Select(enq.Total)
                       .Where(enq.ClosingDate >= model.StartDate)
                       .Where(enq.ClosingDate <= model.EndDate)
                     );


                    #region Enquiry Target Achievement with respect to count

                    //This will be static
                    //Count
                    var sum_of_count = model.TargetEnquiry.Select(x => x.MonthlyTarget).Sum();

                    double per_day_count_target = (double)sum_of_count / 30;

                    //Days in date range picker			

                    int v2 = (model.EndDate - model.StartDate).Days;

                    var divisorCnt = ((double)per_day_count_target * v2);

                    //Achieved Target with respect to Count
                    model.AchievedTargetCount = (double)Math.Ceiling((double)model.TargetEnquiryAchieved.Select(x => x.Id).Count());



                    if (divisorCnt == 0)
                    {
                        if (model.AchievedTargetCount == 0)
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
                        model.EnquiryTargetCount = Math.Ceiling(((model.TargetEnquiryAchieved.Count()) / divisorCnt) * 100);
                    }


                    //Required Target with respect to Count 

                    model.RequiredTargetCount = (double)Math.Ceiling(per_day_count_target * v2);

                    #endregion

                }
                return Json(new { Percent = model.EnquiryTargetCount, RCnt = model.RequiredTargetCount, ACnt = model.AchievedTargetCount });
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return Json("");
            }

        }


        //OverallTarget EnquiryChart with respect to Amount on Date Change
        [HttpGet]
        public ActionResult OverallTargetEnquiryAmtChart(string StartDate, string EndDate)
        {
            try
            {
                var model = new EnquiryDashboardPageModel();

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
                               .Where(ts.Type == 1)
                             );

                    model.TargetEnquiryAchieved = connection.List<EnquiryRow>(us => us
                       .SelectTableFields()
                       .Select(enq.Id)
                       .Select(enq.Total)
                       .Where(enq.ClosingDate >= model.StartDate)
                       .Where(enq.ClosingDate <= model.EndDate)
                     );


                    #region Enquiry Target Achievement with respect to Amount 


                    //Amount
                    var sum_of_amt = model.TargetEnquiry.Select(x => x.MonthlyTargetAmount).Sum();

                    double per_day_amt_target = (double)sum_of_amt / 30;

                    //Days in date range picker

                    int v2 = (model.EndDate - model.StartDate).Days;

                    var divisorAmt = ((double)per_day_amt_target * v2);

                    //Achieved Target with respect to Amount

                    model.AchievedTargetAmount = (double)Math.Ceiling((double)model.TargetEnquiryAchieved.Select(x => x.Total).Sum());



                    if (divisorAmt == 0)
                    {
                        if (model.AchievedTargetAmount == 0)
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
                        model.EnquiryTargetAmount = Math.Ceiling(((double)(model.TargetEnquiryAchieved.Select(x => x.Total).Sum()) / divisorAmt) * 100);
                    }


                    //Required Target with respect to Amount

                    model.RequiredTargetAmount = (double)Math.Ceiling(per_day_amt_target * v2);
                    #endregion


                }
                return Json(new { Per = model.EnquiryTargetAmount, RAmt = model.RequiredTargetAmount, AAmt = model.AchievedTargetAmount });
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return Json("");
            }

        }

    }
}
