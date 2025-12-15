namespace AdvanceCRM.Settings.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Serenity.Web;

    [Route("Settings/ProductModules")]
    [PageAuthorize(typeof(ProductModuleRow))]
    public class ProductModulesController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/ProductModules/ProductModuleIndex.cshtml");
        }
    }
}
