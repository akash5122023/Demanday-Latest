
namespace AdvanceCRM.ThirdParty.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("ThirdParty/TradeIndiaDetails")]
    [PageAuthorize(typeof(TradeIndiaDetailsRow))]
    public class TradeIndiaDetailsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/ThirdParty/TradeIndiaDetails/TradeIndiaDetailsIndex.cshtml");
        }
    }
}