
namespace AdvanceCRM.Sales.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Reports/InvoiceAppointments")]
    [PageAuthorize(typeof(InvoiceAppointmentsRow))]
    public class InvoiceAppointmentsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Sales/InvoiceAppointments/InvoiceAppointmentsIndex.cshtml");
        }
    }
}