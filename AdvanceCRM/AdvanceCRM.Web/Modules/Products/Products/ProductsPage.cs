
namespace AdvanceCRM.Products.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Products")]
    [PageAuthorize(typeof(ProductsRow))]
    public class ProductsController : Controller
    {
        public ActionResult Index()
        {
            return View(MVC.Views.Products.Products_.ProductsIndex);
        }
    }
}