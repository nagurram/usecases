using web.app.Models;

namespace web.app.Services
{
    public interface ICartService
    {
        Task<Cart> GetCartAsync(string customerId);
        Task<Cart> AddToCartAsync(string customerId, CartItem item);
        Task<Cart> RemoveFromCartAsync(string customerId, int productId);
        Task<Cart> UpdateCartItemAsync(string customerId, int productId, int quantity);
        Task<bool> ClearCartAsync(string customerId);
    }
}
