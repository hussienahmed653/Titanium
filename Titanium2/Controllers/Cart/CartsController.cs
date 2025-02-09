using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Titanium2.Application.DTOs;
using Titanium2.Application.Services;

namespace Titanium2.Api.Controllers.Cart
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        CartServices _cartServices;

        public CartsController(CartServices cartServices)
        {
            _cartServices = cartServices;
        }

        [HttpGet("GetAllCarts")]
        public async Task<IActionResult> GetAllCarts()
        {
            try
            {
                var data = await _cartServices.GetCarts();
                if(data.Count is 0)
                    return NotFound("No Data Here.");
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
        [HttpPost("AddIntoCart")]
        public async Task<IActionResult> AddIntoCart(CartDTO cartDTO)
        {
            try
            {
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if(userid is null)
                    return BadRequest("Please Login First");
                cartDTO.UserId = int.Parse(userid);
                var added = await _cartServices.AddCarts(cartDTO);
                if(!added)
                    return BadRequest("Can't add into cart!");
                return Ok("Added Successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveFromCart(Guid guid)
        {
            try
            {
                var removed = await _cartServices.RemoveCart(guid);
                if (!removed)
                    return BadRequest("Can't Remove!");
                return Ok("Removed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
    }
}
