
namespace AdvanceCRM.ThirdParty.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("ThirdParty/WebsiteEnquiry")]
    [PageAuthorize(typeof(WebsiteEnquiryRow))]
    public class WebsiteEnquiryController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/ThirdParty/WebsiteEnquiry/WebsiteEnquiryIndex.cshtml");
        }
    }
}