
namespace AdvanceCRM.BizMail.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("BizMail/BizMailTask")]
    [PageAuthorize(typeof(BizMailTaskRow))]
    public class BizMailTaskController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/BizMail/BizMailTask/BizMailTaskIndex.cshtml");
        }
    }
}