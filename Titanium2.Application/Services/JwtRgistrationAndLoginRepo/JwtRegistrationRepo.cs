using Microsoft.AspNetCore.Http.HttpResults;
using Titanium2.Application.DTOs;
using Titanium2.Domain;
using Titanium2.Domain.UserRepo;
using Titanium2.Domain.UsersRoles;

namespace Titanium2.Application.Services.JwtRgistrationAndLoginRepo
{
    public class JwtRegistrationRepo
    {
        IUserRepo _userrepo;

        public JwtRegistrationRepo(IUserRepo userrepo)
        {
            _userrepo = userrepo;
        }
        public async Task<bool> UserRegister(UserRegisterDTO userRegister)
        {
            var hasemail = await _userrepo.CheckEmail(userRegister.Email);
            var checkpassword = await _userrepo.CheckPassword(userRegister.Password);
            if (hasemail || checkpassword || userRegister.Password != userRegister.ConfirmPassword)
                return false;
            var user = new UsersModel
            {
                Email = userRegister.Email,
                UserName = userRegister.UserName,
                Password = userRegister.Password,
                Address = userRegister.Address,
                PhoneNumber = userRegister.PhoneNumber,
                BirthDate = userRegister.BirthDate,
                Gender = userRegister.Gender,
            };
            var added = await _userrepo.AddUser(user);
            if (added)
                return true;
            return false;
        }

        public async Task<bool> AddRoleToUser(string email, int roleid)
        {
            var added = await _userrepo.AddRolesToUser(email, roleid);
            if (added)
                return true;
            return false;
        }

    }
}
