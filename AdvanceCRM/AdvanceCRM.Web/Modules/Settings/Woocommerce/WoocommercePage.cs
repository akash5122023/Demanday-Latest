
namespace AdvanceCRM.Settings.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Settings/Woocommerce")]
    [PageAuthorize(typeof(WoocommerceRow))]
    public class WoocommerceController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/Woocommerce/WoocommerceIndex.cshtml");
        }
    }
}