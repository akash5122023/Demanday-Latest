
namespace AdvanceCRM.Employee.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Employee/Employee")]
    [PageAuthorize(typeof(EmployeeRow))]
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Employee/Employee/EmployeeIndex.cshtml");
        }
    }
}