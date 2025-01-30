using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Titanium2.Application;
using Titanium2.Application.Services;

namespace Titanium2.Api.Controllers.Categories
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        CategoryServices _categoryServices;

        public CategoriesController(CategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var datacategory = await _categoryServices.GetAllCategories();
                if (datacategory.Count is 0)
                    return NotFound("No Data Found.");
                return Ok(datacategory);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
        [HttpGet("GetCategoryByName")]
        public async Task<IActionResult> GetCategoryByName(string name)
        {
            try
            {
                var datacategory = await _categoryServices.GetCategoryByName(name);
                if (datacategory is null)
                    return NotFound("No Data Found With This Name!");
                return Ok(datacategory);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
        [Authorize(Roles = "1,2")]
        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(CategoryDTO categoryDTO)
        {
            try
            {
                var added = await _categoryServices.AddCategory(categoryDTO);
                if (!added)
                    return BadRequest("Can't add this category");
                return Ok("Added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
        [Authorize(Roles = "1,2")]
        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(CategoryDTO categoryDTO)
        {
            try
            {
                var updated = await _categoryServices.UpdateCategory(categoryDTO.CategoryId, categoryDTO.Categoryname);
                if (!updated)
                    return BadRequest("Can't Update This category!");
                return Ok("Updated Successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }

        [Authorize(Roles = "1,2")]
        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var deleted = await _categoryServices.DeleteCategory(id);
                if (!deleted)
                    return BadRequest("Can't Delete this category!");
                return Ok("Deleted Successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
    }
}
