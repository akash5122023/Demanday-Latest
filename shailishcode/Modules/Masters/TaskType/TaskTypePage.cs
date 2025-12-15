
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/TaskType")]
    [PageAuthorize(typeof(TaskTypeRow))]
    public class TaskTypeController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/TaskType/TaskTypeIndex.cshtml");
        }
    }
}