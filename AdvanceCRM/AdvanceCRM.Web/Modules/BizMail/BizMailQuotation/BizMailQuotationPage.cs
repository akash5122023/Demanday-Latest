
namespace AdvanceCRM.BizMail.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("BizMail/BizMailQuotation")]
    [PageAuthorize(typeof(BizMailQuotationRow))]
    public class BizMailQuotationController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/BizMail/BizMailQuotation/BizMailQuotationIndex.cshtml");
        }
    }
}