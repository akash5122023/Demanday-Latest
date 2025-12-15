
namespace AdvanceCRM.BizMail.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("BizMail/BizMailFacebook")]
    [PageAuthorize(typeof(BizMailFacebookRow))]
    public class BizMailFacebookController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/BizMail/BizMailFacebook/BizMailFacebookIndex.cshtml");
        }
    }
}