
namespace AdvanceCRM.BizMail.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("BizMail/CampaignBm")]
    [PageAuthorize(typeof(CampaignBmRow))]
    public class CampaignBmController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/BizMail/CampaignBm/CampaignBmIndex.cshtml");
        }
    }
}