namespace AdvanceCRM.Settings.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Serenity.Web;

    [Route("Settings/DefaultFeatures")]
    [PageAuthorize(typeof(DefaultFeatureRow))]
    public class DefaultFeaturesPage : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/DefaultFeatures/DefaultFeaturesIndex.cshtml");
        }
    }
}
