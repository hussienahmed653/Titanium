using Titanium2.Domain.Favorites;

namespace Titanium2.Application.Interfaces.FavoriteInterface
{
    public interface IFavoriteInterface
    {
        public Task<List<FavoritesModel>> GetAllFavoritesWithUserId(int userid);
        public Task<bool> AddProductToFavorite(FavoritesModel model);
        public Task<bool> RemoveProductFromFavorite(FavoritesModel model);
        public Task<FavoritesModel> GetFavoriteProductByUserIdAndProductID(int userid, int productid);
        public Task<FavoritesModel> GetFavoriteProductByFavoriteGuid(Guid guid);
    }
}
