
namespace AdvanceCRM.Settings.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Settings/MailChimpConfiguration")]
    [PageAuthorize(typeof(MailChimpConfigurationRow))]
    public class MailChimpConfigurationController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/MailChimpConfiguration/MailChimpConfigurationIndex.cshtml");
        }
    }
}