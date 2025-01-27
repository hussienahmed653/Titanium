using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Titanium2.Domain.UsersRoles;

namespace Titanium2.Infrastructure.RoleRepo
{
    public class RoleConfig : IEntityTypeConfiguration<RolesModel>
    {
        public void Configure(EntityTypeBuilder<RolesModel> builder)
        {
            builder.HasKey(r => r.RoleId);
            //builder.Property(u => u.usersroles).HasDefaultValue(new List<UsersRolesModel> { new UsersRolesModel() { RoleId = 3 } });

            //builder.HasMany(r => r.usersroles)
            //    .WithOne(r => r.Role)
            //    .HasForeignKey(r => r.RoleId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
