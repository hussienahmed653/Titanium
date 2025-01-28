using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Titanium2.Application;
using Titanium2.Application.Services.JwtRgistrationAndLoginRepo;

namespace Titanium2.Api.Authontication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        JwtRegistrationRepo _jwtRegistration;
        JwtLoginRepo _jwtLogin;
        JwtRemoveRoleFromUser _jwtremoverolefromuser;

        public AuthenticationsController(JwtRegistrationRepo jwtRegistration, JwtLoginRepo jwtLogin, JwtRemoveRoleFromUser jwtremoverolefromuser)
        {
            _jwtRegistration = jwtRegistration;
            _jwtLogin = jwtLogin;
            _jwtremoverolefromuser = jwtremoverolefromuser;
        }

        [HttpPost("Registeration")]
        public async Task<IActionResult> UserRegistration(UserRegisterDTO userRegister)
        {
            try
            {
                if(await _jwtRegistration.UserRegister(userRegister))
                    return Ok("User added successfully");
                return BadRequest("Can't add this user");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> UserLogin(UserLoginDTO userLogin)
        {
            var token = await _jwtLogin.UserLogin(userLogin);
            if(token is not null)
                return Ok(new 
                {   Token = token,
                    Errors = "No Errors Found"});
            return BadRequest("Your Login credential don't match an acount in our system.");
        }

        [Authorize(Roles = "1")]
        [HttpPost("AddRoleToUser")]
        public async Task<IActionResult> AddRoleToUser(string email, int roleid)
        {
            try
            {
                if(await _jwtRegistration.AddRoleToUser(email, roleid))
                    return Ok("Role added to user");
                return BadRequest("Can't add this role");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }

        [Authorize(Roles = "1")]
        [HttpDelete("RemoveRoleFromUser")]
        public async Task<IActionResult> RemoveRoleFromUser(string email, int roleid)
        {
            try
            {
                if (await _jwtremoverolefromuser.RemoveRoleFromUser(email, roleid))
                    return Ok("Role removed successfully");
                return BadRequest("Can't remove this role maybe this user don't have this role");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error, {ex.Message}");
            }
        }
    }
}
