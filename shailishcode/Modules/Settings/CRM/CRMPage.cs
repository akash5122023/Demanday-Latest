

namespace AdvanceCRM.Settings.Pages
{
    using AdvanceCRM.Administration;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Settings/CRM")]
    [PageAuthorize(typeof(CompanyDetailsRow))]
    public class CRMController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Settings/CRM/CRMIndex.cshtml");
        }
    }
}