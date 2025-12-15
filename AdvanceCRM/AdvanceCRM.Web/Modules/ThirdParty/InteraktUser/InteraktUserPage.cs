
namespace AdvanceCRM.ThirdParty.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("ThirdParty/InteraktUser")]
    [PageAuthorize(typeof(InteraktUserRow))]
    public class InteraktUserController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/ThirdParty/InteraktUser/InteraktUserIndex.cshtml");
        }
    }
}