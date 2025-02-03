using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Titanium2.Application.Services;

namespace Titanium2.Api.Controllers.Stock
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        StockServices _stockServices;

        public StocksController(StockServices stockServices)
        {
            _stockServices = stockServices;
        }

        [HttpGet("GetAllProductInStock")]
        public async Task<IActionResult> GetAllProductInStock()
        {
            try
            {
                var alldata = await _stockServices.GetAllData();
                if (alldata.Count is 0)
                    return NotFound("No Data Was Found!");
                return Ok(alldata);
            }
            catch (Exception ex)
            {
                return BadRequest($"Errorr, {ex.Message}");
            }
        }
        [HttpGet("GetProductInStockByName")]
        public async Task<IActionResult> GetAllProductsInStockByName(string name)
        {
            try
            {
                var alldata = await _stockServices.GetAllDataByName(name);
                if (alldata.Count is 0)
                    return NotFound("No Data Was Found!");
                return Ok(alldata);
            }
            catch (Exception ex)
            {
                return BadRequest($"Errorr, {ex.Message}");
            }
        }
        [HttpPost("AddProductIntoStock")]
        public async Task<IActionResult> AddProductInStock(Guid guid, int quantity)
        {
            try
            {
                var added = await _stockServices.AddInStock(guid, quantity);
                if (!added)
                    return BadRequest("Can't add this product into stock!");
                return Ok("Product added successfully into stock.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
        [HttpPut("UpdateProductIntoStock")]
        public async Task<IActionResult> UpdateProductIntoStock(Guid guid, int quantity)
        {
            try
            {
                var updated = await _stockServices.UpdateProductInStock(guid, quantity);
                if (!updated)
                    return BadRequest("Can't Update!");
                return Ok("Updated successfully.");
            }
            catch (Exception ex) 
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
        [HttpDelete("DeleteProductFromStock")]
        public async Task<IActionResult> DeleteProductFromStock(Guid guid)
        {
            try
            {
                var deleted = await _stockServices.RemoveProductInStock(guid);
                if (!deleted)
                    return BadRequest("Can't remove product from stock");
                return Ok("Removed successfully.");
            }
            catch(Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
    }
}
