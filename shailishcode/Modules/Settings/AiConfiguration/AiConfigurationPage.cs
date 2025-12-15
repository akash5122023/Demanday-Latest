using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Settings.Pages
{

    [PageAuthorize(typeof(AiConfigurationRow))]
    public class AiConfigurationController : Controller
    {
        [Route("Settings/AiConfiguration")]
        public ActionResult Index()
        {
            return View("~/Modules/Settings/AiConfiguration/AiConfigurationIndex.cshtml");
        }
    }
}