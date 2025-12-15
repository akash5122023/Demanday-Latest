
namespace AdvanceCRM.BizMail.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("BizMail/BizMailTradeIdia")]
    [PageAuthorize(typeof(BizMailTradeIdiaRow))]
    public class BizMailTradeIdiaController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/BizMail/BizMailTradeIdia/BizMailTradeIdiaIndex.cshtml");
        }
    }
}