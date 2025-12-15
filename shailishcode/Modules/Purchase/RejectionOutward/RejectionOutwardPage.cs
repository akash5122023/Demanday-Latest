using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Purchase.Pages
{
    [Route("RejectionOutward")]

    [PageAuthorize(typeof(RejectionOutwardRow))]
    public class RejectionOutwardController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Purchase/RejectionOutward/RejectionOutwardIndex.cshtml");
        }
    }
}