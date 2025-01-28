using Microsoft.EntityFrameworkCore;
using Titanium2.Application;
using Titanium2.Domain;
using Titanium2.Domain.UserRepo;
using Titanium2.Domain.UsersRoles;
using Titanium2.Infrastructure.AppDbContext;

namespace Titanium2.Infrastructure.UserRepo
{
    public class UserRepository : IUserRepo
    {
        ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UsersModel> GetUserByEmail(string email)
        {
            var data = await _context.users
                .Include(u => u.usersroles)
                .SingleOrDefaultAsync(u => u.Email == email);
            if (data == null)
                return null;
            return data;
        }
        public async Task<bool> CheckEmail(string email)
        {
            var data = await _context.users.AnyAsync(u => u.Email == email);
            if(data)
                return true;
            return false;
        }
        public async Task<bool> CheckPassword(string password)
        {
            var data = await _context.users.AnyAsync(u => u.Password == password);
            if (data)
                return true;
            return false;
        }
        public async Task<bool> AddUser(UsersModel users)
        {
            try
            {
                var lastid = await _context.users.AnyAsync() ? await _context.users.MaxAsync(u => u.UserId) : 0;
                users.UserId = lastid+1;
                if (users.usersroles is null)
                {
                    users.usersroles = new List<UsersRolesModel>
                    {
                        new UsersRolesModel()
                        {
                             UserId = users.UserId,
                             RoleId = 3
                        }
                    };
                }
                else
                {
                    foreach (var roleid in users.usersroles)
                    {
                        var ur = new UsersRolesModel
                        {
                            UserId = users.UserId,
                            RoleId = roleid.RoleId
                        };
                    }
                }
                await _context.users.AddAsync(users);
                var rowsaffected = await _context.SaveChangesAsync();
                if(rowsaffected == 0)
                    return false;
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error, {ex.Message}");
                return false;
            }    
        }

        public async Task<bool> AddRolesToUser(string email, int roleid)
        {
            try
            {
                var data = await GetUserByEmail(email);
                if(data is null)
                {
                    Console.WriteLine("No Data Found With This Email!");
                    return false;
                }
                var existingroleid = await _context.usersroles.AnyAsync(r => r.RoleId == roleid);
                if(!existingroleid)
                {
                    Console.WriteLine("No RoleId Found");
                    return false;
                }
                var role = new UsersRolesModel
                {
                    UserId = data.UserId,
                    RoleId = roleid,
                };
                await _context.usersroles.AddAsync(role);
                var rowsaffected = await _context.SaveChangesAsync();
                if (rowsaffected == 0)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error, {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteRoleFromUser(string email, int roleid)
        {
            try
            {
                var data = await GetUserByEmail(email);
                if (data is null)
                {
                    Console.WriteLine("No Data Found With This Email!");
                    return false;
                }
                var existingroleid = await _context.usersroles
                    .FirstOrDefaultAsync(r => r.RoleId == roleid && r.UserId == data.UserId);
                if (existingroleid is null)
                {
                    Console.WriteLine("No RoleId Found");
                    return false;
                }
                _context.usersroles.Remove(existingroleid);
                var rowsaffected = await _context.SaveChangesAsync();
                if (rowsaffected == 0)
                    return false;
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error, {ex.Message}");
                return false;
            }
        }
    }
}
