using AdvanceCRM.Enquiry;
using AdvanceCRM.Quotation;
using Serenity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Reports.Pages
{
    [Route("Reports/CEOReport")]
    //[PageAuthorize(typeof(EnquiryRow))]
    public class CEOReportController : Controller
    {
        public ActionResult Index()
        {
            return View(MVC.Views.Reports.CEO.CEOReport);
        }
    }
}