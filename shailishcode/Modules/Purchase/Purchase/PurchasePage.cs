
namespace AdvanceCRM.Purchase.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Purchase")]
    [PageAuthorize(typeof(PurchaseRow))]
    public class PurchaseController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Purchase/Purchase/PurchaseIndex.cshtml");
        }
    }
}