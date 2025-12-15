using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Demanday.Pages
{

    [PageAuthorize(typeof(DemandayTeamLeaderRow))]
    public class DemandayTeamLeaderController : Controller
    {
        [Route("Demanday/DemandayTeamLeader")]
        public ActionResult Index()
        {
            return View("~/Modules/Demanday/DemandayTeamLeader/DemandayTeamLeaderIndex.cshtml");
        }
    }
}