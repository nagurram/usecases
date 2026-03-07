using web.app.Models;

namespace web.app.Services
{
    public class CartService : ICartService
    {
        // In-memory storage for demo purposes. Replace with database/cache in production.
        private static Dictionary<string, Cart> carts = new();

        public Task<Cart> GetCartAsync(string customerId)
        {
            if (!carts.ContainsKey(customerId))
            {
                carts[customerId] = new Cart
                {
                    Id = Guid.NewGuid().ToString(),
                    CustomerId = customerId,
                    Items = new(),
                    LastModified = DateTime.UtcNow
                };
            }

            return Task.FromResult(carts[customerId]);
        }

        public Task<Cart> AddToCartAsync(string customerId, CartItem item)
        {
            var cart = carts.ContainsKey(customerId) 
                ? carts[customerId] 
                : new Cart { Id = Guid.NewGuid().ToString(), CustomerId = customerId };

            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                cart.Items.Add(item);
            }

            cart.LastModified = DateTime.UtcNow;
            carts[customerId] = cart;
            return Task.FromResult(cart);
        }

        public Task<Cart> RemoveFromCartAsync(string customerId, int productId)
        {
            var cart = carts.ContainsKey(customerId) 
                ? carts[customerId]
                : throw new KeyNotFoundException($"Cart not found for customer {customerId}");

            cart.Items.RemoveAll(i => i.ProductId == productId);
            cart.LastModified = DateTime.UtcNow;
            return Task.FromResult(cart);
        }

        public Task<Cart> UpdateCartItemAsync(string customerId, int productId, int quantity)
        {
            var cart = carts.ContainsKey(customerId)
                ? carts[customerId]
                : throw new KeyNotFoundException($"Cart not found for customer {customerId}");

            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item == null)
                throw new KeyNotFoundException($"Product {productId} not found in cart");

            if (quantity <= 0)
            {
                cart.Items.Remove(item);
            }
            else
            {
                item.Quantity = quantity;
            }

            cart.LastModified = DateTime.UtcNow;
            return Task.FromResult(cart);
        }

        public Task<bool> ClearCartAsync(string customerId)
        {
            if (carts.ContainsKey(customerId))
            {
                carts[customerId].Items.Clear();
                carts[customerId].LastModified = DateTime.UtcNow;
            }
            return Task.FromResult(true);
        }
    }
}
