
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/Project")]
    [PageAuthorize(typeof(ProjectRow))]
    public class ProjectController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/Project/ProjectIndex.cshtml");
        }
    }
}