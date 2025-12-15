using AdvanceCRM.Quotation;
using Serenity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Reports.Pages
{
    [Route("Reports/Quotation")]
    [PageAuthorize(typeof(QuotationRow))]
    public class QuotationReportController : Controller
    {
        public ActionResult Index()
        {
            return View(MVC.Views.Reports.Quotation.QuotationReportIndex);
        }
    }
}