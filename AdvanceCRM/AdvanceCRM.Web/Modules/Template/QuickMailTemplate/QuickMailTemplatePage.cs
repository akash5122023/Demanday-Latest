
namespace AdvanceCRM.Template.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Template/QuickMailTemplate")]
    [PageAuthorize(typeof(QuickMailTemplateRow))]
    public class QuickMailTemplateController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Template/QuickMailTemplate/QuickMailTemplateIndex.cshtml");
        }
    }
}