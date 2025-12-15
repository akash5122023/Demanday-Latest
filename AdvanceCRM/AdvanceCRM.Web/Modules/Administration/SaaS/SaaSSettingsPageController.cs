using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serenity.Data;
using System.Threading.Tasks;
using System;

namespace AdvanceCRM.Administration
{
    [Authorize]
    [Route("Administration/SaaSSettings")]
    public class SaaSSettingsPageController : Controller
    {
        [HttpGet, Route("")]
        public IActionResult Index()
        {
            return View("~/Modules/Administration/SaaS/SaaSSettingsIndex.cshtml");
        }
    }
}
