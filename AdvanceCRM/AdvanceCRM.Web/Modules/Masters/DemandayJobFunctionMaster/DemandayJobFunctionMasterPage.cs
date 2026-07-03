using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Masters.Pages
{

    [PageAuthorize(typeof(DemandayJobFunctionMasterRow))]
    public class DemandayJobFunctionMasterController : Controller
    {
        [Route("Masters/DemandayJobFunctionMaster")]
        public ActionResult Index()
        {
            return View("~/Modules/Masters/DemandayJobFunctionMaster/DemandayJobFunctionMasterIndex.cshtml");
        }
    }
}