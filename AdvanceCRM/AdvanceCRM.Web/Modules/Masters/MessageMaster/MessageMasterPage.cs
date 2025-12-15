
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/MessageMaster")]
    [PageAuthorize(typeof(MessageMasterRow))]
    public class MessageMasterController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/MessageMaster/MessageMasterIndex.cshtml");
        }
    }
}