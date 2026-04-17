using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Masters.Pages
{

    [PageAuthorize(typeof(DemandayTeleMarketingEnquiryQuestionAnswersRow))]
    public class DemandayTeleMarketingEnquiryQuestionAnswersController : Controller
    {
        [Route("Masters/DemandayTeleMarketingEnquiryQuestionAnswers")]
        public ActionResult Index()
        {
            return View("~/Modules/Masters/DemandayTeleMarketingEnquiryQuestionAnswers/DemandayTeleMarketingEnquiryQuestionAnswersIndex.cshtml");
        }
    }
}