
namespace AdvanceCRM.Settings.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Settings/TradeIndiaConfiguration")]
    [PageAuthorize(typeof(TradeIndiaConfigurationRow))]
    public class TradeIndiaConfigurationController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/TradeIndiaConfiguration/TradeIndiaConfigurationIndex.cshtml");
        }
    }
}