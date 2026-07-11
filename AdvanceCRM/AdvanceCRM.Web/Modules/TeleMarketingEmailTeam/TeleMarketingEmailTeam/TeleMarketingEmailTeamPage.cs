using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.TeleMarketingEmailTeam.Pages
{
    [PageAuthorize(typeof(TeleMarketingEmailTeamRow))]
    public class TeleMarketingEmailTeamController : Controller
    {
        [Route("TeleMarketingEmailTeam/TeleMarketingEmailTeam")]
        public ActionResult Index()
        {
            return View("~/Modules/TeleMarketingEmailTeam/TeleMarketingEmailTeam/TeleMarketingEmailTeamIndex.cshtml");
        }
    }
}
