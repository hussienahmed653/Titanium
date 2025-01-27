using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Titanium2.Application;
using Titanium2.Application.JwtRgistrationAndLoginRepo;

namespace Titanium2.Api.Authontication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        JwtRegistrationRepo _jwtRegistration;
        JwtLoginRepo _jwtLogin;

        public AuthenticationsController(JwtRegistrationRepo jwtRegistration, JwtLoginRepo jwtLogin)
        {
            _jwtRegistration = jwtRegistration;
            _jwtLogin = jwtLogin;
        }

        [HttpPost("Registeration")]
        public async Task<IActionResult> UserRegistration(UserRegisterDTO userRegister)
        {
            try
            {
                await _jwtRegistration.UserRegister(userRegister);
                return Ok(userRegister);
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
                return Ok(token);
            return BadRequest("No Token Validation Found");
        }

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
    }
}
