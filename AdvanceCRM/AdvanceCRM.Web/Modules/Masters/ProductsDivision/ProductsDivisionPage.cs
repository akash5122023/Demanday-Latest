
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/ProductsDivision")]
    [PageAuthorize(typeof(ProductsDivisionRow))]
    public class ProductsDivisionController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/ProductsDivision/ProductsDivisionIndex.cshtml");
        }
    }
}