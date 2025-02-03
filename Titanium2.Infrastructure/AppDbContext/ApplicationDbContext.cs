using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Titanium2.Domain.Category;
using Titanium2.Domain.File;
using Titanium2.Domain.Product;
using Titanium2.Domain.Stock;
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
        public DbSet<CategoryModel> Category { get; set; }
        public DbSet<ProductModel> Product { get; set; }
        public DbSet<FileModel> Files { get; set; }
        public DbSet<StockModel> Stock { get; set; }
    }
}
