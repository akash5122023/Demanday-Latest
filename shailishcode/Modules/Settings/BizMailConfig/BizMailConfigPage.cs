
namespace AdvanceCRM.Settings.Pages
{
    using Serenity;
    using Serenity.Web;

    using Microsoft.AspNetCore.Mvc;
    [Route("Settings/BizMailConfig")]
    [PageAuthorize(typeof(BizMailConfigRow))]
    public class BizMailConfigController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/BizMailConfig/BizMailConfigIndex.cshtml");
        }
    }
}