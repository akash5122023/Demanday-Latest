namespace AdvanceCRM.Settings.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Serenity.Web;

    [Route("Settings/ModulePricing")]
    [PageAuthorize(typeof(ProductModuleRow))]
    public class ModulePricingController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/ProductModules/ModulePricingIndex.cshtml");
        }
    }
}
