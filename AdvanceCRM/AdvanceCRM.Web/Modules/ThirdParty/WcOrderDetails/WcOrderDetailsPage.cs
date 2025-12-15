
namespace AdvanceCRM.ThirdParty.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("ThirdParty/WcOrderDetails")]
    [PageAuthorize(typeof(WcOrderDetailsRow))]
    public class WcOrderDetailsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/ThirdParty/WcOrderDetails/WcOrderDetailsIndex.cshtml");
        }
    }
}