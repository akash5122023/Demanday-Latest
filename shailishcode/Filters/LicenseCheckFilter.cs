using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using AdvanceCRM.Web.Helpers;

namespace AdvanceCRM.Web.Filters
{
    public class LicenseCheckFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var path = context.HttpContext.Request.Path;
            if (!path.StartsWithSegments("/Activation") &&
                (path.StartsWithSegments("/Account") || path.StartsWithSegments("/Home")))
            {
                if (!LicenseHelper.Activated)
                {
                    context.Result = new RedirectResult("/Activation");
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
