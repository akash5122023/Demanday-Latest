
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;

    using Microsoft.AspNetCore.Mvc;
    [Route("Masters/Grade")]
    [PageAuthorize(typeof(GradeRow))]
    public class GradeController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/Grade/GradeIndex.cshtml");
        }
    }
}