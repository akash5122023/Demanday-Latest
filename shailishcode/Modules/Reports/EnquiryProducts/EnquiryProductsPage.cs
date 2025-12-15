
namespace AdvanceCRM.Reports.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Reports/EnquiryProducts")]
    [PageAuthorize(typeof(EnquiryProductsRow))]
    public class EnquiryProductsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Reports/EnquiryProducts/EnquiryProductsIndex.cshtml");
        }
    }
}