using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Masters.Pages
{

    [PageAuthorize(typeof(ContactOutreachModeMasterRow))]
    public class ContactOutreachModeMasterController : Controller
    {
        [Route("Masters/ContactOutreachModeMaster")]
        public ActionResult Index()
        {
            return View("~/Modules/Masters/ContactOutreachModeMaster/ContactOutreachModeMasterIndex.cshtml");
        }
    }
}