
namespace AdvanceCRM.ThirdParty.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("ThirdParty/WoocommerceDetails")]
    [PageAuthorize(typeof(WoocommerceDetailsRow))]
    public class WoocommerceDetailsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/ThirdParty/WoocommerceDetails/WoocommerceDetailsIndex.cshtml");
        }
    }
}