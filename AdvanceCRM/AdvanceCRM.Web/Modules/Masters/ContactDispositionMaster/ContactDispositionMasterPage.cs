using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Masters.Pages
{

    [PageAuthorize(typeof(ContactDispositionMasterRow))]
    public class ContactDispositionMasterController : Controller
    {
        [Route("Masters/ContactDispositionMaster")]
        public ActionResult Index()
        {
            return View("~/Modules/Masters/ContactDispositionMaster/ContactDispositionMasterIndex.cshtml");
        }
    }
}