
namespace AdvanceCRM.BizMail.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("BizMail/BizMailWeb")]
    [PageAuthorize(typeof(BizMailWebRow))]
    public class BizMailWebController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/BizMail/BizMailWeb/BizMailWebIndex.cshtml");
        }
    }
}