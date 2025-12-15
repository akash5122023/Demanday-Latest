
namespace AdvanceCRM.ThirdParty.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("ThirdParty/Visit")]
    [PageAuthorize(typeof(VisitRow))]
    public class VisitController : Controller
    {
        public ActionResult Index()
        {            
            return View("~/Modules/ThirdParty/Visit/VisitIndex.cshtml");
        }

        
    }
}