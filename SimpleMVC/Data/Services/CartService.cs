using SimpleMVC.Models;

namespace SimpleMVC.Data.Services
{
    public class CartService
    {
        private readonly ApplicationContext context;
        public CartService(ApplicationContext context)
        {

            this.context = context;
        }

        public async Task<bool> RemoveCartItemAsync(CartItem? cartItem)
        {
            if (cartItem == null)
            {
                return false;
            }
            else
            {
                context.CartItems.Remove(cartItem);
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
