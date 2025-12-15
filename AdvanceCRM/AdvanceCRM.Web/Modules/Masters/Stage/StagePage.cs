
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/Stage")]
    [PageAuthorize(typeof(StageRow))]
    public class StageController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/Stage/StageIndex.cshtml");
        }
    }
}