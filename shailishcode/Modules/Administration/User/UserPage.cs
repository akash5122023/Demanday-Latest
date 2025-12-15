
namespace AdvanceCRM.Administration.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Serenity.Web;
    

    [Route("Administration/User")]
    [PageAuthorize(typeof(UserRow))]
    public class UserController : Controller
    {
        public ActionResult Index()
        {

            return View(MVC.Views.Administration.User.UserIndex);
        }
    }
}