
namespace AdvanceCRM.BizMail.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("BizMail/BizMailCms")]
    [PageAuthorize(typeof(BizMailCmsRow))]
    public class BizMailCmsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/BizMail/BizMailCms/BizMailCmsIndex.cshtml");
        }
    }
}