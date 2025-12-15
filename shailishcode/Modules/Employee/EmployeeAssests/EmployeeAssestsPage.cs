
namespace AdvanceCRM.Employee.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Employee/EmployeeAssests")]
    [PageAuthorize(typeof(EmployeeAssestsRow))]
    public class EmployeeAssestsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Default/EmployeeAssests/EmployeeAssestsIndex.cshtml");
        }
    }
}