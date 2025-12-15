
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/Source")]
    [PageAuthorize(typeof(SourceRow))]
    public class SourceController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/Source/SourceIndex.cshtml");
        }
    }
}