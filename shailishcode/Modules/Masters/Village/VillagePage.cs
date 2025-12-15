
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/Village")]
    [PageAuthorize(typeof(VillageRow))]
    public class VillageController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/Village/VillageIndex.cshtml");
        }
    }
}