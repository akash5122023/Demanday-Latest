
namespace AdvanceCRM.Purchase.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("PurchaseReturn")]
    [PageAuthorize(typeof(PurchaseReturnRow))]
    public class PurchaseReturnController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Purchase/PurchaseReturn/PurchaseReturnIndex.cshtml");
        }
    }
}