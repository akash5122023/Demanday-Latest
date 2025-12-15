
namespace AdvanceCRM.Quotation.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Reports/QuotationAppointments")]
    [PageAuthorize(typeof(QuotationAppointmentsRow))]
    public class QuotationAppointmentsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Quotation/QuotationAppointments/QuotationAppointmentsIndex.cshtml");
        }
    }
}