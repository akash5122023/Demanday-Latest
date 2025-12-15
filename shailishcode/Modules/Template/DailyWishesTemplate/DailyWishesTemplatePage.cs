
namespace AdvanceCRM.Template.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Template/DailyWishesTemplate")]
    [PageAuthorize(typeof(DailyWishesTemplateRow))]
    public class DailyWishesTemplateController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Template/DailyWishesTemplate/DailyWishesTemplateIndex.cshtml");
        }
    }
}