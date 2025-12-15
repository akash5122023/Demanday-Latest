
namespace AdvanceCRM.BizMail.Pages
{
    using Serenity;
    using Serenity.Web;

    using Microsoft.AspNetCore.Mvc;
    [Route("BizMail/BizMailIvr")]
    [PageAuthorize(typeof(BizMailIvrRow))]
    public class BizMailIvrController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/BizMail/BizMailIvr/BizMailIvrIndex.cshtml");
        }
    }
}