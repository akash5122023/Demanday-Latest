
namespace AdvanceCRM.Template.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Template/ChallanTemplate")]
    [PageAuthorize(typeof(ChallanTemplateRow))]
    public class ChallanTemplateController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Template/ChallanTemplate/ChallanTemplateIndex.cshtml");
        }
    }
}