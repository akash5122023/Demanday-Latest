
namespace AdvanceCRM.Settings.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Settings/WaConfigration")]
    [PageAuthorize(typeof(WaConfigrationRow))]
    public class WaConfigrationController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/WaConfigration/WaConfigrationIndex.cshtml");
        }
    }
}