
namespace AdvanceCRM.Products.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("StockTransfer")]
    [PageAuthorize(typeof(StockTransferRow))]
    public class StockTransferController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Products/StockTransfer/StockTransferIndex.cshtml");
        }
    }
}