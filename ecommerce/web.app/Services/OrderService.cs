using web.app.Models;

namespace web.app.Services
{
    public class OrderService : IOrderService
    {
        // In-memory storage for demo purposes. Replace with database in production.
        private static List<Order> orders = new();

        public Task<List<Order>> GetOrdersByCustomerAsync(string customerId)
        {
            return Task.FromResult(orders.Where(o => o.CustomerId == customerId).ToList());
        }

        public Task<Order?> GetOrderByIdAsync(int id)
        {
            return Task.FromResult(orders.FirstOrDefault(o => o.Id == id));
        }

        public Task<Order> CreateOrderAsync(Order order)
        {
            order.Id = orders.Count > 0 ? orders.Max(o => o.Id) + 1 : 1;
            order.OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMddHHmmss}";
            order.OrderDate = DateTime.UtcNow;
            order.Status = OrderStatus.Pending;
            orders.Add(order);
            return Task.FromResult(order);
        }

        public Task<Order> UpdateOrderStatusAsync(int id, OrderStatus status)
        {
            var order = orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                throw new KeyNotFoundException($"Order with ID {id} not found");

            order.Status = status;
            return Task.FromResult(order);
        }

        public Task<Order> CancelOrderAsync(int id)
        {
            var order = orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                throw new KeyNotFoundException($"Order with ID {id} not found");

            if (order.Status == OrderStatus.Shipped || order.Status == OrderStatus.Delivered)
                throw new InvalidOperationException("Cannot cancel a shipped or delivered order");

            order.Status = OrderStatus.Cancelled;
            return Task.FromResult(order);
        }
    }
}
