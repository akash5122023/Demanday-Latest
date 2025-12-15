
namespace AdvanceCRM.Common.Pages
{
    using Serenity;
    using Serenity.Web;

    using Microsoft.AspNetCore.Mvc;
    [Route("Common/Notifications")]
    [PageAuthorize(typeof(NotificationUsersRow))]
    public class NotificationUsersController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Common/NotificationUsers/NotificationUsersIndex.cshtml");
        }
    }
}