
namespace AdvanceCRM.Services.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Services/TeleCalling")]
    [PageAuthorize(typeof(TeleCallingRow))]
    public class TeleCallingController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Services/TeleCalling/TeleCallingIndex.cshtml");
        }
    }
}