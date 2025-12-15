using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Demanday.Pages
{

    [PageAuthorize(typeof(DemandayTeleMarketingQualiltyRow))]
    public class DemandayTeleMarketingQualiltyController : Controller
    {
        [Route("Demanday/DemandayTeleMarketingQualilty")]
        public ActionResult Index()
        {
            return View("~/Modules/Demanday/DemandayTeleMarketingQualilty/DemandayTeleMarketingQualiltyIndex.cshtml");
        }
    }
}