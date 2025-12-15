
namespace AdvanceCRM.Purchase.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Rorder")]
    [PageAuthorize(typeof(RorderRow))]
    public class RorderController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Purchase/Rorder/RorderIndex.cshtml");
        }
    }
}