
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/Area")]
    [PageAuthorize(typeof(AreaRow))]
    public class AreaController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/Area/AreaIndex.cshtml");
        }
    }
}