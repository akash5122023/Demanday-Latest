
namespace AdvanceCRM.Template.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Template/PurchaseTemplate")]
    [PageAuthorize(typeof(PurchaseOrderTemplateRow))]
    public class PurchaseOrderTemplateController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Template/PurchaseOrderTemplate/PurchaseOrderTemplateIndex.cshtml");
        }
    }
}