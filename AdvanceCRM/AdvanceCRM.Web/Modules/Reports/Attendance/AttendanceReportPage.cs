using AdvanceCRM.Attendance;

using Serenity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Reports.Pages
{
    [Route("Reports/Attendance")]
    [PageAuthorize(typeof(AttendanceRow))]
    public class AttendanceReportController : Controller
    {
        public ActionResult Index()
        {
            return View(MVC.Views.Reports.Attendance.AttendanceReportIndex);
        }
    }
}