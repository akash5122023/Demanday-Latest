using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Toolkit.Pages
{

    [PageAuthorize(typeof(DemandaySpecsRow))]
    public class DemandaySpecsController : Controller
    {
        [Route("Toolkit/DemandaySpecs")]
        public ActionResult Index()
        {
            return View("~/Modules/Toolkit/DemandaySpecs/DemandaySpecsIndex.cshtml");
        }
    }
}