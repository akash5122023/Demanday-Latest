using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Masters.Pages
{

    [PageAuthorize(typeof(ContactMeetingModeMasterRow))]
    public class ContactMeetingModeMasterController : Controller
    {
        [Route("Masters/ContactMeetingModeMaster")]
        public ActionResult Index()
        {
            return View("~/Modules/Masters/ContactMeetingModeMaster/ContactMeetingModeMasterIndex.cshtml");
        }
    }
}