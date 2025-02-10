using Microsoft.EntityFrameworkCore;
using Titanium2.Application.Interfaces.FavoriteInterface;
using Titanium2.Domain.Favorites;
using Titanium2.Infrastructure.AppDbContext;

namespace Titanium2.Infrastructure.FavoritesRepo
{
    internal class FavoriteRepository : IFavoriteInterface
    {
        ApplicationDbContext _context;

        public FavoriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FavoritesModel>> GetAllFavoritesWithUserId(int userid)
        {
            return await _context.Favorites
                .Include(f => f.Product)
                .ToListAsync();
        }
        public async Task<bool> AddProductToFavorite(FavoritesModel model)
        {
            try
            {
                await _context.Favorites.AddAsync(model);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> RemoveProductFromFavorite(FavoritesModel model)
        {
            try
            {
                _context.Favorites.Remove(model);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FavoritesModel> GetFavoriteProductByUserIdAndProductID(int userid, int productid)
        {
            return await _context.Favorites
                .SingleOrDefaultAsync(f => f.UserId == userid && f.ProductId == productid);
        }

        public async Task<FavoritesModel> GetFavoriteProductByFavoriteGuid(Guid guid)
        {
            return await _context.Favorites
                .SingleOrDefaultAsync(f => f.FavoriteGuid == guid);
        }
    }
}
