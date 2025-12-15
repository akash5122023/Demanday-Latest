
namespace AdvanceCRM.Purchase.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("PurchaseOrder")]
    [PageAuthorize(typeof(PurchaseOrderRow))]
    public class PurchaseOrderController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Purchase/PurchaseOrder/PurchaseOrderIndex.cshtml");
        }
    }
}