
namespace AdvanceCRM.Template.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Template/TeleCallingTemplate")]
    [PageAuthorize(typeof(TeleCallingTemplateRow))]
    public class TeleCallingTemplateController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Template/TeleCallingTemplate/TeleCallingTemplateIndex.cshtml");
        }
    }
}