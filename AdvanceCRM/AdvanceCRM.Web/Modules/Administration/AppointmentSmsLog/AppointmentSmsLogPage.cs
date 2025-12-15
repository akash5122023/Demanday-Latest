
namespace AdvanceCRM.Administration.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Administration/AppointmentSmsLog")]
    [PageAuthorize(typeof(AppointmentSmsLogRow))]
    public class AppointmentSmsLogController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Administration/AppointmentSmsLog/AppointmentSmsLogIndex.cshtml");
        }
    }
}