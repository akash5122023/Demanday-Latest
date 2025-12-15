
namespace AdvanceCRM.ThirdParty.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("ThirdParty/FacebookDetails")]
    [PageAuthorize(typeof(FacebookDetailsRow))]
    public class FacebookDetailsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/ThirdParty/FacebookDetails/FacebookDetailsIndex.cshtml");
        }
    }
}