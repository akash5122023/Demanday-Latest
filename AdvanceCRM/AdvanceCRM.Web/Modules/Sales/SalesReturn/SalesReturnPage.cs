
namespace AdvanceCRM.Sales.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("SalesReturn")]
    [PageAuthorize(typeof(SalesReturnRow))]
    public class SalesReturnController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Sales/SalesReturn/SalesReturnIndex.cshtml");
        }
    }
}