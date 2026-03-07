using Microsoft.AspNetCore.Mvc;
using web.app.Models;
using web.app.Services;

namespace web.app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<Cart>> GetCart(string customerId)
        {
            var cart = await _cartService.GetCartAsync(customerId);
            return Ok(cart);
        }

        [HttpPost("{customerId}/items")]
        public async Task<ActionResult<Cart>> AddToCart(string customerId, [FromBody] CartItem item)
        {
            if (item.ProductId <= 0 || item.Quantity <= 0)
                return BadRequest(new { message = "Invalid product ID or quantity" });

            var cart = await _cartService.AddToCartAsync(customerId, item);
            return Ok(cart);
        }

        [HttpDelete("{customerId}/items/{productId}")]
        public async Task<ActionResult<Cart>> RemoveFromCart(string customerId, int productId)
        {
            try
            {
                var cart = await _cartService.RemoveFromCartAsync(customerId, productId);
                return Ok(cart);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("{customerId}/items/{productId}")]
        public async Task<ActionResult<Cart>> UpdateCartItem(string customerId, int productId, [FromQuery] int quantity)
        {
            try
            {
                var cart = await _cartService.UpdateCartItemAsync(customerId, productId, quantity);
                return Ok(cart);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> ClearCart(string customerId)
        {
            await _cartService.ClearCartAsync(customerId);
            return NoContent();
        }
    }
}
