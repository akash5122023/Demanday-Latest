using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Demanday.Pages
{

    [PageAuthorize(typeof(DemandayEnquiryRow))]
    public class DemandayEnquiryController : Controller
    {
        [Route("Demanday/DemandayEnquiry")]
        public ActionResult Index()
        {
            return View("~/Modules/Demanday/DemandayEnquiry/DemandayEnquiryIndex.cshtml");
        }
    }
}