using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Purchase.Pages
{
    [Route("GrnTwo")]
    [PageAuthorize(typeof(GrnTwoRow))]
    public class GrnTwoController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Purchase/GrnTwo/GrnTwoIndex.cshtml");
        }
    }
}