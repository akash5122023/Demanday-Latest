using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Toolkit.Pages
{

    [PageAuthorize(typeof(MasterSupressionRow))]
    public class MasterSupressionController : Controller
    {
        [Route("Toolkit/MasterSupression")]
        public ActionResult Index()
        {
            return View("~/Modules/Toolkit/MasterSupression/MasterSupressionIndex.cshtml");
        }
    }
}