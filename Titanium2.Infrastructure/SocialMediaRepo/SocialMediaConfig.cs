using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Titanium2.Domain.SocialMedia;

namespace Titanium2.Infrastructure.SocialMediaRepo
{
    public class SocialMediaConfig : IEntityTypeConfiguration<SocialMediaModel>
    {
        public void Configure(EntityTypeBuilder<SocialMediaModel> builder)
        {
            builder.HasKey(sm => sm.SocialMediaId);
            builder.Property(sm => sm.SocialMediaId)
                .ValueGeneratedNever();
        }
    }
}
