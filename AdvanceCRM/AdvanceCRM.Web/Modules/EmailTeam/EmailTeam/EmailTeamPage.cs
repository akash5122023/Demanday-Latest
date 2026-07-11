using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.EmailTeam.Pages
{
    [PageAuthorize(typeof(EmailTeamRow))]
    public class EmailTeamController : Controller
    {
        [Route("EmailTeam/EmailTeam")]
        public ActionResult Index()
        {
            return View("~/Modules/EmailTeam/EmailTeam/EmailTeamIndex.cshtml");
        }
    }
}
