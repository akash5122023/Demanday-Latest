
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/BankMaster")]
    [PageAuthorize(typeof(BankMasterRow))]
    public class BankMasterController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/BankMaster/BankMasterIndex.cshtml");
        }
    }
}