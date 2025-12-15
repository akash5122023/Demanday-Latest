
namespace AdvanceCRM.Administration.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Administration/LogInOutLog")]
    [PageAuthorize(typeof(LogInOutLogRow))]
    public class LogInOutLogController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Administration/LogInOutLog/LogInOutLogIndex.cshtml");
        }
    }
}