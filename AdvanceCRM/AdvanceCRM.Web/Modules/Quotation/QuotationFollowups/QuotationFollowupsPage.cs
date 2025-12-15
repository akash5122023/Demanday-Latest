
namespace AdvanceCRM.Quotation.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Reports/QuotationFollowups")]
    [PageAuthorize(typeof(QuotationFollowupsRow))]
    public class QuotationFollowupsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Quotation/QuotationFollowups/QuotationFollowupsIndex.cshtml");
        }
    }
}