
namespace AdvanceCRM.ThirdParty.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("ThirdParty/RpPaymentDetails")]
    [PageAuthorize(typeof(RpPaymentDetailsRow))]
    public class RpPaymentDetailsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/ThirdParty/RpPaymentDetails/RpPaymentDetailsIndex.cshtml");
        }
    }
}