
namespace AdvanceCRM.Accounting.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Serenity.Web;


    [Route("Accounting/Cashbook")]
    [PageAuthorize(typeof(CashbookRow))]
    public class CashbookController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Accounting/Cashbook/CashbookIndex.cshtml");
        }
    }
}