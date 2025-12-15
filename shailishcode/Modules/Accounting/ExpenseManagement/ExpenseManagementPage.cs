
namespace AdvanceCRM.Accounting.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Serenity;
    using Serenity.Web;
    

    [Route("Accounting/ExpenseManagement")]
    [PageAuthorize(typeof(ExpenseManagementRow))]
    public class ExpenseManagementController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Accounting/ExpenseManagement/ExpenseManagementIndex.cshtml");
        }
    }
}