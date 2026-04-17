using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Toolkit.Pages
{

    [PageAuthorize(typeof(ClientSupressionRow))]
    public class ClientSupressionController : Controller
    {
        [Route("Toolkit/ClientSupression")]
        public ActionResult Index()
        {
            return View("~/Modules/Toolkit/ClientSupression/ClientSupressionIndex.cshtml");
        }
    }
}