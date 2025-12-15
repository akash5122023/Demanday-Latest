
namespace AdvanceCRM.Enquiry.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Reports/EnquiryFollowups")]
    [PageAuthorize(typeof(EnquiryFollowupsRow))]
    public class EnquiryFollowupsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Enquiry/EnquiryFollowups/EnquiryFollowupsIndex.cshtml");
        }
    }
}