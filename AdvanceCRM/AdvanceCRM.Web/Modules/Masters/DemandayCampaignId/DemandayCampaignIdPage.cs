using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Masters.Pages
{

    [PageAuthorize(typeof(DemandayCampaignIdRow))]
    public class DemandayCampaignIdController : Controller
    {
        [Route("Masters/DemandayCampaignId")]
        public ActionResult Index()
        {
            return View("~/Modules/Masters/DemandayCampaignId/DemandayCampaignIdIndex.cshtml");
        }
    }
}