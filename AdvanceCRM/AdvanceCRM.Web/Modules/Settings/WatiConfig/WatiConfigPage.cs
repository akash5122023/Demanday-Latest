
namespace AdvanceCRM.Settings.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Settings/WatiConfig")]
    [PageAuthorize(typeof(WatiConfigRow))]
    public class WatiConfigController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/WatiConfig/WatiConfigIndex.cshtml");
        }
    }
}