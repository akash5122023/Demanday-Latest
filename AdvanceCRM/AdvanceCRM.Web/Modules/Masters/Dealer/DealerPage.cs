
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/Dealer")]
    [PageAuthorize(typeof(DealerRow))]
    public class DealerController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/Dealer/DealerIndex.cshtml");
        }
    }
}