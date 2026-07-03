using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Masters.Pages
{

    [PageAuthorize(typeof(DemandayEmployeeSizeMasterRow))]
    public class DemandayEmployeeSizeMasterController : Controller
    {
        [Route("Masters/DemandayEmployeeSizeMaster")]
        public ActionResult Index()
        {
            return View("~/Modules/Masters/DemandayEmployeeSizeMaster/DemandayEmployeeSizeMasterIndex.cshtml");
        }
    }
}