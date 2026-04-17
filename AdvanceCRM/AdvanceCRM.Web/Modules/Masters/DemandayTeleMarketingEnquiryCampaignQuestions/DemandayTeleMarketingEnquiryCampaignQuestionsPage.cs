using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Masters.Pages
{

    [PageAuthorize(typeof(DemandayTeleMarketingEnquiryCampaignQuestionsRow))]
    public class DemandayTeleMarketingEnquiryCampaignQuestionsController : Controller
    {
        [Route("Masters/DemandayTeleMarketingEnquiryCampaignQuestions")]
        public ActionResult Index()
        {
            return View("~/Modules/Masters/DemandayTeleMarketingEnquiryCampaignQuestions/DemandayTeleMarketingEnquiryCampaignQuestionsIndex.cshtml");
        }
    }
}