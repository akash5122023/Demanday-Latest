
namespace AdvanceCRM.Services.Pages
{
    using Serenity;
    using Serenity.Web;

    using Microsoft.AspNetCore.Mvc;
    [Route("Services/TeleCallingFollowups")]
    [PageAuthorize(typeof(TeleCallingFollowupsRow))]
    public class TeleCallingFollowupsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Services/TeleCallingFollowups/TeleCallingFollowupsIndex.cshtml");
        }
    }
}