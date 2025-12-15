
namespace AdvanceCRM.DMS.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("DMS")]
    [PageAuthorize(typeof(DMSRow))]
    public class DMSController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/DMS/DMSIndex.cshtml");
        }
    }
}