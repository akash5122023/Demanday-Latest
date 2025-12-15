using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using AdvanceCRM.Web.Helpers;

namespace AdvanceCRM.Web.Filters
{
    public class RequireActivationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!LicenseHelper.Activated &&
                !context.HttpContext.Request.Path.StartsWithSegments("/Activation"))
            {
                context.Result = new RedirectResult("/Activation");
            }
        }
    }
}
