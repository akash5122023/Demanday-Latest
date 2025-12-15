
namespace AdvanceCRM.Settings.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Settings/IndiaMartConfiguration")]
    [PageAuthorize(typeof(IndiaMartConfigurationRow))]
    public class IndiaMartConfigurationController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/IndiaMartConfiguration/IndiaMartConfigurationIndex.cshtml");
        }
    }
}