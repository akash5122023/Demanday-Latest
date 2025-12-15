
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/Transportation")]
    [PageAuthorize(typeof(TransportationRow))]
    public class TransportationController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/Transportation/TransportationIndex.cshtml");
        }
    }
}