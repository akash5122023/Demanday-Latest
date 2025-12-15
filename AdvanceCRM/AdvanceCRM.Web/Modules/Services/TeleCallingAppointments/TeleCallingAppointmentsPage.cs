
namespace AdvanceCRM.Services.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Services/TeleCallingAppointments")]
    [PageAuthorize(typeof(TeleCallingAppointmentsRow))]
    public class TeleCallingAppointmentsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Services/TeleCallingAppointments/TeleCallingAppointmentsIndex.cshtml");
        }
    }
}