
namespace AdvanceCRM.BizMail.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("BizMail/BmList")]
    [PageAuthorize(typeof(BmListRow))]
    public class BmListController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/BizMail/BmList/BmListIndex.cshtml");
        }
    }
}