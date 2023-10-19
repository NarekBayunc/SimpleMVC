using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SimpleMVC.Data.CustomAttributes
{
    public class AllowAnonymousOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity!.IsAuthenticated)
            {
                context.Result = new ForbidResult(); 
            }

            base.OnActionExecuting(context);
        }
    }
}
