
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/AdditionalInfo")]
    [PageAuthorize(typeof(AdditionalInfoRow))]
    public class AdditionalInfoController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/AdditionalInfo/AdditionalInfoIndex.cshtml");
        }
    }
}