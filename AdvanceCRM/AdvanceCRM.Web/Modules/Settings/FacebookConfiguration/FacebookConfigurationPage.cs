
namespace AdvanceCRM.Settings.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Settings/FacebookConfiguration")]
    [PageAuthorize(typeof(FacebookConfigurationRow))]
    public class FacebookConfigurationController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/FacebookConfiguration/FacebookConfigurationIndex.cshtml");
        }
    }
}