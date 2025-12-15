using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Demanday.Pages
{

    [PageAuthorize(typeof(DemandayQualityRow))]
    public class DemandayQualityController : Controller
    {
        [Route("Demanday/DemandayQuality")]
        public ActionResult Index()
        {
            return View("~/Modules/Demanday/DemandayQuality/DemandayQualityIndex.cshtml");
        }
    }
}