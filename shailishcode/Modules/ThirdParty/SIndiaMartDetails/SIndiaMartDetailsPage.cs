
namespace AdvanceCRM.ThirdParty.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("ThirdParty/SIndiaMartDetails")]
    [PageAuthorize(typeof(SIndiaMartDetailsRow))]
    public class SIndiaMartDetailsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/ThirdParty/SIndiaMartDetails/SIndiaMartDetailsIndex.cshtml");
        }
    }
}