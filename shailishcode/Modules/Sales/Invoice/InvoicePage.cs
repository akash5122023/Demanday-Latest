
namespace AdvanceCRM.Sales.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Proforma")]
    [PageAuthorize(typeof(InvoiceRow))]
    public class InvoiceController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Sales/Invoice/InvoiceIndex.cshtml");
        }
    }
}