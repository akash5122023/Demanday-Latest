
namespace AdvanceCRM.Enquiry.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Enquiry")]
    [PageAuthorize(typeof(EnquiryRow))]
    public class EnquiryController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Enquiry/Enquiry/EnquiryIndex.cshtml");
        }
    }
}