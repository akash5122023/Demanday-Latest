
namespace AdvanceCRM.Feedback.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Feedback/FeedbackDetails")]
    [PageAuthorize(typeof(FeedbackDetailsRow))]
    public class FeedbackDetailsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Feedback/FeedbackDetails/FeedbackDetailsIndex.cshtml");
        }
    }
}