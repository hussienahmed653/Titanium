using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Titanium2.Domain.Category;

namespace Titanium2.Infrastructure.CategoryRepo
{
    public class CategoryConfig : IEntityTypeConfiguration<CategoryModel>
    {
        public void Configure(EntityTypeBuilder<CategoryModel> builder)
        {
            builder.HasKey(p => p.CategoryId);
            builder.Property(c => c.CategoryId)
                .ValueGeneratedNever();

        }
    }
}
