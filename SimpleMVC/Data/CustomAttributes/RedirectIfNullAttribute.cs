using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace SimpleMVC.Data.CustomAttributes
{
    public class RedirectIfNullAttribute : ActionFilterAttribute
    {
        private readonly string _redirectUrl;
        public RedirectIfNullAttribute(string redirectUrl)
        {
            _redirectUrl = redirectUrl;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context,
                                                            ActionExecutionDelegate next)
        {
            if (context.ActionArguments.TryGetValue("id", out var idValue) && idValue is int id)
            {
                var getByIdAsyncMethodInfo = context.Controller.GetType().GetMethod("GetByIdAsync");
                if (getByIdAsyncMethodInfo != null)
                {
                    var getByIdAsync = (Func<int, Task<object?>>)Delegate.CreateDelegate(typeof(Func<int, Task<object?>>), context.Controller, getByIdAsyncMethodInfo);
                    var entity = await getByIdAsync(id);
                    if (entity == null)
                    {
                        context.Result = new RedirectResult(_redirectUrl);
                        return;
                    }
                }
            }

            await next();
        }
    }
}
