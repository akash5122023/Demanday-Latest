
namespace AdvanceCRM.Services.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Services/CMS")]
    [PageAuthorize(typeof(CMSRow))]
    public class CMSController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Services/CMS/CMSIndex.cshtml");
        }
    }
}