
namespace AdvanceCRM.ThirdParty.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("ThirdParty/SulekhaDetails")]
    [PageAuthorize(typeof(SulekhaDetailsRow))]
    public class SulekhaDetailsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/ThirdParty/SulekhaDetails/SulekhaDetailsIndex.cshtml");
        }
    }
}