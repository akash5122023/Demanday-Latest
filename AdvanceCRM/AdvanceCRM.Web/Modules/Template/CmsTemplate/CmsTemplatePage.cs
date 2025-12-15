
namespace AdvanceCRM.Template.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Template/CmsTemplate")]
    [PageAuthorize(typeof(CmsTemplateRow))]
    public class CmsTemplateController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Template/CmsTemplate/CmsTemplateIndex.cshtml");
        }
    }
}