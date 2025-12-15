
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/ProductsGroup")]
    [PageAuthorize(typeof(ProductsGroupRow))]
    public class ProductsGroupController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/ProductsGroup/ProductsGroupIndex.cshtml");
        }
    }
}