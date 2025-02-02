using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Titanium2.Application;
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
        [HttpPost]
        public async Task<IActionResult> AddFile(FileDTO fileDTO)
        {
            try
            {
                var filepath = await _imageServices.AddFile(fileDTO);
                if (filepath is null)
                    return BadRequest("Can't add this path");
                return Ok(filepath);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
    }
}
