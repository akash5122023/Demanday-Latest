
namespace AdvanceCRM.Template.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Template/AppointmentTemplate")]
    [PageAuthorize(typeof(AppointmentTemplateRow))]
    public class AppointmentTemplateController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Template/AppointmentTemplate/AppointmentTemplateIndex.cshtml");
        }
    }
}