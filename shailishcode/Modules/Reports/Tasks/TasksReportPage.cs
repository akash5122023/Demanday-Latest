using AdvanceCRM.Tasks;
using Serenity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Reports.Pages
{
    [Route("Reports/TasksReport")]
    [PageAuthorize(typeof(TasksRow))]
    public class TasksReportController : Controller
    {
        public ActionResult Index()
        {
            return View(MVC.Views.Reports.Tasks.TasksReportIndex);
        }
    }
}