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
            builder.HasIndex(u => u.UserName).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();
        }
    }
}
