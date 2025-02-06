using Microsoft.EntityFrameworkCore;
using Titanium2.Application.Interfaces.CartItemInterface;
using Titanium2.Domain.CartItem;
using Titanium2.Infrastructure.AppDbContext;

namespace Titanium2.Infrastructure.CartItemRepo
{
    public class CartItemRepository : ICartItemInterface
    {
        ApplicationDbContext _context;
        public CartItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCartItem(CartItemModel cartItem)
        {
            try
            {
                await _context.CartItems.AddAsync(cartItem);
                return await _context.SaveChangesAsync() > 0;
            }
            catch(Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<bool> RemoveCartItem(CartItemModel cartItem)
        {
            _context.CartItems.Remove(cartItem);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<CartItemModel>> GetCartItems()
        {
            var data = await _context.CartItems
                .Include(c => c.Product)
                .OrderBy(c => c.CartItemId)
                .ToListAsync();
            if (data.Count is 0)
                throw new FileNotFoundException("No Data Found");
            return data;
        }

        public async Task<bool> UpdateCartItem(CartItemModel cartItem)
        {
            _context.CartItems.Update(cartItem);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> GetLastId()
        {
            return await _context.CartItems.AnyAsync() ? await _context.CartItems.MaxAsync(c => c.CartItemId) : 0;
        }

        public async Task<CartItemModel> GetCartItemByGuid(Guid guid)
        {
            return await _context.CartItems.SingleOrDefaultAsync(c => c.CartItemGuid == guid);
        }
    }
}
