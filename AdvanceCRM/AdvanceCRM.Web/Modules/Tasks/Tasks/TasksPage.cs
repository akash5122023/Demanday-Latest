
namespace AdvanceCRM.Tasks.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Tasks")]
    [PageAuthorize(typeof(TasksRow))]
    public class TasksController : Controller
    {
        public ActionResult Index()
        {
            return View(MVC.Views.Tasks.Tasks_.TasksIndex);
        }
    }
}