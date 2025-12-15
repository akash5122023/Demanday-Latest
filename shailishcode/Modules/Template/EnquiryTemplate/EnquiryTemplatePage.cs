
namespace AdvanceCRM.Template.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Template/EnquiryTemplate")]
    [PageAuthorize(typeof(EnquiryTemplateRow))]
    public class EnquiryTemplateController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Template/EnquiryTemplate/EnquiryTemplateIndex.cshtml");
        }
    }
}