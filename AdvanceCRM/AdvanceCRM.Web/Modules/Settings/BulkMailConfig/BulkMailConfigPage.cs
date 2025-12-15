
namespace AdvanceCRM.Settings.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Settings/BulkMailConfig")]
    [PageAuthorize(typeof(BulkMailConfigRow))]
    public class BulkMailConfigController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/BulkMailConfig/BulkMailConfigIndex.cshtml");
        }
    }
}