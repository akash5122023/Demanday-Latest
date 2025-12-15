
namespace AdvanceCRM.Settings.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Settings/Sulekha")]
    [PageAuthorize(typeof(SulekhaRow))]
    public class SulekhaController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/Sulekha/SulekhaIndex.cshtml");
        }
    }
}