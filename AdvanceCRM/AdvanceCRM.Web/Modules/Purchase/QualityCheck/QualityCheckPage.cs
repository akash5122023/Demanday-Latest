using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;
using AdvanceCRM.Purchase;

namespace AdvanceCRM.Purchase.Pages
{
    [Route("QualityCheck")]
    [PageAuthorize(typeof(QualityCheckRow))]
    public class QualityCheckController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Purchase/QualityCheck/QualityCheckIndex.cshtml");
        }
    }
}