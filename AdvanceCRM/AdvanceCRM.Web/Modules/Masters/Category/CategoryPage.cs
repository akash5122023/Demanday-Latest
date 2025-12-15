
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/Category")]
    [PageAuthorize(typeof(CategoryRow))]
    public class CategoryController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/Category/CategoryIndex.cshtml");
        }
    }
}