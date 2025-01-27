using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Titanium2.Domain.UsersRoles;
namespace Titanium2.Infrastructure.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UsersModel> users { get; set; }
        public DbSet<RolesModel> roles { get; set; }
        public DbSet<UsersRolesModel> usersroles { get; set; }
    }
}
