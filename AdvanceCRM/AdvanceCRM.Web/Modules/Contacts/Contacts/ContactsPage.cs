
namespace AdvanceCRM.Contacts.Pages
{
    using Serenity;
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;

    [Route("Contacts")]
    [PageAuthorize(typeof(ContactsRow))]
    public class ContactsController : Controller
    {
        public ActionResult Index()
        {
            return View(MVC.Views.Contacts.Contacts_.ContactsIndex);
        }
    }
}