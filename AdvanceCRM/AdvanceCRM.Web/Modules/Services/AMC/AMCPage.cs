
namespace AdvanceCRM.Services.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Services/AMC")]
    [PageAuthorize(typeof(AMCRow))]
    public class AMCController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Services/AMC/AMCIndex.cshtml");
        }
    }
}