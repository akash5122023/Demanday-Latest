
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/Department")]
    [PageAuthorize(typeof(DepartmentRow))]
    public class DepartmentController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/Department/DepartmentIndex.cshtml");
        }
    }
}