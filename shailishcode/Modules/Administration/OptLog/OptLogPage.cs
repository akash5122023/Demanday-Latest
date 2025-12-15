
namespace AdvanceCRM.Administration.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Administration/OptLog")]
    [PageAuthorize(typeof(OptLogRow))]
    public class OptLogController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Administration/OptLog/OptLogIndex.cshtml");
        }
    }
}