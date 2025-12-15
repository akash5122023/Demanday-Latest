using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Demanday.Pages
{

    [PageAuthorize(typeof(DemandayTeleMarketingContactsRow))]
    public class DemandayTeleMarketingContactsController : Controller
    {
        [Route("Demanday/DemandayTeleMarketingContacts")]
        public ActionResult Index()
        {
            return View("~/Modules/Demanday/DemandayTeleMarketingContacts/DemandayTeleMarketingContactsIndex.cshtml");
        }
    }
}