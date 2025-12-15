using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Administration.Pages
{

    [PageAuthorize(typeof(TabGenderRow))]
    public class TabGenderController : Controller
    {
        [Route("Administration/TabGender")]
        public ActionResult Index()
        {
            return View("~/Modules/Administration/TabGender/TabGenderIndex.cshtml");
        }
    }
}