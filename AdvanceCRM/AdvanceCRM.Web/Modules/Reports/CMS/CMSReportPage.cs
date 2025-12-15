
using AdvanceCRM.Services;
using Serenity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Reports.Pages
{
    [Route("Reports/CMS")]
    [PageAuthorize(typeof(CMSRow))]
    public class CMSReportController : Controller
    {
        public ActionResult Index()
        {
            return View(MVC.Views.Reports.CMS.CMSReportIndex);
        }
    }
}