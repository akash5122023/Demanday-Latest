
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;

    using Microsoft.AspNetCore.Mvc;
    [Route("Masters/Days")]
    [PageAuthorize(typeof(DaysRow))]
    public class DaysController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/Days/DaysIndex.cshtml");
        }
    }
}