
namespace AdvanceCRM.BizMail.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("BizMail/BizMailWooCom")]
    [PageAuthorize(typeof(BizMailWooComRow))]
    public class BizMailWooComController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/BizMail/BizMailWooCom/BizMailWooComIndex.cshtml");
        }
    }
}