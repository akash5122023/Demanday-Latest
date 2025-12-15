
namespace AdvanceCRM.BizMail.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("BizMail/BizMailIdiaMart")]
    [PageAuthorize(typeof(BizMailIdiaMartRow))]
    public class BizMailIdiaMartController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/BizMail/BizMailIdiaMart/BizMailIdiaMartIndex.cshtml");
        }
    }
}