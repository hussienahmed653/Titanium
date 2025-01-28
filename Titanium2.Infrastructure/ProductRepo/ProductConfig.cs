using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Titanium2.Domain.Product;

namespace Titanium2.Infrastructure.ProductRepo
{
    public class ProductConfig : IEntityTypeConfiguration<ProductModel>
    {
        public void Configure(EntityTypeBuilder<ProductModel> builder)
        {
            builder.HasKey(x => x.ProductId);

            builder.Property(x => x.ProductId)
                .ValueGeneratedNever();
        }
    }
}
