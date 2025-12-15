using Microsoft.AspNetCore.Mvc.Filters;

public class AllowCrossSiteAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        context.HttpContext.Response.Headers.Remove("X-Frame-Options");
        context.HttpContext.Response.Headers["X-Frame-Options"] = "AllowAll";

        base.OnActionExecuting(context);
    }
}