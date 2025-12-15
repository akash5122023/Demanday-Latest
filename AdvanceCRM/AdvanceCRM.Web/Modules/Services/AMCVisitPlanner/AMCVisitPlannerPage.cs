
namespace AdvanceCRM.Services.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Services/AMCVisitPlanner")]
    [PageAuthorize(typeof(AMCVisitPlannerRow))]
    public class AMCVisitPlannerController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Services/AMCVisitPlanner/AMCVisitPlannerIndex.cshtml");
        }
    }
}