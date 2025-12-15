
namespace AdvanceCRM.Reports.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Reports/QuotationProducts")]
    [PageAuthorize(typeof(QuotationProductsRow))]
    public class QuotationProductsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Reports/QuotationProducts/QuotationProductsIndex.cshtml");
        }
    }
}