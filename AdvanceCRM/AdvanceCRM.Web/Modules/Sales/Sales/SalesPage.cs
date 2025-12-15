
namespace AdvanceCRM.Sales.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Sales")]
    [PageAuthorize(typeof(SalesRow))]
    public class SalesController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Sales/Sales/SalesIndex.cshtml");
        }
    }
}