
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/State")]
    [PageAuthorize(typeof(StateRow))]
    public class StateController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/State/StateIndex.cshtml");
        }
    }
}