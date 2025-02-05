using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Titanium2.Application.DTOs;
using Titanium2.Application.Services;

namespace Titanium2.Api.Controllers.Image
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        ImageServices _imageServices;

        public FilesController(ImageServices imageServices)
        {
            _imageServices = imageServices;
        }
        [Authorize(Roles = "1,2")]
        [HttpPost("AddFile")]
        public async Task<IActionResult> AddFile(FileDTO fileDTO)
        {
            try
            {
                var filepath = await _imageServices.AddFile(fileDTO);
                if (filepath is null)
                    return BadRequest("Can't add this path");
                return Ok("File added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
        [Authorize(Roles = "1,2")]
        [HttpDelete("DeleteFile")]
        public async Task<IActionResult> RemoveFile(Guid guid)
        {
            try
            {
                var deleted = await _imageServices.RemoveFile(guid);
                if (!deleted)
                    return BadRequest("Can't remove this file!");
                return Ok("Deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
    }
}
