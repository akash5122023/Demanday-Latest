
namespace AdvanceCRM.Administration.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Administration/DailyWishesLog")]
    [PageAuthorize(typeof(DailyWishesLogRow))]
    public class DailyWishesLogController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Administration/DailyWishesLog/DailyWishesLogIndex.cshtml");
        }
    }
}