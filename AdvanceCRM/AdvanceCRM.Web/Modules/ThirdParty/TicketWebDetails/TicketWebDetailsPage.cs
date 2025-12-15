
namespace AdvanceCRM.ThirdParty.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("ThirdParty/TicketWebDetails")]
    [PageAuthorize(typeof(TicketWebDetailsRow))]
    public class TicketWebDetailsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/ThirdParty/TicketWebDetails/TicketWebDetailsIndex.cshtml");
        }
    }
}