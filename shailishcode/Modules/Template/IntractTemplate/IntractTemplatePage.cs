
namespace AdvanceCRM.Template.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Template/IntractTemplate")]
    [PageAuthorize(typeof(IntractTemplateRow))]
    public class IntractTemplateController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Template/IntractTemplate/IntractTemplateIndex.cshtml");
        }
    }
}