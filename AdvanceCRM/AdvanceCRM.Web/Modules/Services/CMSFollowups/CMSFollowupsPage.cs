
namespace AdvanceCRM.Services.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Services/CMSFollowups")]
    [PageAuthorize(typeof(CMSFollowupsRow))]
    public class CMSFollowupsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Services/CMSFollowups/CMSFollowupsIndex.cshtml");
        }
    }
}