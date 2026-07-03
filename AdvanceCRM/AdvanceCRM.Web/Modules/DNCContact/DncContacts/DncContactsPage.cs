using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.DNCContact.Pages
{

    [PageAuthorize(typeof(DncContactsRow))]
    public class DncContactsController : Controller
    {
        [Route("DNCContact/DncContacts")]
        public ActionResult Index()
        {
            return View("~/Modules/DNCContact/DncContacts/DncContactsIndex.cshtml");
        }
    }
}