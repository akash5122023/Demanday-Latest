using AdvanceCRM.Quotation;
using Serenity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Reports.Pages
{
    [Route("Reports/UserDetail")]
    [PageAuthorize(typeof(QuotationRow))]
    public class UserDetailReportController : Controller
    {
        public ActionResult Index()
        {
            return View(MVC.Views.Reports.UserDetail.UserDetailReportIndex);
        }
    }
}