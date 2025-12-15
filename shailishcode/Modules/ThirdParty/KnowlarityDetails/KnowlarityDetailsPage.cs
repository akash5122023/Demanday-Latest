
namespace AdvanceCRM.ThirdParty.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("ThirdParty/KnowlarityDetails")]
    [PageAuthorize(typeof(KnowlarityDetailsRow))]
    public class KnowlarityDetailsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/ThirdParty/KnowlarityDetails/KnowlarityDetailsIndex.cshtml");
        }
    }
}