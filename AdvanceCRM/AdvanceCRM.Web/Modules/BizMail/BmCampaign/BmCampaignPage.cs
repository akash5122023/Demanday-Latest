
namespace AdvanceCRM.BizMail.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("BizMail/BmCampaign")]
    [PageAuthorize(typeof(BmCampaignRow))]
    public class BmCampaignController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/BizMail/BmCampaign/BmCampaignIndex.cshtml");
        }
    }
}