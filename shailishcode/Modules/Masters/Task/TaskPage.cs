
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/Task")]
    [PageAuthorize(typeof(TaskRow))]
    public class TaskController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/Task/TaskIndex.cshtml");
        }
    }
}