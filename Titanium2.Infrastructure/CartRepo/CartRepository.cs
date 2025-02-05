using Microsoft.EntityFrameworkCore;
using Titanium2.Application.DTOs;
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

        public async Task<bool> AddToCart(CartDTO cartDTO)
        {
            var lastid = await _context.Carts.AnyAsync() ? await _context.Carts.MaxAsync(c => c.CartId) : 0;
            var userexist = await _context.users.AnyAsync(u => u.UserId == cartDTO.UserId);
            var ifcartexist = await _context.Carts.AnyAsync(u => u.UserId == cartDTO.UserId);
            if (ifcartexist)
                throw new Exception("This user is already exist");
            if (!userexist)
                throw new FileNotFoundException("No user found with this id");
            var cart = new CartModel
            {
                CartId = lastid + 1,
                CartGuid = Guid.NewGuid(),
                UserId = cartDTO.UserId,
            };
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

        public async Task<bool> RemoveFromCart(Guid guid)
        {
            var ifexist = await _context.Carts.AnyAsync(c => c.CartGuid == guid);
            if (!ifexist)
                throw new FileNotFoundException("No Data found");
            var data = await _context.Carts.SingleOrDefaultAsync(c => c.CartGuid == guid);
            _context.Carts.Remove(data);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
