
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;

    using Microsoft.AspNetCore.Mvc;
    [Route("Masters/AdditionalCharges")]
    [PageAuthorize(typeof(AdditionalChargesRow))]
    public class AdditionalChargesController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/AdditionalCharges/AdditionalChargesIndex.cshtml");
        }
    }
}