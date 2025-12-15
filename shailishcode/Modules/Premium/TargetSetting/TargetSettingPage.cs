
namespace AdvanceCRM.Premium.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Premium/TargetSetting")]
    [PageAuthorize(typeof(TargetSettingRow))]
    public class TargetSettingController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Premium/TargetSetting/TargetSettingIndex.cshtml");
        }
    }
}