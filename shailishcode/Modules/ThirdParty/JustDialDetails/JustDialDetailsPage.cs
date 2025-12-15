
namespace AdvanceCRM.ThirdParty.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("ThirdParty/JustDialDetails")]
    [PageAuthorize(typeof(JustDialDetailsRow))]
    public class JustDialDetailsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/ThirdParty/JustDialDetails/JustDialDetailsIndex.cshtml");
        }
    }
}