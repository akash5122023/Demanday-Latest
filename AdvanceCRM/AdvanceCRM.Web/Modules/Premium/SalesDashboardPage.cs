namespace AdvanceCRM.Modules.Premium
{
    using AdvanceCRM.Administration;
using AdvanceCRM.Web.Helpers;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Premium;
    using AdvanceCRM.Sales;
    using Newtonsoft.Json;
    using Serenity;
    using Serenity.Data;
    using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    


[Route("SalesDashboard")]
[ReadPermission("Premium:Dashboards")]
public class SalesDashboardController : Controller
{
    private readonly ISqlConnections _connections;
    private readonly IRequestContext Context;

    public SalesDashboardController(ISqlConnections connections, IRequestContext context)
    {
        _connections = connections ?? throw new ArgumentNullException(nameof(connections));
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet, Route("~/Premium/Sales")]
    public ActionResult Index()
        {
            try
            {
                var cachedModel = LocalCache.GetLocalStoreOnly("SalesDashboardPageModel", TimeSpan.FromSeconds(1),
                     SalesRow.Fields.GenerationKey, () =>
                     {
                         var model = new SalesDashboardPageModel();
                         var user = (UserDefinition)Context.User.ToUserDefinition();
                         var Sal = SalesRow.Fields;
                         var SalP = SalesProductsRow.Fields;
                         var c = CityRow.Fields;
                         var b = BranchRow.Fields;
                         var ts = TargetSettingRow.Fields;

                         using (var connection = _connections.NewFor<SalesRow>())
                         {
                             //Mediawise Sales
                             model.SalesSource = connection.List<SalesRow>(us => us
                                   .SelectTableFields()
                                   .Select(Sal.Source)
                                   .Select(Sal.Date)
                                   .Select(Sal.Total)
                                   .Where(Sal.Date >= DateTime.Now.AddMonths(-1))
                                   .Where(Sal.Date <= DateTime.Now)
                                   );

                             //Sales Status Open   
                             model.SalesStatusOpen = connection.Count<SalesRow>(
                                     Sal.Status == 1 &&
                                     Sal.Date >= DateTime.Now.AddMonths(-1) &&
                                     Sal.Date <= DateTime.Now
                                     );

                             //Sales Status Close
                             model.SalesStatusClosed = connection.Count<SalesRow>(
                                     Sal.Status == 2 &&
                                     Sal.Date >= DateTime.Now.AddMonths(-1) &&
                                     Sal.Date <= DateTime.Now
                                     );

                             //Sales Status Pending
                             model.SalesStatusPending = connection.Count<SalesRow>(
                                     Sal.Status == 3 &&
                                     Sal.Date >= DateTime.Now.AddMonths(-1) &&
                                     Sal.Date <= DateTime.Now
                                     );

                             //Productwise Division Quotation
                             model.ProductwiseDivisionSales = connection.List<SalesProductsRow>(us => us
                                   .SelectTableFields()
                                   .Select(SalP.ProductsDivisionId)
                                   .Select(SalP.ProductsName)
                                   .Select(SalP.SalesDate)
                                   .Where(SalP.SalesDate >= DateTime.Now.AddMonths(-1))
                                   .Where(SalP.SalesDate <= DateTime.Now)
                                  );

                             //DivisionList
                             var div = ProductsDivisionRow.Fields;
                             model.DivisionList = connection.List<ProductsDivisionRow>(us => us
                               .SelectTableFields()
                               .Select(div.Id)
                               .Select(div.ProductsDivision)
                              );

                             //Total SalesTypes
                             model.TotalSalesTypes = connection.Count<SalesRow>(
                                     Sal.Date >= DateTime.Now.AddMonths(-1) &&
                                     Sal.Date <= DateTime.Now
                                     );

                             //Most Sales Product
                             model.MostSalesProduct = connection.List<SalesProductsRow>(us => us
                                   .SelectTableFields()
                                   .Select(SalP.ProductsName)
                                   .Select(SalP.SalesDate)
                                   .Where(SalP.SalesDate >= DateTime.Now.AddMonths(-1))
                                   .Where(SalP.SalesDate <= DateTime.Now)
                                   );

                             //Sales Analysis Cash Credit for last 30 days
                             model.SalesAnalysisList = connection.List<SalesRow>(us => us
                                .SelectTableFields()
                                .Select(Sal.Id)
                                .Select(Sal.Date)
                                .Select(Sal.Type)
                                .Where(Sal.Date >= DateTime.Now.AddMonths(-1))
                                .Where(Sal.Date <= DateTime.Now)
                                );

                             //Sales Analysis for Yearly
                             model.SalesAnalysisYearlyList = connection.List<SalesRow>(us => us
                             .SelectTableFields()
                             .Select(Sal.Id)
                             .Select(Sal.Date)
                             .Select(Sal.Total)
                             .Where(Sal.Date >= DateTime.Now.AddYears(-1))
                             .Where(Sal.Date <= DateTime.Now)
                             );

                             //Sales Analysis for last three months
                             model.LastthreeMonthsSalesAnalysisList = connection.List<SalesRow>(us => us
                                 .SelectTableFields()
                                 .Select(Sal.Id)
                                 .Select(Sal.Date)
                                 .Select(Sal.Total)
                                 .Where(Sal.Date <= DateTime.Now)
                                 .Where(Sal.Date >= DateTime.Now.AddMonths(-3))
                                );

                             //Citywise Sales Analysis

                             model.CitywiseSalesList = connection.List<SalesRow>(us => us
                                  .SelectTableFields()
                                  .Select(Sal.ContactsCityId)
                                  .Select(Sal.Date)
                                  .Select(Sal.Total)
                                  .Where(Sal.Date >= DateTime.Now.AddMonths(-1))
                                  .Where(Sal.Date <= DateTime.Now)
                                  );

                             model.City = connection.List<CityRow>(us => us
                              .SelectTableFields()
                              .Select(c.City)
                              );

                             //Branchwise Sales Analysis

                             model.BranchwiseSalesList = connection.List<SalesRow>(us => us
                                  .SelectTableFields()
                                  .Select(Sal.BranchId)
                                  .Select(Sal.Date)
                                  .Select(Sal.Total)
                                  .Where(Sal.Date >= DateTime.Now.AddMonths(-1))
                                  .Where(Sal.Date <= DateTime.Now)
                                  );

                             model.Branch = connection.List<BranchRow>(us => us
                              .SelectTableFields()
                              .Select(b.Branch)
                              );

                             model.TargetSales = connection.List<TargetSettingRow>(us => us
                              .SelectTableFields()
                              .Select(ts.MonthlyTarget)
                              .Select(ts.MonthlyTargetAmount)
                              .Where(ts.Type == 3)
                            );


                             model.TargetSalesAchieved = connection.List<SalesRow>(us => us
                                .SelectTableFields()
                                .Select(Sal.Id)
                                .Select(Sal.Total)
                                .Where(Sal.Date >= DateTime.Now.AddMonths(-1))
                                .Where(Sal.Date <= DateTime.Now)
                              );


                             //Days in date range picker
                             var v2 = DateTime.DaysInMonth(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month);


                             #region Sales Target Achievement with respect to count

                             //Count
                             var sum_of_count = model.TargetSales.Select(x => x.MonthlyTarget).Sum();

                             double per_day_count_target = (double)sum_of_count / 30;


                             var divisorCnt = ((double)per_day_count_target * v2);

                             //Achieved Target with respect to Count
                             model.AchievedTargetCount = (double)Math.Ceiling((double)model.TargetSalesAchieved.Select(x => x.Id).Count());



                             if (divisorCnt == 0)
                             {
                                 if (model.AchievedTargetCount == 0)
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
                                 model.SalesTargetCount = Math.Ceiling(((model.TargetSalesAchieved.Count()) / divisorCnt) * 100);

                             }

                             //Required Target with respect to Count 

                             model.RequiredTargetCount = (double)Math.Ceiling(per_day_count_target * v2);

                             #endregion

                             #region Sales Target Achievement with respect to Amount 

                             //Amount
                             var sum_of_amt = model.TargetSales.Select(x => x.MonthlyTargetAmount).Sum();

                             double per_day_amt_target = (double)sum_of_amt / 30;

                             var divisorAmt = ((double)per_day_amt_target * v2);

                             //Achieved Target with respect to Amount

                             model.AchievedTargetAmount = (double)Math.Ceiling((double)model.TargetSalesAchieved.Select(x => x.Total).Sum());




                             if (divisorAmt == 0)
                             {
                                 if (model.AchievedTargetAmount == 0)
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

                                 model.SalesTargetAmount = Math.Ceiling(((double)(model.TargetSalesAchieved.Select(x => x.Total).Sum()) / divisorAmt) * 100);

                             }


                             //Required Target with respect to Amount

                             model.RequiredTargetAmount = (double)Math.Ceiling(per_day_amt_target * v2);
                             #endregion


                         }
                         return model;
                     });
                return View(MVC.Views.Premium.SalesDashboard, cachedModel);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return View(MVC.Views.Premium.SalesDashboard);
            }
        }

        //Mediawise Sales Chart on Date Change
        [HttpGet]
        public ActionResult MediawiseSalesChart(string StartDate, string EndDate)
        {
            try
            {
                var model = new SalesDashboardPageModel();

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
                    model.SalesSource = connection.List<SalesRow>(us => us
                             .SelectTableFields()
                             .Select(Sal.Source)
                             .Select(Sal.Date)
                             .Select(Sal.Total)
                             .Where(Sal.Date >= model.StartDate)
                             .Where(Sal.Date <= model.EndDate)
                             );

                }
                List<MediawiseSalesClass> lst = new List<MediawiseSalesClass>();
                foreach (var item in model.SalesSource.GroupBy(y => y.Source).Select(y => new { Source = y.Key, Count = y.Count(), Sum = ((int)y.Sum(x => x.Total)).ToString("0,0##.##") }).OrderByDescending(z => z.Count).ToList())
                {
                    var y = new MediawiseSalesClass { title = item.Source, value = item.Count, sum = item.Sum };
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

        public class MediawiseSalesClass
        {
            [JsonProperty("title")]
            public string title { get; set; }
            [JsonProperty("value")]
            public int value { get; set; }
            [JsonProperty("sum")]
            public string sum { get; set; }
        }

        //Sales Status on Date Change
        [HttpGet]
        public ActionResult SalesStatusChart(string StartDate, string EndDate)
        {
            try
            {
                var model = new SalesDashboardPageModel();

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
                    model.SalesStatusClosed = connection.Count<SalesRow>(
                             Sal.Status == 2 &&
                             Sal.Date >= model.StartDate &&
                             Sal.Date <= model.EndDate
                             );
                    model.SalesStatusOpen = connection.Count<SalesRow>(
                              Sal.Status == 1 &&
                              Sal.Date >= model.StartDate &&
                              Sal.Date <= model.EndDate
                              );

                    model.TotalSalesTypes = connection.Count<SalesRow>(
                               Sal.Date >= model.StartDate &&
                               Sal.Date <= model.EndDate
                               );
                    model.SalesStatusPending = connection.Count<SalesRow>(
                               Sal.Status == 3 &&
                               Sal.Date >= model.StartDate &&
                               Sal.Date <= model.EndDate
                               );
                }
                List<SalesStatusClass> lst = new List<SalesStatusClass>();
                var y = new SalesStatusClass { title = "Total", value = model.TotalSalesTypes };
                lst.Add(y);

                y = new SalesStatusClass { title = "Open", value = model.SalesStatusOpen };
                lst.Add(y);

                y = new SalesStatusClass { title = "Closed", value = model.SalesStatusClosed };
                lst.Add(y);

                y = new SalesStatusClass { title = "Pending", value = model.SalesStatusPending };
                lst.Add(y);


                if ((model.TotalSalesTypes == 0) && (model.SalesStatusOpen == 0) && (model.SalesStatusClosed == 0) && (model.SalesStatusPending == 0))
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

        public class SalesStatusClass
        {
            [JsonProperty("title")]
            public string title { get; set; }
            [JsonProperty("value")]
            public int value { get; set; }
        }

        //Productwise Division Sales Chart
        [HttpGet]
        public ActionResult ProductwiseDivisionSalesChart(string StartDate, string EndDate)
        {
            try
            {
                var model = new SalesDashboardPageModel();

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
                    model.ProductwiseDivisionSales = connection.List<SalesProductsRow>(us => us
                              .SelectTableFields()
                              .Select(SalP.ProductsDivisionId)
                              .Select(SalP.ProductsName)
                              .Select(SalP.SalesDate)
                              .Where(SalP.SalesDate >= model.StartDate)
                              .Where(SalP.SalesDate <= model.EndDate)
                              );

                    var div = ProductsDivisionRow.Fields;
                    model.DivisionList = connection.List<ProductsDivisionRow>(us => us
                      .SelectTableFields()
                      .Select(div.Id)
                      .Select(div.ProductsDivision)
                     );
                }

                List<ProductwiseDivisionSalesClass> lst = new List<ProductwiseDivisionSalesClass>();
                foreach (var item in model.ProductwiseDivisionSales.GroupBy(info => info.ProductsDivisionId).Select(group => new
                {
                    DivId = group.Key,
                    Count = group.Count(),
                }))
                {
                    var y = new ProductwiseDivisionSalesClass { country = model.DivisionList.SingleOrDefault(x => x.Id == item.DivId).ProductsDivision, visits = item.Count };
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


        public class ProductwiseDivisionSalesClass
        {
            [JsonProperty("country")]
            public string country { get; set; }
            [JsonProperty("visits")]
            public int visits { get; set; }
        }

        //Most 15 Sales Product Chart on Date Change
        [HttpGet]
        public ActionResult MostSalesProductChart(string StartDate, string EndDate)
        {
            try
            {
                var model = new SalesDashboardPageModel();

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
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return Json("");
            }
        }

        public class MostSalesClass
        {
            [JsonProperty("country")]
            public string country { get; set; }
            [JsonProperty("visits")]
            public int visits { get; set; }
        }

        //Citywise Sales Chart on Date Change
        [HttpGet]
        public ActionResult CitywiseSalesChart(string StartDate, string EndDate)
        {
            try
            {
                var model = new SalesDashboardPageModel();

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
                    model.City = connection.List<CityRow>(us => us
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
                        cityid = model.City.SingleOrDefault(x => x.Id == item.CityId).City;
                    }

                    var y = new CitywiseSalesClass { country = cityid, litres = item.Count, sum = item.Sum };
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

        public class CitywiseSalesClass
        {
            [JsonProperty("country")]
            public string country { get; set; }
            [JsonProperty("litres")]
            public int litres { get; set; }
            [JsonProperty("sum")]
            public int sum { get; set; }
        }

        //Branchwise Sales Chart on Date Change
        [HttpGet]
        public ActionResult BranchwiseSalesChart(string StartDate, string EndDate)
        {
            try
            {
                var model = new SalesDashboardPageModel();

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
                    model.BranchwiseSalesList = connection.List<SalesRow>(us => us
                              .SelectTableFields()
                              .Select(Sal.BranchId)
                              .Select(Sal.Date)
                              .Select(Sal.Total)
                              .Where(Sal.Date >= model.StartDate)
                              .Where(Sal.Date <= model.EndDate)
                              );

                    var b = BranchRow.Fields;
                    model.Branch = connection.List<BranchRow>(us => us
                      .SelectTableFields()
                      .Select(b.Branch)
                     );

                }

                List<BranchwiseSalesClass> lst = new List<BranchwiseSalesClass>();

                foreach (var item in model.BranchwiseSalesList.GroupBy(y =>
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

                    var y = new BranchwiseSalesClass { country = branchid + "\n" + "(Amt:" + item.Sum + ")", visits = item.Count, sum = item.Sum };
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

        public class BranchwiseSalesClass
        {
            [JsonProperty("country")]
            public string country { get; set; }
            [JsonProperty("visits")]
            public int visits { get; set; }
            [JsonProperty("sum")]
            public int sum { get; set; }
        }

        //OverallTarget SalesChart  with respect to Count on Date Change
        [HttpGet]
        public ActionResult OverallTargetSalesChart(string StartDate, string EndDate)
        {
            try
            {

                var model = new SalesDashboardPageModel();

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
                               .Where(ts.Type == 3)
                             );

                    model.TargetSalesAchieved = connection.List<SalesRow>(us => us
                       .SelectTableFields()
                       .Select(Sal.Id)
                       .Select(Sal.Total)
                       .Where(Sal.Date >= model.StartDate)
                       .Where(Sal.Date <= model.EndDate)
                     );

                    int v2 = (model.EndDate - model.StartDate).Days;

                    #region Sales Target Achievement with respect to count

                    //Count
                    var sum_of_count = model.TargetSales.Select(x => x.MonthlyTarget).Sum();

                    double per_day_count_target = (double)sum_of_count / 30;


                    var divisorCnt = ((double)per_day_count_target * v2);

                    //Achieved Target with respect to Count
                    model.AchievedTargetCount = (double)Math.Ceiling((double)model.TargetSalesAchieved.Select(x => x.Id).Count());



                    if (divisorCnt == 0)
                    {
                        if (model.AchievedTargetCount == 0)
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
                        model.SalesTargetCount = Math.Ceiling(((model.TargetSalesAchieved.Count()) / divisorCnt) * 100);

                    }

                    //Required Target with respect to Count 

                    model.RequiredTargetCount = (double)Math.Ceiling(per_day_count_target * v2);

                    #endregion
                }
                return Json(new { Percent = model.SalesTargetCount, RCnt = model.RequiredTargetCount, ACnt = model.AchievedTargetCount });
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return Json("");
            }
        }


        //OverallTarget SalesChart with respect to Amount on Date Change
        [HttpGet]
        public ActionResult OverallTargetSalesAmtChart(string StartDate, string EndDate)
        {
            try
            {
                var model = new SalesDashboardPageModel();

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
                               .Where(ts.Type == 3)
                             );

                    model.TargetSalesAchieved = connection.List<SalesRow>(us => us
                       .SelectTableFields()
                       .Select(Sal.Id)
                       .Select(Sal.Total)
                       .Where(Sal.Date >= model.StartDate)
                       .Where(Sal.Date <= model.EndDate)
                     );


                    //Days in date range picker

                    int v2 = (model.EndDate - model.StartDate).Days;

                    #region Sales Target Achievement with respect to Amount 

                    //Amount
                    var sum_of_amt = model.TargetSales.Select(x => x.MonthlyTargetAmount).Sum();

                    double per_day_amt_target = (double)sum_of_amt / 30;

                    var divisorAmt = ((double)per_day_amt_target * v2);

                    //Achieved Target with respect to Amount

                    model.AchievedTargetAmount = (double)Math.Ceiling((double)model.TargetSalesAchieved.Select(x => x.Total).Sum());




                    if (divisorAmt == 0)
                    {
                        if (model.AchievedTargetAmount == 0)
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

                        model.SalesTargetAmount = Math.Ceiling(((double)(model.TargetSalesAchieved.Select(x => x.Total).Sum()) / divisorAmt) * 100);

                    }


                    //Required Target with respect to Amount

                    model.RequiredTargetAmount = (double)Math.Ceiling(per_day_amt_target * v2);
                    #endregion


                }
                return Json(new { Per = model.SalesTargetAmount, RAmt = model.RequiredTargetAmount, AAmt = model.AchievedTargetAmount });
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return Json("");
            }
        }

    }
}