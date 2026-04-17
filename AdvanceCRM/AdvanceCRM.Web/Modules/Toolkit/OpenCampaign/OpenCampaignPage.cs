using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Toolkit.Pages
{

    [PageAuthorize(typeof(OpenCampaignRow))]
    public class OpenCampaignController : Controller
    {
        [Route("Toolkit/OpenCampaign")]
        public ActionResult Index()
        {
            return View("~/Modules/Toolkit/OpenCampaign/OpenCampaignIndex.cshtml");
        }
    }
}