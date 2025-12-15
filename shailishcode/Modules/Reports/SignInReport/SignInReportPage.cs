using AdvanceCRM.Administration;
using Serenity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Reports.Pages
{
    [Route("Reports/SignInReport")]
    [PageAuthorize(typeof(LogInOutLogRow))]
    public class SignInReportController : Controller
    {
        public ActionResult Index()
        {
            return View(MVC.Views.Reports.SignInReport.SignInReportIndex);
        }
    }
}