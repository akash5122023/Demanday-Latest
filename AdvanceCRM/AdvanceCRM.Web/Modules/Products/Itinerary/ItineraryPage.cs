
namespace AdvanceCRM.Products.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Itinerary")]
    [PageAuthorize(typeof(ItineraryRow))]
    public class ItineraryController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Products/Itinerary/ItineraryIndex.cshtml");
        }
    }
}