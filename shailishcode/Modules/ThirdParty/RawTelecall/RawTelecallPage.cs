
namespace AdvanceCRM.ThirdParty.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("ThirdParty/RawTelecall")]
    [PageAuthorize(typeof(RawTelecallRow))]
    public class RawTelecallController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/ThirdParty/RawTelecall/RawTelecallIndex.cshtml");
        }
    }
}