
namespace AdvanceCRM.Settings.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Settings/WebsiteEnquiryConfiguration")]
    [PageAuthorize(typeof(WebsiteEnquiryConfigurationRow))]
    public class WebsiteEnquiryConfigurationController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/WebsiteEnquiryConfiguration/WebsiteEnquiryConfigurationIndex.cshtml");
        }
    }
}