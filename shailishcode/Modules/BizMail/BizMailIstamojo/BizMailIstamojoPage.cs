
namespace AdvanceCRM.BizMail.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("BizMail/BizMailIstamojo")]
    [PageAuthorize(typeof(BizMailIstamojoRow))]
    public class BizMailIstamojoController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/BizMail/BizMailIstamojo/BizMailIstamojoIndex.cshtml");
        }
    }
}