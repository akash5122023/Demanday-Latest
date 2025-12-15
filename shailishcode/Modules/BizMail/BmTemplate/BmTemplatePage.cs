
namespace AdvanceCRM.BizMail.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("BizMail/BmTemplate")]
    [PageAuthorize(typeof(BmTemplateRow))]
    public class BmTemplateController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/BizMail/BmTemplate/BmTemplateIndex.cshtml");
        }
    }
}