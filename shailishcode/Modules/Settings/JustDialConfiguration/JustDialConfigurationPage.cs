
namespace AdvanceCRM.Settings.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Settings/JustDialConfiguration")]
    [PageAuthorize(typeof(JustDialConfigurationRow))]
    public class JustDialConfigurationController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/JustDialConfiguration/JustDialConfigurationIndex.cshtml");
        }
    }
}