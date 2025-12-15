using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Demanday.Pages
{

    [PageAuthorize(typeof(DemandayTeleMarketingTeamLeaderRow))]
    public class DemandayTeleMarketingTeamLeaderController : Controller
    {
        [Route("Demanday/DemandayTeleMarketingTeamLeader")]
        public ActionResult Index()
        {
            return View("~/Modules/Demanday/DemandayTeleMarketingTeamLeader/DemandayTeleMarketingTeamLeaderIndex.cshtml");
        }
    }
}