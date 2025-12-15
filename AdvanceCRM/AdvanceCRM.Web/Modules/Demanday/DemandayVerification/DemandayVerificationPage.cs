using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Demanday.Pages
{

    [PageAuthorize(typeof(DemandayVerificationRow))]
    public class DemandayVerificationController : Controller
    {
        [Route("Demanday/DemandayVerification")]
        public ActionResult Index()
        {
            return View("~/Modules/Demanday/DemandayVerification/DemandayVerificationIndex.cshtml");
        }
    }
}