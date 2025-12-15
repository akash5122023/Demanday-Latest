using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;
using AdvanceCRM.Products;

namespace AdvanceCRM.Products.Pages
{
    [Route("Bom")]
    [PageAuthorize(typeof(BomRow))]
    public class BomController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Products/Bom/BomIndex.cshtml");
        }
    }
}