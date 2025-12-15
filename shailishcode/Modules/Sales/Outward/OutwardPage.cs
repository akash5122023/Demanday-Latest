using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Sales.Pages
{

    [PageAuthorize(typeof(OutwardRow))]
    public class OutwardController : Controller
    {
        [Route("Sales/Outward")]
        public ActionResult Index()
        {
            return View("~/Modules/Sales/Outward/OutwardIndex.cshtml");
        }
    }
}