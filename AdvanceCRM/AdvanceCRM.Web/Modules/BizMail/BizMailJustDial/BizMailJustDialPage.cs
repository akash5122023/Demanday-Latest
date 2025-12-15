
namespace AdvanceCRM.BizMail.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("BizMail/BizMailJustDial")]
    [PageAuthorize(typeof(BizMailJustDialRow))]
    public class BizMailJustDialController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/BizMail/BizMailJustDial/BizMailJustDialIndex.cshtml");
        }
    }
}