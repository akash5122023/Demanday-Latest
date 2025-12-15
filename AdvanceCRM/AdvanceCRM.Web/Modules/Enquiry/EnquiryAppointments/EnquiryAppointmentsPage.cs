
namespace AdvanceCRM.Enquiry.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Reports/EnquiryAppointments")]
    [PageAuthorize(typeof(EnquiryAppointmentsRow))]
    public class EnquiryAppointmentsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Enquiry/EnquiryAppointments/EnquiryAppointmentsIndex.cshtml");
        }
    }
}