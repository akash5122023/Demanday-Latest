
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/Purpose")]
    [PageAuthorize(typeof(PurposeRow))]
    public class PurposeController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/Purpose/PurposeIndex.cshtml");
        }
    }
}