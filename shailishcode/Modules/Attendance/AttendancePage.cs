
namespace AdvanceCRM.Attendance.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Attendance")]
    [PageAuthorize(typeof(AttendanceRow))]
    public class AttendanceController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Attendance/AttendanceIndex.cshtml");
        }
    }
}