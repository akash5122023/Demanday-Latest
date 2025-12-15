
namespace AdvanceCRM.ThirdParty.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("ThirdParty/MailInboxDetails")]
    [PageAuthorize(typeof(MailInboxDetailsRow))]
    public class MailInboxDetailsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/ThirdParty/MailInboxDetails/MailInboxDetailsIndex.cshtml");
        }
    }
}