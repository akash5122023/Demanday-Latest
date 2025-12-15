
namespace AdvanceCRM.Template.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Template/QuotationTemplate")]
    [PageAuthorize(typeof(QuotationTemplateRow))]
    public class QuotationTemplateController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Template/QuotationTemplate/QuotationTemplateIndex.cshtml");
        }
    }
}