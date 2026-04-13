using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Toolkit.Pages
{

    [PageAuthorize(typeof(DemandayCompetitorRow))]
    public class DemandayCompetitorController : Controller
    {
        [Route("Toolkit/DemandayCompetitor")]
        public ActionResult Index()
        {
            return View("~/Modules/Toolkit/DemandayCompetitor/DemandayCompetitorIndex.cshtml");
        }
    }
}