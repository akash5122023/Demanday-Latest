using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.EBBCheck.Pages
{
    [PageAuthorize(typeof(EBBCheckRow))]
    public class EBBCheckController : Controller
    {
        [Route("EBBCheck/EBBCheck")]
        public ActionResult Index()
        {
            return View("~/Modules/EBBCheck/EBBCheck/EBBCheckIndex.cshtml");
        }
    }
}
