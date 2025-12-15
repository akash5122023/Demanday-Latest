
namespace AdvanceCRM.Settings.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Settings/TicketWeb")]
    [PageAuthorize(typeof(TicketWebRow))]
    public class TicketWebController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/TicketWeb/TicketWebIndex.cshtml");
        }
    }
}