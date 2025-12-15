
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/AccountingHeads")]
    [PageAuthorize(typeof(AccountingHeadsRow))]
    public class AccountingHeadsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/AccountingHeads/AccountingHeadsIndex.cshtml");
        }
    }
}