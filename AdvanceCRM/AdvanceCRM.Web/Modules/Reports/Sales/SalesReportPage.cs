
using AdvanceCRM.Products;
using Serenity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Reports.Pages
{
    [Route("Reports/Sales")]
    [PageAuthorize(typeof(ProductsRow))]
    public class SalesReportController : Controller
    {
        public ActionResult Index()
        {
            return View(MVC.Views.Reports.Sales.SalesReportIndex);
        }
    }
}