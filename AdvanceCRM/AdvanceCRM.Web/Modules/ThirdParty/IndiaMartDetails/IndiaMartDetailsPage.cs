
namespace AdvanceCRM.ThirdParty.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("ThirdParty/IndiaMartDetails")]
    [PageAuthorize(typeof(IndiaMartDetailsRow))]
    public class IndiaMartDetailsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/ThirdParty/IndiaMartDetails/IndiaMartDetailsIndex.cshtml");
        }
    }
}