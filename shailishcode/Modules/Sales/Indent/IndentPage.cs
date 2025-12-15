using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Sales.Pages
{

    [PageAuthorize(typeof(IndentRow))]
    public class IndentController : Controller
    {
        [Route("Sales/Indent")]
        public ActionResult Index()
        {
            return View("~/Modules/Sales/Indent/IndentIndex.cshtml");
        }
    }
}