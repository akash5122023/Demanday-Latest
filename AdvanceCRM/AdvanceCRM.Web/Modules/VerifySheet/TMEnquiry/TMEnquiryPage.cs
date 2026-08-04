using Microsoft.AspNetCore.Mvc;
using Serenity.Web;

namespace AdvanceCRM.VerifySheet.Pages
{
    [PageAuthorize(typeof(TMEnquiryRow))]
    public class TMEnquiryPage : Controller
    {
        [Route("VerifySheet/TMEnquiry")]
        public ActionResult Index()
        {
            return View("~/Modules/VerifySheet/TMEnquiry/TMEnquiryIndex.cshtml");
        }
    }
}
