
namespace AdvanceCRM.BizMail.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("BizMail/BizMailEnquiry")]
    [PageAuthorize(typeof(BizMailEnquiryRow))]
    public class BizMailEnquiryController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/BizMail/BizMailEnquiry/BizMailEnquiryIndex.cshtml");
        }
    }
}