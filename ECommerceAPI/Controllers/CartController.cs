using AppLibrary.DTOs.Cart;
using AppLibrary.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return NotFound("User not found!");
            }

            var res = await _cartService.GetCartAsync(userId);
            if (res == null)
            {
                return NotFound("Cart not found!");
            }
            return Ok(res);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto dto)
        {
            if (dto == null || dto.Quantity <= 0)
            {
                return BadRequest("Invalid data!");
            }
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    return Unauthorized("User not found!");
                }
                await _cartService.AddToCartAsync(userId, dto.ProductId, dto.Quantity);
                return Ok(dto);
            }
            catch
            {
                return BadRequest("Error while adding products to cart!");
            }
        }

        [HttpDelete("productId")]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _cartService.GetCartAsync(userId);

            try
            {
                await _cartService.RemoveFromCartAsync(userId, productId);
                var updatedCart = await _cartService.GetCartAsync(userId);
                return Ok(updatedCart);
            }
            catch
            {
                return BadRequest("Error while removing product from cart");
            }
        }
        [HttpDelete("Clear")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _cartService.GetCartAsync(userId);

            try
            {
                await _cartService.ClearCartAsync(userId);
                var updatedCart = await _cartService.GetCartAsync(userId);
                return Ok(updatedCart);
            }
            catch
            {
                return BadRequest("Error while clearing the cart");
            }
        }
    }
}