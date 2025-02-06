using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Titanium2.Application.DTOs;
using Titanium2.Application.Services;

namespace Titanium2.Api.Controllers.CartItem
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        CartItemServices _services;

        public CartItemsController(CartItemServices services)
        {
            _services = services;
        }
        [HttpGet("GetAllCartsItems")]
        public async Task<IActionResult> GetAllCartsItems()
        {
            try
            {
                var data = await _services.GetAllCarts();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
        [HttpPost("AddCartItem")]
        public async Task<IActionResult> AddCartItem(CartItemDTO cartItemDTO)
        {
            try
            {
                var added = await _services.AddCart(cartItemDTO);
                if(added)
                    return Ok("Added successfully.");
                return BadRequest("Can't add cartitem");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
        [HttpPut("UpdateCartItem")]
        public async Task<IActionResult> UpdateCartItem(CartItemDTO cartItemDTO)
        {
            try
            {
                var updated = await _services.UpdateCart(cartItemDTO.CartItemGuid, cartItemDTO.ProductId, cartItemDTO.Quantity);
                if (updated)
                    return Ok("Updated successfully");
                return BadRequest("Can't update cartitem");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
        [HttpDelete("RemoveCartItem")]
        public async Task<IActionResult> RemoveCartItem(Guid guid)
        {
            try
            {
                var deleted = await _services.RemoveCart(guid);
                if (deleted)
                    return Ok("Removed successfully.");
                return BadRequest("Can't remove cartitem");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
    }
}
