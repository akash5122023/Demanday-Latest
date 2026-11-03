using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Masters.Pages
{

    [PageAuthorize(typeof(ContactAccountStatusMasterRow))]
    public class ContactAccountStatusMasterController : Controller
    {
        [Route("Masters/ContactAccountStatusMaster")]
        public ActionResult Index()
        {
            return View("~/Modules/Masters/ContactAccountStatusMaster/ContactAccountStatusMasterIndex.cshtml");
        }
    }
}