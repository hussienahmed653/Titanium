using Titanium2.Domain.UsersRoles;

namespace Titanium2.Domain.UserRepo
{
    public interface IUserRepo
    {
        public Task<UsersModel> GetUserByEmail(string email);
        public Task<bool> CheckPassword(string password);
        public Task<bool> CheckEmail(string email);
        public Task<bool> AddUser(UsersModel users);
        public Task<bool> AddRolesToUser(string email, int roleid);
        public Task<bool> DeleteRoleFromUser(string email, int roleid);
        // دول الي انا هستخدمم في اماكن تانيه في ال services
        public Task<bool> UserExist(int userid);
    }
}
