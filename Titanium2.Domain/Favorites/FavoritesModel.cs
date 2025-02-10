using Titanium2.Domain.Product;
using Titanium2.Domain.UsersRoles;

namespace Titanium2.Domain.Favorites
{
    public class FavoritesModel
    {
        public Guid FavoriteGuid { get; set; } = Guid.NewGuid();
        public int UserId { get; set; }
        public UsersModel User { get; set; }
        public int ProductId { get; set; }
        public ProductModel Product { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
