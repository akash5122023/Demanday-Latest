
namespace AdvanceCRM.Settings.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Settings/SMSConfiguration")]
    [PageAuthorize(typeof(SMSConfigurationRow))]
    public class SMSConfigurationController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/SMSConfiguration/SMSConfigurationIndex.cshtml");
        }
    }
}