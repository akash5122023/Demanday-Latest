
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
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Quotation;
    using AdvanceCRM.Premium;
    using AdvanceCRM.Administration;
    using Serenity.Services;

    [Route("QuotationDashboard")]
    [ReadPermission("Premium:Dashboards")]
    public class QuotationDashboardController : Controller
    {
        private readonly ISqlConnections _connections;
        private readonly IRequestContext Context;

        public QuotationDashboardController(ISqlConnections connections, IRequestContext context)
        {
            _connections = connections;
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet, Route("~/Premium/Quotation")]
        public ActionResult Index()
        {
            try
            {
                var cachedModel = LocalCache.GetLocalStoreOnly("QuotationDashboardPageModel", TimeSpan.FromSeconds(1),
                      QuotationRow.Fields.GenerationKey, () =>
                      {
                          var model = new QuotationDashboardPageModel();
                          var user = (UserDefinition)Context.User.ToUserDefinition();
                          var Quot = QuotationRow.Fields;
                          var QuotP = QuotationProductsRow.Fields;
                          var c = CityRow.Fields;
                          var b = BranchRow.Fields;
                          var ts = TargetSettingRow.Fields;

                          using (var connection = _connections.NewFor<QuotationRow>())
                          {
                              //Quotation Stages List
                              model.QuotationStages = connection.List<QuotationRow>(us => us
                                     .SelectTableFields()
                                     .Select(Quot.Stage)
                                     .Select(Quot.Date)
                                     .Select(Quot.Total)
                                     .Where(Quot.Date >= DateTime.Now.AddMonths(-1))
                                     .Where(Quot.Date <= DateTime.Now)
                                     );

                              //Quotation Status Open   
                              model.QuotationStatusOpen = connection.Count<QuotationRow>(
                                      Quot.Status == 1 &&
                                      Quot.Date >= DateTime.Now.AddMonths(-1) &&
                                      Quot.Date <= DateTime.Now
                                      );

                              //Quotation Status Close
                              model.QuotationStatusClosed = connection.Count<QuotationRow>(
                                      Quot.Status == 2 &&
                                      Quot.Date >= DateTime.Now.AddMonths(-1) &&
                                      Quot.Date <= DateTime.Now
                                      );

                              //Quotation Status Pending
                              model.QuotationStatusPending = connection.Count<QuotationRow>(
                                      Quot.Status == 3 &&
                                      Quot.Date >= DateTime.Now.AddMonths(-1) &&
                                      Quot.Date <= DateTime.Now
                                      );

                              //Quotation WonLost 
                              model.QuotationWonLost = connection.List<QuotationRow>(us => us
                                     .SelectTableFields()
                                     .Select(Quot.Id)
                                     .Select(Quot.ClosingType)
                                     .Select(Quot.Date)
                                     .Where(Quot.Date >= DateTime.Now.AddMonths(-1))
                                     .Where(Quot.Date <= DateTime.Now)
                                     );

                              //Total QuotationTypes
                              model.TotalQuotationTypes = connection.Count<QuotationRow>(
                                      Quot.Date >= DateTime.Now.AddMonths(-1) &&
                                      Quot.Date <= DateTime.Now
                                      );

                              //Quotation Won
                              //model.QuotationWon = model.QuotationWonLost.Where(x => x.Date >= DateTime.Now.AddMonths(-1) && x.Date <= DateTime.Now).Where(z => z.ClosingType == (Masters.ClosingTypeMaster)1).Count();

                              ////Quotation Lost
                              //model.QuotationLost = model.QuotationWonLost.Where(x => x.Date >= DateTime.Now.AddMonths(-1) && x.Date <= DateTime.Now).Where(z => z.ClosingType == (Masters.ClosingTypeMaster)2).Count();

                              //Mediawise Quotation
                              model.QuotationSource = connection.List<QuotationRow>(us => us
                                    .SelectTableFields()
                                    .Select(Quot.Source)
                                    .Select(Quot.Date)
                                    .Select(Quot.Total)
                                    .Where(Quot.Date >= DateTime.Now.AddMonths(-1))
                                    .Where(Quot.Date <= DateTime.Now)
                                    );

                              //Most Quotation Product
                              model.MostQuotationProduct = connection.List<QuotationProductsRow>(us => us
                                    .SelectTableFields()
                                    .Select(QuotP.ProductsName)
                                    .Select(QuotP.QuotationDate)
                                    .Where(QuotP.QuotationDate >= DateTime.Now.AddMonths(-1))
                                    .Where(QuotP.QuotationDate <= DateTime.Now)
                                    );

                              //Least Quotation Product
                              model.LeastQuotationProduct = connection.List<QuotationProductsRow>(us => us
                                    .SelectTableFields()
                                    .Select(QuotP.ProductsName)
                                    .Select(QuotP.QuotationDate)
                                    .Where(QuotP.QuotationDate >= DateTime.Now.AddMonths(-1))
                                    .Where(QuotP.QuotationDate <= DateTime.Now)
                                    );

                              //Productwise Division Quotation
                              model.ProductwiseDivisionQuotation = connection.List<QuotationProductsRow>(us => us
                                    .SelectTableFields()
                                    .Select(QuotP.ProductsDivisionId)
                                    .Select(QuotP.ProductsName)
                                    .Select(QuotP.QuotationDate)
                                    .Where(QuotP.QuotationDate >= DateTime.Now.AddMonths(-1))
                                    .Where(QuotP.QuotationDate <= DateTime.Now)
                                   );

                              //DivisionList
                              var div = ProductsDivisionRow.Fields;
                              model.DivisionList = connection.List<ProductsDivisionRow>(us => us
                                .SelectTableFields()
                                .Select(div.Id)
                                .Select(div.ProductsDivision)
                               );

                              //QuotationType HotQuotation
                              model.HotQuotation = connection.Count<QuotationRow>(
                                      Quot.Type == 1 &&
                                      Quot.Date >= DateTime.Now.AddMonths(-1) &&
                                      Quot.Date <= DateTime.Now
                                      );

                              //QuotationType WarmQuotation
                              model.WarmQuotation = connection.Count<QuotationRow>(
                                      Quot.Type == 2 &&
                                      Quot.Date >= DateTime.Now.AddMonths(-1) &&
                                      Quot.Date <= DateTime.Now
                                      );

                              //QuotationType ColdQuotation
                              model.ColdQuotation = connection.Count<QuotationRow>(
                                      Quot.Type == 3 &&
                                      Quot.Date >= DateTime.Now.AddMonths(-1) &&
                                      Quot.Date <= DateTime.Now
                                      );

                              //Percentage Calculation HotQuotation,WarmQuotation and ColdQuotation
                              if (model.HotQuotation > 0)
                              {
                                  model.HotEndvalue = (int)Math.Round((double)(100 * model.HotQuotation) / model.TotalQuotationTypes);
                              }
                              else
                              {
                                  model.HotEndvalue = 0;
                              }
                              if (model.WarmQuotation > 0)
                              {
                                  model.WarmEndvalue = (int)Math.Round((double)(100 * model.WarmQuotation) / model.TotalQuotationTypes);
                              }
                              else
                              {
                                  model.WarmEndvalue = 0;
                              }
                              if (model.ColdQuotation > 0)
                              {
                                  model.ColdEndvalue = (int)Math.Round((double)(100 * model.ColdQuotation) / model.TotalQuotationTypes);
                              }
                              else
                              {
                                  model.ColdEndvalue = 0;
                              }

                              //Quotation Analysis List
                              model.QuotationAnalysisList = connection.List<QuotationRow>(us => us
                             .SelectTableFields()
                             .Select(Quot.Id)
                             .Select(Quot.Date)
                             .Where(Quot.Date >= DateTime.Now.AddMonths(-2))
                             .Where(Quot.Date <= DateTime.Now)
                             );

                              //Citywise Quotation Analysis

                              model.CitywiseQuotationList = connection.List<QuotationRow>(us => us
                                   .SelectTableFields()
                                   .Select(Quot.ContactsCityId)
                                   .Select(Quot.Date)
                                   .Select(Quot.Total)
                                   .Where(Quot.Date >= DateTime.Now.AddMonths(-1))
                                   .Where(Quot.Date <= DateTime.Now)
                                   );

                              model.City = connection.List<CityRow>(us => us
                               .SelectTableFields()
                               .Select(c.City)
                               );

                              //Branchwise Quotation Analysis

                              model.BranchwiseQuotationList = connection.List<QuotationRow>(us => us
                                   .SelectTableFields()
                                   .Select(Quot.BranchId)
                                   .Select(Quot.Date)
                                   .Select(Quot.Total)
                                   .Where(Quot.Date >= DateTime.Now.AddMonths(-1))
                                   .Where(Quot.Date <= DateTime.Now)
                                   );

                              model.Branch = connection.List<BranchRow>(us => us
                               .SelectTableFields()
                               .Select(b.Branch)
                               );

                              model.TargetQuotation = connection.List<TargetSettingRow>(us => us
                               .SelectTableFields()
                               .Select(ts.MonthlyTarget)
                               .Select(ts.MonthlyTargetAmount)
                               .Where(ts.Type == 2)
                             );


                              model.TargetQuotationAchieved = connection.List<QuotationRow>(us => us
                                 .SelectTableFields()
                                 .Select(Quot.Id)
                                 .Select(Quot.Total)
                                 .Where(Quot.ClosingDate >= DateTime.Now.AddMonths(-1))
                                 .Where(Quot.ClosingDate <= DateTime.Now)
                               );



                              //Days in date range picker
                              var v2 = DateTime.DaysInMonth(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month);

                              #region Quotation Target Achievement with respect to count

                              //Count
                              var sum_of_count = model.TargetQuotation.Select(x => x.MonthlyTarget).Sum();

                              double per_day_count_target = (double)sum_of_count / 30;


                              var divisorCnt = ((double)per_day_count_target * v2);

                              //Achieved Target with respect to Count
                              model.AchievedTargetCount = (double)Math.Ceiling((double)model.TargetQuotationAchieved.Select(x => x.Id).Count());



                              if (divisorCnt == 0)
                              {
                                  if (model.AchievedTargetCount == 0)
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
                                  model.QuotationTargetCount = Math.Ceiling(((double)(model.TargetQuotationAchieved.Count()) / divisorCnt) * 100);

                              }


                              //Required Target with respect to Count 

                              model.RequiredTargetCount = (double)Math.Ceiling(per_day_count_target * v2);

                              #endregion

                              #region Quotation Target Achievement with respect to Amount 


                              //Amount
                              var sum_of_amt = model.TargetQuotation.Select(x => x.MonthlyTargetAmount).Sum();

                              double per_day_amt_target = (double)sum_of_amt / 30;

                              var divisorAmt = ((double)per_day_amt_target * v2);

                              //Achieved Target with respect to Amount

                              model.AchievedTargetAmount = (double)Math.Ceiling((double)model.TargetQuotationAchieved.Select(x => x.Total).Sum());




                              if (divisorAmt == 0)
                              {
                                  if (model.AchievedTargetAmount == 0)
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

                                  model.QuotationTargetAmount = Math.Ceiling(((double)(model.TargetQuotationAchieved.Select(x => x.Total).Sum()) / divisorAmt) * 100);
                              }


                              //Required Target with respect to Amount

                              model.RequiredTargetAmount = (double)Math.Ceiling(per_day_amt_target * v2);
                              #endregion

                          }
                          return model;
                      });

                return View(MVC.Views.Premium.QuotationDashboard, cachedModel);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return View(MVC.Views.Premium.QuotationDashboard);
            }
        }

        //Quotation Stages FunnelChart On Date Change
        [HttpGet]
        public ActionResult QuotationStagesFunnelChart(string StartDate, string EndDate)
        {
            try
            {
                var model = new QuotationDashboardPageModel();

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
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return Json("");
            }

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

        //Quotation Status Open, Close and Pending Chart On Date Change
        [HttpGet]
        public ActionResult QuotationStatusChart(string StartDate, string EndDate)
        {
            try
            {
                var model = new QuotationDashboardPageModel();

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
                var Quot = QuotationRow.Fields;

                using (var connection = _connections.NewFor<QuotationRow>())
                {
                    model.QuotationStatusOpen = connection.Count<QuotationRow>(
                        Quot.Status == 1 &&
                        Quot.Date >= model.StartDate &&
                        Quot.Date <= model.EndDate
                        );

                    model.QuotationStatusClosed = connection.Count<QuotationRow>(
                        Quot.Status == 2 &&
                        Quot.Date >= model.StartDate &&
                        Quot.Date <= model.EndDate
                        );

                    model.QuotationStatusPending = connection.Count<QuotationRow>(
                        Quot.Status == 3 &&
                        Quot.Date >= model.StartDate &&
                        Quot.Date <= model.EndDate
                        );

                }
                List<QuotationStatusClass> lst = new List<QuotationStatusClass>();

                var y = new QuotationStatusClass { country = "Open", value = model.QuotationStatusOpen };
                lst.Add(y);

                y = new QuotationStatusClass { country = "Closed", value = model.QuotationStatusClosed };
                lst.Add(y);

                y = new QuotationStatusClass { country = "Pending", value = model.QuotationStatusPending };
                lst.Add(y);

                return Json(lst);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return Json("");
            }
        }

        public class QuotationStatusClass
        {
            [JsonProperty("country")]
            public string country { get; set; }
            [JsonProperty("value")]
            public int value { get; set; }
        }

        //Monthly Quotation Won Loss on Date Change
        [HttpGet]
        public ActionResult QuotationWonLossChart(string StartDate, string EndDate)
        {
            try
            {

                var model = new QuotationDashboardPageModel();

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
                var Quot = QuotationRow.Fields;

                using (var connection = _connections.NewFor<QuotationRow>())
                {
                    model.QuotationWonLost = connection.List<QuotationRow>(us => us
                               .SelectTableFields()
                               .Select(Quot.Id)
                               .Select(Quot.ClosingType)
                               .Select(Quot.Date)
                               .Where(Quot.Date >= model.StartDate)
                               .Where(Quot.Date <= model.EndDate)
                            );
                }

                List<QuotationWonLostClass> lst = new List<QuotationWonLostClass>();

                var dtlst = model.QuotationWonLost.Where(x => x.ClosingType != null).Select(x => x.Date).Distinct();

                foreach (var itm in dtlst)
                {
                    var y = new QuotationWonLostClass
                    {
                        day = itm.Value.ToString("dd/MM"),
                        //won = model.QuotationWonLost.Where(x => x.Date == itm).Where(z => z.ClosingType == (Masters.ClosingTypeMaster)1).Count(),
                        //lost = model.QuotationWonLost.Where(x => x.Date == itm).Where(z => z.ClosingType == (Masters.ClosingTypeMaster)2).Count()
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

        public class QuotationWonLostClass
        {
            [JsonProperty("day")]
            public string day { get; set; }
            [JsonProperty("won")]
            public int won { get; set; }
            [JsonProperty("lost")]
            public int lost { get; set; }
        }

        //Quotation Analysis on Date Change
        [HttpGet]
        public ActionResult QuotationAnalysisChart(string StartDate, string EndDate)
        {
            try
            {
                var model = new QuotationDashboardPageModel();

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
                    model.QuotationStatusClosed = connection.Count<QuotationRow>(
                             Quot.Status == 2 &&
                             Quot.Date >= model.StartDate &&
                             Quot.Date <= model.EndDate
                             );
                    model.QuotationStatusOpen = connection.Count<QuotationRow>(
                              Quot.Status == 1 &&
                              Quot.Date >= model.StartDate &&
                              Quot.Date <= model.EndDate
                              );

                    model.TotalQuotationTypes = connection.Count<QuotationRow>(
                               Quot.Date >= model.StartDate &&
                               Quot.Date <= model.EndDate
                               );
                    model.QuotationStatusPending = connection.Count<QuotationRow>(
                               Quot.Status == 3 &&
                               Quot.Date >= model.StartDate &&
                               Quot.Date <= model.EndDate
                               );

                    model.QuotationWon = connection.Count<QuotationRow>(
                               Quot.ClosingType == 1 &&
                               Quot.Date >= model.StartDate &&
                               Quot.Date <= model.EndDate
                               );


                    model.QuotationLost = connection.Count<QuotationRow>(
                               Quot.ClosingType == 2 &&
                               Quot.Date >= model.StartDate &&
                               Quot.Date <= model.EndDate
                               );
                }
                List<QuotationAnalysisClass> lst = new List<QuotationAnalysisClass>();
                var y = new QuotationAnalysisClass { title = "Total", value = model.TotalQuotationTypes };
                lst.Add(y);

                y = new QuotationAnalysisClass { title = "Open", value = model.QuotationStatusOpen };
                lst.Add(y);

                y = new QuotationAnalysisClass { title = "Closed", value = model.QuotationStatusClosed };
                lst.Add(y);

                y = new QuotationAnalysisClass { title = "Pending", value = model.QuotationStatusPending };
                lst.Add(y);

                y = new QuotationAnalysisClass { title = "Won", value = model.QuotationWon };
                lst.Add(y);

                y = new QuotationAnalysisClass { title = "Lost", value = model.QuotationLost };
                lst.Add(y);

                if ((model.TotalQuotationTypes == 0) && (model.QuotationStatusOpen == 0) && (model.QuotationStatusClosed == 0) && (model.QuotationStatusPending == 0) && (model.QuotationWon == 0) && (model.QuotationLost == 0))
                {
                    return Json("");
                }
                else
                {
                    return Json(lst);
                }
            }

            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return Json("");
            }
        }

        public class QuotationAnalysisClass
        {
            [JsonProperty("title")]
            public string title { get; set; }
            [JsonProperty("value")]
            public int value { get; set; }
        }

        //Mediawise Quotation Chart on Date Change
        [HttpGet]
        public ActionResult MediawiseQuotationChart(string StartDate, string EndDate)
        {
            try
            {

                var model = new QuotationDashboardPageModel();

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
                    model.QuotationSource = connection.List<QuotationRow>(us => us
                             .SelectTableFields()
                             .Select(Quot.Source)
                             .Select(Quot.Date)
                             .Select(Quot.Total)
                             .Where(Quot.Date >= model.StartDate)
                             .Where(Quot.Date <= model.EndDate)
                             );

                }
                List<MediawiseQuotationClass> lst = new List<MediawiseQuotationClass>();
                foreach (var item in model.QuotationSource.GroupBy(y => y.Source).Select(y => new { Source = y.Key, Count = y.Count(), Sum = ((int)y.Sum(x => x.Total)).ToString("0,0##.##") }).OrderByDescending(z => z.Count).ToList())
                {
                    var y = new MediawiseQuotationClass { title = item.Source, value = item.Count, sum = item.Sum };
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

        public class MediawiseQuotationClass
        {
            [JsonProperty("title")]
            public string title { get; set; }
            [JsonProperty("value")]
            public int value { get; set; }
            [JsonProperty("sum")]
            public string sum { get; set; }
        }

        //Most 15 Quotation Product Chart on Date Change
        [HttpGet]
        public ActionResult MostQuotationProductChart(string StartDate, string EndDate)
        {
            try
            {
                var model = new QuotationDashboardPageModel();

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
                var QuotP = QuotationProductsRow.Fields;

                using (var connection = _connections.NewFor<QuotationProductsRow>())
                {
                    model.GroupMostQuotationProduct = connection.List<QuotationProductsRow>(us => us
                              .SelectTableFields()
                              .Select(QuotP.ProductsName)
                              .Select(QuotP.QuotationDate)
                              .Where(QuotP.QuotationDate >= model.StartDate)
                              .Where(QuotP.QuotationDate <= model.EndDate)
                              );
                }
                List<MostQuotationClass> lst = new List<MostQuotationClass>();
                foreach (var item in model.GroupMostQuotationProduct.GroupBy(info => info.ProductsName).Select(group => new
                {
                    Name = group.Key,
                    Count = group.Count(),
                    Sum = group.Sum(x => x.Quantity)
                }).OrderByDescending(x => x.Count).Take(15))
                {
                    var y = new MostQuotationClass { country = item.Name, visits = item.Count };
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

        public class MostQuotationClass
        {
            [JsonProperty("country")]
            public string country { get; set; }
            [JsonProperty("visits")]
            public int visits { get; set; }
        }

        //Least 15 Quotation Product Chart on Date Change
        [HttpGet]
        public ActionResult LeastQuotationProductChart(string StartDate, string EndDate)
        {
            try
            {
                var model = new QuotationDashboardPageModel();

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
                var QuotP = QuotationProductsRow.Fields;

                using (var connection = _connections.NewFor<QuotationProductsRow>())
                {
                    model.GroupLeastQuotationProduct = connection.List<QuotationProductsRow>(us => us
                              .SelectTableFields()
                              .Select(QuotP.ProductsName)
                              .Select(QuotP.QuotationDate)
                              .Where(QuotP.QuotationDate >= model.StartDate)
                              .Where(QuotP.QuotationDate <= model.EndDate)
                              );
                }
                List<LeastQuotationClass> lst = new List<LeastQuotationClass>();
                foreach (var item in model.GroupLeastQuotationProduct.GroupBy(info => info.ProductsName).Select(group => new
                {
                    Name = group.Key,
                    Count = group.Count(),
                    Sum = group.Sum(x => x.Quantity)
                }).OrderBy(x => x.Count).Take(15))
                {
                    var y = new LeastQuotationClass { country = item.Name, visits = item.Count };
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

        public class LeastQuotationClass
        {
            [JsonProperty("country")]
            public string country { get; set; }
            [JsonProperty("visits")]
            public int visits { get; set; }
        }

        //Productwise Division Quotation Chart
        [HttpGet]
        public ActionResult ProductwiseDivisionQuotationChart(string StartDate, string EndDate)
        {

            try
            {
                var model = new QuotationDashboardPageModel();

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
                var QuotP = QuotationProductsRow.Fields;

                using (var connection = _connections.NewFor<QuotationProductsRow>())
                {
                    model.ProductwiseDivisionQuotation = connection.List<QuotationProductsRow>(us => us
                              .SelectTableFields()
                              .Select(QuotP.ProductsDivisionId)
                              .Select(QuotP.ProductsName)
                              .Select(QuotP.QuotationDate)
                              .Where(QuotP.QuotationDate >= model.StartDate)
                              .Where(QuotP.QuotationDate <= model.EndDate)
                              );

                    var div = ProductsDivisionRow.Fields;
                    model.DivisionList = connection.List<ProductsDivisionRow>(us => us
                      .SelectTableFields()
                      .Select(div.Id)
                      .Select(div.ProductsDivision)
                     );
                }

                List<ProductwiseDivisionQuotationClass> lst = new List<ProductwiseDivisionQuotationClass>();
                foreach (var item in model.ProductwiseDivisionQuotation.GroupBy(info => info.ProductsDivisionId).Select(group => new
                {
                    DivId = group.Key,
                    Count = group.Count(),
                }))
                {
                    var y = new ProductwiseDivisionQuotationClass { country = model.DivisionList.SingleOrDefault(x => x.Id == item.DivId).ProductsDivision, visits = item.Count };
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

        public class ProductwiseDivisionQuotationClass
        {
            [JsonProperty("country")]
            public string country { get; set; }
            [JsonProperty("visits")]
            public int visits { get; set; }
        }

        //QuotationTypes Chart on date Change
        [HttpGet]
        public ActionResult QuotationTypesChart(string StartDate, string EndDate)
        {
            try
            {
                var model = new QuotationDashboardPageModel();

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
                    model.HotQuotation = connection.Count<QuotationRow>(
                                Quot.Type == 1 &&
                                Quot.Date >= model.StartDate &&
                                Quot.Date <= model.EndDate
                                );

                    model.WarmQuotation = connection.Count<QuotationRow>(
                        Quot.Type == 2 &&
                        Quot.Date >= model.StartDate &&
                        Quot.Date <= model.EndDate
                        );

                    model.ColdQuotation = connection.Count<QuotationRow>(
                        Quot.Type == 3 &&
                        Quot.Date >= model.StartDate &&
                        Quot.Date <= model.EndDate
                        );

                    model.TotalQuotationTypes = connection.Count<QuotationRow>(
                        Quot.Date >= model.StartDate &&
                        Quot.Date <= model.EndDate
                        );
                    if (model.HotQuotation > 0)
                    {
                        model.HotEndvalue = (int)Math.Round((double)(100 * model.HotQuotation) / model.TotalQuotationTypes);
                    }
                    else
                    {
                        model.HotEndvalue = 0;
                    }
                    if (model.WarmQuotation > 0)
                    {
                        model.WarmEndvalue = (int)Math.Round((double)(100 * model.WarmQuotation) / model.TotalQuotationTypes);
                    }
                    else
                    {
                        model.WarmEndvalue = 0;
                    }
                    if (model.ColdQuotation > 0)
                    {
                        model.ColdEndvalue = (int)Math.Round((double)(100 * model.ColdQuotation) / model.TotalQuotationTypes);
                    }
                    else
                    {
                        model.ColdEndvalue = 0;
                    }
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
                    "\"balloonText\": \"" + model.HotEndvalue.ToString() + "% (Count:" + model.HotQuotation + ")\"}," +
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
                     "\"balloonText\": \"" + model.WarmEndvalue.ToString() + "% (Count:" + model.WarmQuotation + ")\"}," +
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
                   "\"balloonText\": \"" + model.ColdEndvalue.ToString() + "% (Count:" + model.ColdQuotation + ")\"" +
                        "}";
                return Json(jsonString);
            }

            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return Json("");
            }
        }

        //Citywise Quotation Chart on Date Change
        [HttpGet]
        public ActionResult CitywiseQuotationChart(string StartDate, string EndDate)
        {
            try
            {

                var model = new QuotationDashboardPageModel();

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
                    model.City = connection.List<CityRow>(us => us
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
                        cityid = model.City.SingleOrDefault(x => x.Id == item.CityId).City;
                    }

                    var y = new CitywiseQuotationClass { country = cityid, litres = item.Count, sum = item.Sum };
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

        public class CitywiseQuotationClass
        {
            [JsonProperty("country")]
            public string country { get; set; }
            [JsonProperty("litres")]
            public int litres { get; set; }
            [JsonProperty("sum")]
            public int sum { get; set; }
        }

        //Branchwise Quotation Chart on Date Change
        [HttpGet]
        public ActionResult BranchwiseQuotationChart(string StartDate, string EndDate)
        {
            try
            {
                var model = new QuotationDashboardPageModel();

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
                    model.BranchwiseQuotationList = connection.List<QuotationRow>(us => us
                              .SelectTableFields()
                              .Select(Quot.BranchId)
                              .Select(Quot.Date)
                              .Select(Quot.Total)
                              .Where(Quot.Date >= model.StartDate)
                              .Where(Quot.Date <= model.EndDate)
                              );

                    var b = BranchRow.Fields;
                    model.Branch = connection.List<BranchRow>(us => us
                      .SelectTableFields()
                      .Select(b.Branch)
                     );

                }

                List<BranchwiseQuotationClass> lst = new List<BranchwiseQuotationClass>();

                foreach (var item in model.BranchwiseQuotationList.GroupBy(y =>
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

                    var y = new BranchwiseQuotationClass { country = branchid + "\n" + "(Amt:" + item.Sum + ")", visits = item.Count, sum = item.Sum };
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

        public class BranchwiseQuotationClass
        {
            [JsonProperty("country")]
            public string country { get; set; }
            [JsonProperty("visits")]
            public int visits { get; set; }
            [JsonProperty("sum")]
            public int sum { get; set; }
        }


        //OverallTarget QuotationChart  with respect to Count on Date Change
        [HttpGet]
        public ActionResult OverallTargetQuotationChart(string StartDate, string EndDate)
        {
            try
            {
                var model = new QuotationDashboardPageModel();

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
                               .Where(ts.Type == 2)
                             );

                    model.TargetQuotationAchieved = connection.List<QuotationRow>(us => us
                       .SelectTableFields()
                       .Select(Quot.Id)
                       .Select(Quot.Total)
                       .Where(Quot.ClosingDate >= model.StartDate)
                       .Where(Quot.ClosingDate <= model.EndDate)
                     );


                    #region Quotation Target Achievement with respect to count

                    //Count
                    var sum_of_count = model.TargetQuotation.Select(x => x.MonthlyTarget).Sum();

                    double per_day_count_target = (double)sum_of_count / 30;

                    //Days in date range picker     

                    int v2 = (model.EndDate - model.StartDate).Days;

                    var divisorCnt = ((double)per_day_count_target * v2);

                    //Achieved Target with respect to Count
                    model.AchievedTargetCount = (double)Math.Ceiling((double)model.TargetQuotationAchieved.Select(x => x.Id).Count());



                    if (divisorCnt == 0)
                    {
                        if (model.AchievedTargetCount == 0)
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
                        model.QuotationTargetCount = Math.Ceiling(((double)(model.TargetQuotationAchieved.Count()) / divisorCnt) * 100);

                    }

                    //Required Target with respect to Count 

                    model.RequiredTargetCount = (double)Math.Ceiling(per_day_count_target * v2);

                    #endregion


                }
                return Json(new { Percent = model.QuotationTargetCount, RCnt = model.RequiredTargetCount, ACnt = model.AchievedTargetCount });
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return Json("");
            }

        }


        //OverallTarget QuotationChart with respect to Amount on Date Change
        [HttpGet]
        public ActionResult OverallTargetQuotationAmtChart(string StartDate, string EndDate)
        {
            try
            {
                var model = new QuotationDashboardPageModel();

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
                               .Where(ts.Type == 2)
                             );

                    model.TargetQuotationAchieved = connection.List<QuotationRow>(us => us
                       .SelectTableFields()
                       .Select(Quot.Id)
                       .Select(Quot.Total)
                       .Where(Quot.ClosingDate >= model.StartDate)
                       .Where(Quot.ClosingDate <= model.EndDate)
                     );

                    #region Quotation Target Achievement with respect to Amount 


                    //Amount
                    var sum_of_amt = model.TargetQuotation.Select(x => x.MonthlyTargetAmount).Sum();

                    double per_day_amt_target = (double)sum_of_amt / 30;


                    //Days in date range picker

                    int v2 = (model.EndDate - model.StartDate).Days;

                    var divisorAmt = ((double)per_day_amt_target * v2);

                    //Achieved Target with respect to Amount

                    model.AchievedTargetAmount = (double)Math.Ceiling((double)model.TargetQuotationAchieved.Select(x => x.Total).Sum());




                    if (divisorAmt == 0)
                    {
                        if (model.AchievedTargetAmount == 0)
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

                        model.QuotationTargetAmount = Math.Ceiling(((double)(model.TargetQuotationAchieved.Select(x => x.Total).Sum()) / divisorAmt) * 100);
                    }


                    //Required Target with respect to Amount

                    model.RequiredTargetAmount = (double)Math.Ceiling(per_day_amt_target * v2);
                    #endregion

                }
                return Json(new { Per = model.QuotationTargetAmount, RAmt = model.RequiredTargetAmount, AAmt = model.AchievedTargetAmount });
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return Json("");
            }

        }
    }
}