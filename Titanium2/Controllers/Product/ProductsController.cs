using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Titanium2.Application.DTOs;
using Titanium2.Application.Services;

namespace Titanium2.Api.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        ProductServices _productsService;

        public ProductsController(ProductServices productsService)
        {
            _productsService = productsService;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var productdata = await _productsService.GetAllProduct();
                if (productdata.Count is 0)
                    return NotFound("No Products Found Here.");
                return Ok(productdata);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
        [HttpGet("GetProductByName")]
        public async Task<IActionResult> GetProductByNAme(string name)
        {
            try
            {
                var productdata = await _productsService.GetProductByName(name);
                if (productdata is null)
                    return NotFound("No Products Found Here.");
                return Ok(productdata);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
        [HttpPost("AddNewProduct")]
        public async Task<IActionResult> AddNewProduct(ProductDTO productDTO)
        {
            try
            {
                var added = await _productsService.AddProduct(productDTO);
                if(!added)
                    return BadRequest("Can't add this product.");
                return Ok("Added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(ProductDTO productDTO)
        {
            try
            {
                var updated = await _productsService.UpdateProduct(productDTO);
                if (!updated)
                    return BadRequest("Can't update this product");
                return Ok("Updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(Guid guid)
        {
            try
            {
                var deleted = await _productsService.DeleteProduct(guid);
                if (!deleted)
                    return BadRequest("Can't Delete this product.");
                return Ok("Deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
    }
}
