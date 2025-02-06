using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Titanium2.Domain.CartItem;

namespace Titanium2.Infrastructure.CartItemRepo
{
    internal class CartItemConfig : IEntityTypeConfiguration<CartItemModel>
    {
        public void Configure(EntityTypeBuilder<CartItemModel> builder)
        {
            builder.HasKey(ci => ci.CartItemId);
            builder.Property(ci => ci.CartItemId)
                .ValueGeneratedNever();

        }
    }
}
