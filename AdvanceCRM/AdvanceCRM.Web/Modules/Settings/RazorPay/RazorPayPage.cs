
namespace AdvanceCRM.Settings.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Settings/RazorPay")]
    [PageAuthorize(typeof(RazorPayRow))]
    public class RazorPayController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/RazorPay/RazorPayIndex.cshtml");
        }
    }
}