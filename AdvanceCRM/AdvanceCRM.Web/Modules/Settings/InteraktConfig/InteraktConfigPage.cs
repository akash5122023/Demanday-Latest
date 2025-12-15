
namespace AdvanceCRM.Settings.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Settings/InteraktConfig")]
    [PageAuthorize(typeof(InteraktConfigRow))]
    public class InteraktConfigController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/InteraktConfig/InteraktConfigIndex.cshtml");
        }
    }
}