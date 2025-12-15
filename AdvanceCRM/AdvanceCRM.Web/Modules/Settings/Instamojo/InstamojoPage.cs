
namespace AdvanceCRM.Settings.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Settings/Instamojo")]
    [PageAuthorize(typeof(InstamojoRow))]
    public class InstamojoController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/Instamojo/InstamojoIndex.cshtml");
        }
    }
}