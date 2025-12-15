using AdvanceCRM.Enquiry;
using Serenity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Reports.Pages
{
    [Route("Reports/Enquiry")]
    [PageAuthorize(typeof(EnquiryRow))]
    public class EnquiryReportController : Controller
    {
        public ActionResult Index()
        {
            return View(MVC.Views.Reports.Enquiry.EnquiryReportIndex);
        }
    }
}