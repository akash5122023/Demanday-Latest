
namespace AdvanceCRM.ThirdParty.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("ThirdParty/KnowlarityIvr")]
    [PageAuthorize(typeof(KnowlarityIvrRow))]
    public class KnowlarityIvrController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/ThirdParty/KnowlarityIvr/KnowlarityIvrIndex.cshtml");
        }
    }
}