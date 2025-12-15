namespace AdvanceCRM.Settings.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Serenity.Web;

    [Route("Settings/CouponCodes")]
    [PageAuthorize(typeof(CouponCodeRow))]
    public class CouponCodesController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/CouponCodes/CouponCodesIndex.cshtml");
        }
    }
}
