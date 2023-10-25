using Microsoft.AspNetCore.Mvc;
using SimpleMVC.Models;

namespace SimpleMVC.Data.Extensions
{
    public static class ControllerExtensions
    {
        public static RedirectResult RedirectIfNull<T>(this Controller controller,
                                                    T model, string redirectUrl)
        {
            if (model == null)
            {
                return new RedirectResult(redirectUrl);
            }
            else
            {
                return null!;
            }
        }
        public static async Task<RedirectResult> RedirectIfNullAsync<T>(this Controller controller,
                                                      int id, Func<int, Task<T?>> getByIdAsync,
                                                      string redirectUrl)
        {
            T? entity = await getByIdAsync(id);

            if (entity == null)
            {
                return new RedirectResult(redirectUrl);
            }
            else
            {
                return null!;
            }
        }
    }
}
