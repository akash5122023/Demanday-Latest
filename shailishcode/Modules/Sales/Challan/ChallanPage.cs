
namespace AdvanceCRM.Sales.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Challan")]
    [PageAuthorize(typeof(ChallanRow))]
    public class ChallanController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Sales/Challan/ChallanIndex.cshtml");
        }
    }
}