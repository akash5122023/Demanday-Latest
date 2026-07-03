using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Masters.Pages
{

    [PageAuthorize(typeof(DemandaySubIndustryMasterRow))]
    public class DemandaySubIndustryMasterController : Controller
    {
        [Route("Masters/DemandaySubIndustryMaster")]
        public ActionResult Index()
        {
            return View("~/Modules/Masters/DemandaySubIndustryMaster/DemandaySubIndustryMasterIndex.cshtml");
        }
    }
}