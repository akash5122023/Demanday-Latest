
namespace AdvanceCRM.ThirdParty.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("ThirdParty/InstamojoDetails")]
    [PageAuthorize(typeof(InstamojoDetailsRow))]
    public class InstamojoDetailsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/ThirdParty/InstamojoDetails/InstamojoDetailsIndex.cshtml");
        }
    }
}