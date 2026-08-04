using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Toolkit.Pages
{
    [PageAuthorize(typeof(ToolkitTMEnquiryRow))]
    public class ToolkitTMEnquiryController : Controller
    {
        [Route("Toolkit/ToolkitTMEnquiry")]
        public ActionResult Index()
        {
            return View("~/Modules/Toolkit/TMEnquiry/ToolkitTMEnquiryIndex.cshtml");
        }
    }
}
