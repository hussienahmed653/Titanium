using Titanium2.Domain.UserRepo;

namespace Titanium2.Application.JwtRgistrationAndLoginRepo
{
    public class JwtRemoveRoleFromUser
    {
        IUserRepo _userRepo;

        public JwtRemoveRoleFromUser(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<bool> RemoveRoleFromUser(string email, int roleid)
        {
            if(await _userRepo.DeleteRoleFromUser(email, roleid))
                return true;
            return false;
        }
    }
}
