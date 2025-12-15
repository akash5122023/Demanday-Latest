using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Demanday.Pages
{

    [PageAuthorize(typeof(EnquiryContactsRow))]
    public class EnquiryContactsController : Controller
    {
        [Route("Demanday/EnquiryContacts")]
        public ActionResult Index()
        {
            return View("~/Modules/Demanday/EnquiryContacts/EnquiryContactsIndex.cshtml");
        }
    }
}