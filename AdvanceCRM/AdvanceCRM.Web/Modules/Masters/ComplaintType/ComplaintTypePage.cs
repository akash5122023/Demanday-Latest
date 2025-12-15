
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;

    using Microsoft.AspNetCore.Mvc;
    [Route("Masters/ComplaintType")]
    [PageAuthorize(typeof(ComplaintTypeRow))]
    public class ComplaintTypeController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/ComplaintType/ComplaintTypeIndex.cshtml");
        }
    }
}