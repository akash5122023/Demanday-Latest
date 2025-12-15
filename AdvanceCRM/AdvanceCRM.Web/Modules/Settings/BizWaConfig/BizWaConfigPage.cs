
namespace AdvanceCRM.Settings.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Settings/BizWaConfig")]
    [PageAuthorize(typeof(BizWaConfigRow))]
    public class BizWaConfigController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/BizWaConfig/BizWaConfigIndex.cshtml");
        }
    }
}