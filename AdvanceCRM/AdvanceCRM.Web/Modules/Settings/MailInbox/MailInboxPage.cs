
namespace AdvanceCRM.Settings.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Settings/MailInbox")]
    [PageAuthorize(typeof(MailInboxRow))]
    public class MailInboxController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/MailInbox/MailInboxIndex.cshtml");
        }
    }
}