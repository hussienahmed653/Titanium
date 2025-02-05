using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Titanium2.Domain.Cart;

namespace Titanium2.Infrastructure.CartRepo
{
    internal class CartConfig : IEntityTypeConfiguration<CartModel>
    {
        public void Configure(EntityTypeBuilder<CartModel> builder)
        {
            builder.HasKey(c => c.CartId);
            builder.Property(c => c.CartId)
                .ValueGeneratedNever();
        }
    }
}
