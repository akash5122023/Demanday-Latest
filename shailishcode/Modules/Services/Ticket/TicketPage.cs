
namespace AdvanceCRM.Services.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Services/Ticket")]
    [PageAuthorize(typeof(TicketRow))]
    public class TicketController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Services/Ticket/TicketIndex.cshtml");
        }
    }
}