using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Demanday.Pages
{

    [PageAuthorize(typeof(DemandayMisRow))]
    public class DemandayMisController : Controller
    {
        [Route("Demanday/DemandayMis")]
        public ActionResult Index()
        {
            return View("~/Modules/Demanday/DemandayMis/DemandayMisIndex.cshtml");
        }
    }
}