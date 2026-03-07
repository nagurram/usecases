using web.app.Models;

namespace web.app.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersByCustomerAsync(string customerId);
        Task<Order?> GetOrderByIdAsync(int id);
        Task<Order> CreateOrderAsync(Order order);
        Task<Order> UpdateOrderStatusAsync(int id, OrderStatus status);
        Task<Order> CancelOrderAsync(int id);
    }
}
