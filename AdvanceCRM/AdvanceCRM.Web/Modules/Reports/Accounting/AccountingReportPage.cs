
using AdvanceCRM.Accounting;

using Serenity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Reports.Pages
{
    [Route("Reports/Accounting")]
    [PageAuthorize(typeof(CashbookRow))]
    public class AccountingReportController : Controller
    {
        public ActionResult Index()
        {
            return View(MVC.Views.Reports.Accounting.AccountingReportIndex);
        }
    }
}