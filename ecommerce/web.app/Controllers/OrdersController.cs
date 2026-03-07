using Microsoft.AspNetCore.Mvc;
using web.app.Models;
using web.app.Services;

namespace web.app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<List<Order>>> GetOrdersByCustomer(string customerId)
        {
            var orders = await _orderService.GetOrdersByCustomerAsync(customerId);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound(new { message = $"Order with ID {id} not found" });

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Order order)
        {
            if (string.IsNullOrEmpty(order.CustomerId))
                return BadRequest(new { message = "Customer ID is required" });

            if (order.Items.Count == 0)
                return BadRequest(new { message = "Order must contain at least one item" });

            var createdOrder = await _orderService.CreateOrderAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult<Order>> UpdateOrderStatus(int id, [FromBody] OrderStatusUpdate statusUpdate)
        {
            try
            {
                var order = await _orderService.UpdateOrderStatusAsync(id, statusUpdate.Status);
                return Ok(order);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Order with ID {id} not found" });
            }
        }

        [HttpPut("{id}/cancel")]
        public async Task<ActionResult<Order>> CancelOrder(int id)
        {
            try
            {
                var order = await _orderService.CancelOrderAsync(id);
                return Ok(order);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Order with ID {id} not found" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

    public class OrderStatusUpdate
    {
        public OrderStatus Status { get; set; }
    }
}
