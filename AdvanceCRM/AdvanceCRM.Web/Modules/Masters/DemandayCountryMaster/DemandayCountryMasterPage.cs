using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Masters.Pages
{

    [PageAuthorize(typeof(DemandayCountryMasterRow))]
    public class DemandayCountryMasterController : Controller
    {
        [Route("Masters/DemandayCountryMaster")]
        public ActionResult Index()
        {
            return View("~/Modules/Masters/DemandayCountryMaster/DemandayCountryMasterIndex.cshtml");
        }
    }
}