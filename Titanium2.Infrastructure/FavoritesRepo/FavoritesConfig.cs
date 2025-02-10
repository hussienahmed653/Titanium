using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Titanium2.Domain.Favorites;

namespace Titanium2.Infrastructure.FavoritesRepo
{
    public class FavoritesConfig : IEntityTypeConfiguration<FavoritesModel>
    {
        public void Configure(EntityTypeBuilder<FavoritesModel> builder)
        {
            builder.HasKey(f => new { f.UserId , f.ProductId });
        }
    }
}
