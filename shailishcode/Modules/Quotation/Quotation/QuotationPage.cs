
namespace AdvanceCRM.Quotation.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Quotation")]
    [PageAuthorize(typeof(QuotationRow))]
    public class QuotationController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Quotation/Quotation/QuotationIndex.cshtml");
        }
    }
}