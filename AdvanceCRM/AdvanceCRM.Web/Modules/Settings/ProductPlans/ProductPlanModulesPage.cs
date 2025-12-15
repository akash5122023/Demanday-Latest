namespace AdvanceCRM.Settings.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Serenity.Web;

    [Route("Settings/ProductPlanModules")]
    [PageAuthorize(typeof(ProductPlanModuleRow))]
    public class ProductPlanModulesController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/ProductPlans/ProductPlanModulesIndex.cshtml");
        }
    }
}
