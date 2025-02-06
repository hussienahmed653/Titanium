using Microsoft.EntityFrameworkCore;
using Titanium2.Application.Interfaces.CartInterface;
using Titanium2.Domain.Cart;
using Titanium2.Infrastructure.AppDbContext;

namespace Titanium2.Infrastructure.CartRepo
{
    public class CartRepository : ICartInterface
    {
        ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddToCart(CartModel cart)
        {
            await _context.Carts.AddAsync(cart);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<CartModel>> GetAllCarts()
        {
            var data = await _context.Carts.ToListAsync();
            if (data.Count is 0)
                throw new Exception("No Data Found");
            return data;
        }
        public async Task<bool> RemoveFromCart(CartModel cart)
        {
            _context.Carts.Remove(cart);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> LastId()
        {
            return await _context.Carts.AnyAsync() ? await _context.Carts.MaxAsync(c => c.CartId) : 0;
        }

        public async Task<bool> IfUserHasCart(int userid)
        {
            return await _context.Carts.AnyAsync(u => u.UserId == userid);
        }

        public async Task<CartModel> GetCartByGuid(Guid guid)
        {
            return await _context.Carts.SingleOrDefaultAsync(c => c.CartGuid == guid);
        }
        public async Task<bool> HasCart(int? cartid)
        {
            return await _context.Carts.AnyAsync(c => c.CartId == cartid);
        }
    }
}
