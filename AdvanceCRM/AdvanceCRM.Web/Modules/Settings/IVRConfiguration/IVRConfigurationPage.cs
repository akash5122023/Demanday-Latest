
namespace AdvanceCRM.Settings.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Settings/IVRConfiguration")]
    [PageAuthorize(typeof(IVRConfigurationRow))]
    public class IVRConfigurationController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/IVRConfiguration/IVRConfigurationIndex.cshtml");
        }
    }
}