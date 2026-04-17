using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Masters.Pages
{

    [PageAuthorize(typeof(DemandayMasterAccountRow))]
    public class DemandayMasterAccountController : Controller
    {
        [Route("Masters/DemandayMasterAccount")]
        public ActionResult Index()
        {
            return View("~/Modules/Masters/DemandayMasterAccount/DemandayMasterAccountIndex.cshtml");
        }
    }
}