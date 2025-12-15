using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Demanday.Pages
{

    [PageAuthorize(typeof(DemandayTeleMarketingMISRow))]
    public class DemandayTeleMarketingMISController : Controller
    {
        [Route("Demanday/DemandayTeleMarketingMIS")]
        public ActionResult Index()
        {
            return View("~/Modules/Demanday/DemandayTeleMarketingMIS/DemandayTeleMarketingMISIndex.cshtml");
        }
    }
}