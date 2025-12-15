
namespace AdvanceCRM.BizMail.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("BizMail/BmSubscribers")]
    [PageAuthorize(typeof(BmSubscribersRow))]
    public class BmSubscribersController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/BizMail/BmSubscribers/BmSubscribersIndex.cshtml");
        }
    }
}