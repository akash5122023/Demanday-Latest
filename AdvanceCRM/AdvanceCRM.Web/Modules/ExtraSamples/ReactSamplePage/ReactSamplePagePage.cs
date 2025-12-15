namespace AdvanceCRM.ExtraSamples.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Serenity.Web;

    [PageAuthorize]
    [Route("ExtraSamples/ReactSample")]
    public class ReactSamplePageController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/ExtraSamples/ReactSamplePage/ReactSamplePageIndex.cshtml");
        }
    }
}
