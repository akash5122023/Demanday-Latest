
namespace AdvanceCRM.BizMail.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("BizMail/BizMailContact")]
    [PageAuthorize(typeof(BizMailContactRow))]
    public class BizMailContactController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/BizMail/BizMailContact/BizMailContactIndex.cshtml");
        }
    }
}