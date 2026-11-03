using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Masters.Pages
{

    [PageAuthorize(typeof(ContactAccountTypeMasterRow))]
    public class ContactAccountTypeMasterController : Controller
    {
        [Route("Masters/ContactAccountTypeMaster")]
        public ActionResult Index()
        {
            return View("~/Modules/Masters/ContactAccountTypeMaster/ContactAccountTypeMasterIndex.cshtml");
        }
    }
}