
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/City")]
    [PageAuthorize(typeof(CityRow))]
    public class CityController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/City/CityIndex.cshtml");
        }
    }
}