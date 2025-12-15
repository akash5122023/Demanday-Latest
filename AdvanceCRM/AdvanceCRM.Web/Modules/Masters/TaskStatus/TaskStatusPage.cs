
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/TaskStatus")]
    [PageAuthorize(typeof(TaskStatusRow))]
    public class TaskStatusController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/TaskStatus/TaskStatusIndex.cshtml");
        }
    }
}