using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Sales.Pages
{

    [PageAuthorize(typeof(InwardRow))]
    public class InwardController : Controller
    {
        [Route("Sales/Inward")]
        public ActionResult Index()
        {
            return View("~/Modules/Sales/Inward/InwardIndex.cshtml");
        }
    }
}