
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/AdditionalConcession")]
    [PageAuthorize(typeof(AdditionalConcessionRow))]
    public class AdditionalConcessionController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/AdditionalConcession/AdditionalConcessionIndex.cshtml");
        }
    }
}