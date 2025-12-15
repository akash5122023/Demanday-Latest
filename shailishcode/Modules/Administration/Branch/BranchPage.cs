
namespace AdvanceCRM.Administration.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Administration/Branch")]
    [PageAuthorize(typeof(BranchRow))]
    public class BranchController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Administration/Branch/BranchIndex.cshtml");
        }
    }
}