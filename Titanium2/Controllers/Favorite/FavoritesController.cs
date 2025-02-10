using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Titanium2.Application.DTOs;
using Titanium2.Application.Services;

namespace Titanium2.Api.Controllers.Favorite
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        FavoriteServices _favoriteServices;

        public FavoritesController(FavoriteServices favoriteServices)
        {
            _favoriteServices = favoriteServices;
        }
        [HttpGet("GetAllFavorite")]
        public async Task<IActionResult> GetAllFavorites()
        {
            try
            {
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userid is null)
                    return BadRequest("Please login first");
                var id = int.Parse(userid);
                var data = await _favoriteServices.GetAllFavoritesModelByUserId(id);
                if(data.Count is 0)
                    return NotFound("No Products Found With This User");
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
        [HttpPost("Add Product In Favorite")]
        public async Task<IActionResult> AddProductInFavorite(FavoriteDTO favoriteDTO)
        {
            try
            {
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userid is null)
                    return BadRequest("Please login first");
                var id = int.Parse(userid);
                favoriteDTO.UserId = id;
                var added = await _favoriteServices.AddProductInFavorite(favoriteDTO);
                if(added)
                    return Ok("Added successfully");
                return BadRequest("Can't Add Into Favorites");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
        [HttpDelete("Delete Product From Favorite")]
        public async Task<IActionResult> RemoveProductFromFavorites(Guid FavoriteGuid)
        {
            try
            {
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userid is null)
                    return BadRequest("Please login first");
                var id = int.Parse(userid);
                var deleted = await _favoriteServices.RemoveProductFromFavorite(FavoriteGuid);
                if (deleted)
                    return Ok("Deleted successfully");
                return BadRequest("Can't Remove From Favorite");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }

        }
    }
}
