using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Titanium2.Domain.UsersRoles;

namespace Titanium2.Infrastructure.UserRepo
{
    public class UserConfig : IEntityTypeConfiguration<UsersModel>
    {
        public void Configure(EntityTypeBuilder<UsersModel> builder)
        {
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.Password).HasMaxLength(75);

            //builder.Property(u => u.usersroles).HasDefaultValue(new List<UsersRolesModel> { new UsersRolesModel() { RoleId = 3} });
            //builder.HasMany(u => u.usersroles)
            //    .WithOne(u => u.User)
            //    .HasForeignKey(u => u.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
