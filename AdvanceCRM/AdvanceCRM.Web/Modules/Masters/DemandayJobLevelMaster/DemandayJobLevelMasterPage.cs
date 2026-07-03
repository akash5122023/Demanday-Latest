using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Masters.Pages
{

    [PageAuthorize(typeof(DemandayJobLevelMasterRow))]
    public class DemandayJobLevelMasterController : Controller
    {
        [Route("Masters/DemandayJobLevelMaster")]
        public ActionResult Index()
        {
            return View("~/Modules/Masters/DemandayJobLevelMaster/DemandayJobLevelMasterIndex.cshtml");
        }
    }
}