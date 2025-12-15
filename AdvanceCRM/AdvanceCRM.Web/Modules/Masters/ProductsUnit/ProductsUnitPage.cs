
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/ProductsUnit")]
    [PageAuthorize(typeof(ProductsUnitRow))]
    public class ProductsUnitController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/ProductsUnit/ProductsUnitIndex.cshtml");
        }
    }
}