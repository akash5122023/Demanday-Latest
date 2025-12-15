
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/Tehsil")]
    [PageAuthorize(typeof(TehsilRow))]
    public class TehsilController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/Tehsil/TehsilIndex.cshtml");
        }
    }
}