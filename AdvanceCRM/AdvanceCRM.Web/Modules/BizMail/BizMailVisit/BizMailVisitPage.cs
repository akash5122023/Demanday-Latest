
namespace AdvanceCRM.BizMail.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("BizMail/BizMailVisit")]
    [PageAuthorize(typeof(BizMailVisitRow))]
    public class BizMailVisitController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/BizMail/BizMailVisit/BizMailVisitIndex.cshtml");
        }
    }
}