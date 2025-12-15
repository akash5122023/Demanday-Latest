
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/Teams"), Route("Team"), Route("Teams")]
    [PageAuthorize(typeof(TeamsRow))]
    public class TeamsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/Teams/TeamsIndex.cshtml");
        }
    }
}