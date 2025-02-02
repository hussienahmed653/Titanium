using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Titanium2.Domain.File;
using Titanium2.Domain.Product;

namespace Titanium2.Infrastructure.FileRepo
{
    public class FileConfig : IEntityTypeConfiguration<FileModel>
    {
        public void Configure(EntityTypeBuilder<FileModel> builder)
        {
            builder.HasKey(f => f.FileId);
            builder.Property(f => f.FileId)
                .ValueGeneratedNever();


        }
    }
}
