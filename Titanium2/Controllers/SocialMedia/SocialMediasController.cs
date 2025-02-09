using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Titanium2.Application.DTOs;
using Titanium2.Application.Services;

namespace Titanium2.Api.Controllers.SocialMedia
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediasController : ControllerBase
    {
        SocialMediaServices _socialMediaServices;

        public SocialMediasController(SocialMediaServices socialMediaServices)
        {
            _socialMediaServices = socialMediaServices;
        }

        [HttpPost("AddSocialMediaAcoounts")]
        public async Task<IActionResult> AddSocialMediaAcoounts(SocialMediaDTO socialMediaDTO)
        {
            try
            {
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userid is null)
                    return BadRequest("Please Login First");
                socialMediaDTO.UsersId = int.Parse(userid);
                var added = await _socialMediaServices.AddSocialMediaAcoount(socialMediaDTO);
                if (!added)
                    return BadRequest("Can't add");
                return Ok("Added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateSocialMediaAcoounts")]
        public async Task<IActionResult> UpdateSocialMediaAcoounts(SocialMediaDTO socialMediaDTO)
        {
            try
            {
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userid is null)
                    return BadRequest("Please Login First");
                socialMediaDTO.UsersId = int.Parse(userid);
                var updated = await _socialMediaServices.UpdateSocialMediaAcoount(socialMediaDTO);
                if (!updated)
                    return BadRequest("Can't update");
                return Ok("Updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
        [HttpDelete("RemoveSocialMediaAcounts")]
        public async Task<IActionResult> RemoveSocialMediaAcounts(Guid guid)
        {
            try
            {
                var deleted = await _socialMediaServices.RemoveSocialMediaAcoount(guid);
                if (!deleted)
                    return BadRequest("Can't remove");
                return Ok("removed successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
    }
}
