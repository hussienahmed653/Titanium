using Titanium2.Application.DTOs;
using Titanium2.Application.Interfaces.FavoriteInterface;
using Titanium2.Application.Interfaces.ProductInterfaces;
using Titanium2.Domain.Favorites;
using Titanium2.Domain.UserRepo;

namespace Titanium2.Application.Services
{
    public class FavoriteServices
    {
        IFavoriteInterface _favoriteInterface;
        IproductInterface _productInterface;
        public FavoriteServices(IFavoriteInterface favoriteInterface, IproductInterface productInterface)
        {
            _favoriteInterface = favoriteInterface;
            _productInterface = productInterface;
        }

        public async Task<List<FavoritesModel>> GetAllFavoritesModelByUserId(int userid)
        {
            return await _favoriteInterface.GetAllFavoritesWithUserId(userid);
        }
        public async Task<bool> AddProductInFavorite(FavoriteDTO favorite)
        {
            if (favorite.UserId == null)
                throw new ArgumentNullException("Please login first!");
            var ifproductisexist = await _productInterface.HasProduct(favorite.UserId);
            if (!ifproductisexist)
                throw new ArgumentException("This Product Was Not Found");
            var data = await _favoriteInterface.GetFavoriteProductByUserIdAndProductID(favorite.UserId, favorite.ProductId);
            if (data is not null)
                throw new Exception("This user already have this product in favorite");
            var favoritemodel = new FavoritesModel
            {
                UserId = favorite.UserId,
                ProductId = favorite.ProductId,
                FavoriteGuid = Guid.NewGuid(),
                AddedAt = DateTime.UtcNow,
            };
            return await _favoriteInterface.AddProductToFavorite(favoritemodel);
        }
        public async Task<bool> RemoveProductFromFavorite(Guid favguid)
        {
            var data = await _favoriteInterface.GetFavoriteProductByFavoriteGuid(favguid);
            if (data is null)
                throw new FileNotFoundException("We Can't Find Product With This User");
            return await _favoriteInterface.RemoveProductFromFavorite(data);
        }
    }
}
