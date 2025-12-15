
namespace AdvanceCRM.Template.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Template/InvoiceTemplate")]
    [PageAuthorize(typeof(InvoiceTemplateRow))]
    public class InvoiceTemplateController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Template/InvoiceTemplate/InvoiceTemplateIndex.cshtml");
        }
    }
}