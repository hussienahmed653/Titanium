using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Titanium2.Domain.UserRepo;

namespace Titanium2.Application.JwtRgistrationAndLoginRepo
{
    public class JwtLoginRepo
    {
        IUserRepo _userRepo;
        IJwtServices _jwtServices;

        public JwtLoginRepo(IUserRepo userRepo, IJwtServices jwtServices)
        {
            _userRepo = userRepo;
            _jwtServices = jwtServices;
        }

        public async Task<string> UserLogin(UserLoginDTO userLogin)
        {
            var getuserbyemail = await _userRepo.GetUserByEmail(userLogin.Email);
            if (getuserbyemail is not null && (getuserbyemail.Password == userLogin.password))
            {
                var roleid = getuserbyemail.usersroles.Select(ur => ur.RoleId).ToList();
                var user = new UserDTO
                {
                    UserId = getuserbyemail.UserId,
                    UserName = getuserbyemail.UserName,
                    Email = getuserbyemail.Email,
                    Role = roleid
                };
                return await _jwtServices.GenerateToken(user);
            }
            return null;
        }
    }
}
