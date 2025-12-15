
namespace AdvanceCRM.Template.Pages
{
    using Serenity;
    using Serenity.Web;

    using Microsoft.AspNetCore.Mvc;
    [Route("Template/OtherTemplates")]
    [PageAuthorize(typeof(OtherTemplatesRow))]
    public class OtherTemplatesController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Template/OtherTemplates/OtherTemplatesIndex.cshtml");
        }
    }
}