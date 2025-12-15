using AdvanceCRM.Administration.Entities;
using Microsoft.AspNetCore.Mvc;
using Serenity.Web;

namespace AdvanceCRM.Administration.Pages
{
    [PageAuthorize(typeof(RoleRow))]
    public class RoleController : Controller
    {
        [Route("Administration/Role")]
        public ActionResult Index()
        {
            return View(MVC.Views.Administration.Role.RoleIndex);
        }
    }
}