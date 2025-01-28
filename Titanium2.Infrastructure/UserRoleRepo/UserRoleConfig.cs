using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Titanium2.Domain.UsersRoles;

namespace Titanium2.Infrastructure.UserRoleRepo
{
    internal class UserRoleConfig : IEntityTypeConfiguration<UsersRolesModel>
    {
        public void Configure(EntityTypeBuilder<UsersRolesModel> builder)
        {
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });
        }
    }
}
