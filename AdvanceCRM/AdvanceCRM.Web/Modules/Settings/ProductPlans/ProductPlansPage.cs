namespace AdvanceCRM.Settings.Pages
{
    using AdvanceCRM.Settings;
    using Microsoft.AspNetCore.Mvc;
    using Serenity.Web;

    [Route("Settings/ProductPlans")]
    [PageAuthorize(typeof(ProductPlanRow))]
    public class ProductPlansController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/ProductPlans/ProductPlansIndex.cshtml");
        }
    }
}
