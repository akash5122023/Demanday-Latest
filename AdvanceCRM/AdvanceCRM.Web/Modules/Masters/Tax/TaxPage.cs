
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/Tax")]
    [PageAuthorize(typeof(TaxRow))]
    public class TaxController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/Tax/TaxIndex.cshtml");
        }
    }
}