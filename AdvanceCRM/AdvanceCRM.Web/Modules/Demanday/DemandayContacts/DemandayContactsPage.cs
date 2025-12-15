using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Demanday.Pages
{

    [PageAuthorize(typeof(DemandayContactsRow))]
    public class DemandayContactsController : Controller
    {
        [Route("Demanday/DemandayContacts")]
        public ActionResult Index()
        {
            return View("~/Modules/Demanday/DemandayContacts/DemandayContactsIndex.cshtml");
        }
    }
}