
namespace AdvanceCRM.ThirdParty.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("ThirdParty/WatiContacts")]
    [PageAuthorize(typeof(WatiContactsRow))]
    public class WatiContactsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/ThirdParty/WatiContacts/WatiContactsIndex.cshtml");
        }
    }
}