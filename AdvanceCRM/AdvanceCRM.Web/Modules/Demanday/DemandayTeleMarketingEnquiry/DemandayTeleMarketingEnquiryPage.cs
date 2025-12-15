using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Demanday.Pages
{

    [PageAuthorize(typeof(DemandayTeleMarketingEnquiryRow))]
    public class DemandayTeleMarketingEnquiryController : Controller
    {
        [Route("Demanday/DemandayTeleMarketingEnquiry")]
        public ActionResult Index()
        {
            return View("~/Modules/Demanday/DemandayTeleMarketingEnquiry/DemandayTeleMarketingEnquiryIndex.cshtml");
        }
    }
}