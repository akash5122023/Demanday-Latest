
namespace AdvanceCRM.Sales.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Reports/SalesFollowups")]
    [PageAuthorize(typeof(SalesFollowupsRow))]
    public class SalesFollowupsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Sales/SalesFollowups/SalesFollowupsIndex.cshtml");
        }
    }
}