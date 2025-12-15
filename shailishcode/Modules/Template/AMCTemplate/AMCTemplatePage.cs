
namespace AdvanceCRM.Template.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Template/AMCTemplate")]
    [PageAuthorize(typeof(AMCTemplateRow))]
    public class AMCTemplateController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Template/AMCTemplate/AMCTemplateIndex.cshtml");
        }
    }
}