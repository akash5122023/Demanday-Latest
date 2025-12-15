using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Products.Pages
{

    [PageAuthorize(typeof(InventoryRow))]
    public class InventoryController : Controller
    {
        [Route("Products/Inventory")]
        public ActionResult Index()
        {
            return View("~/Modules/Products/Inventory/InventoryIndex.cshtml");
        }
    }
}