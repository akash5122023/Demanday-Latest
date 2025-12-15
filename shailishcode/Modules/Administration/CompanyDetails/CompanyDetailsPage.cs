
namespace AdvanceCRM.Administration.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Administration/CompanyDetails")]
    [PageAuthorize(typeof(CompanyDetailsRow))]
    public class CompanyDetailsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Administration/CompanyDetails/CompanyDetailsIndex.cshtml");
        }
    }
}