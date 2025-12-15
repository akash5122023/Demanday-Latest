
namespace AdvanceCRM.Masters.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Masters/QuotationTermsMaster")]
    [PageAuthorize(typeof(QuotationTermsMasterRow))]
    public class QuotationTermsMasterController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Modules/Masters/QuotationTermsMaster/QuotationTermsMasterIndex.cshtml");
        }
    }
}