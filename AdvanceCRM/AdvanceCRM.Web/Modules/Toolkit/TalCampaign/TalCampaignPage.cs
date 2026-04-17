using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Toolkit.Pages
{

    [PageAuthorize(typeof(TalCampaignRow))]
    public class TalCampaignController : Controller
    {
        [Route("Toolkit/TalCampaign")]
        public ActionResult Index()
        {
            return View("~/Modules/Toolkit/TalCampaign/TalCampaignIndex.cshtml");
        }
    }
}