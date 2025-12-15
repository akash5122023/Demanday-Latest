
namespace AdvanceCRM.Sales.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Reports/InvoiceFollowups")]
    [PageAuthorize(typeof(InvoiceFollowupsRow))]
    public class InvoiceFollowupsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Sales/InvoiceFollowups/InvoiceFollowupsIndex.cshtml");
        }
    }
}