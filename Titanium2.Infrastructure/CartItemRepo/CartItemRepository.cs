using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Titanium2.Application.DTOs;
using Titanium2.Application.Interfaces.CartItemInterface;
using Titanium2.Domain.CartItem;
using Titanium2.Infrastructure.AppDbContext;

namespace Titanium2.Infrastructure.CartItemRepo
{
    internal class CartItemRepository : ICartItemInterface
    {
        ApplicationDbContext _context;

        public CartItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCartItem(CartItemDTO cartItemDTO)
        {
            var lastid = await _context.CartItems.AnyAsync() ? await _context.CartItems.MaxAsync(c => c.CartItemId) : 0;
            var hascart = await _context.Carts.AnyAsync(c => c.CartId == cartItemDTO.CartId);
            if (!hascart)
                throw new FileNotFoundException($"No Cart found with this Id: {cartItemDTO.CartId}");
            var hasproduct = await _context.Product.AnyAsync(p => p.ProductId == cartItemDTO.ProductId);
            if(!hasproduct)
                throw new FileNotFoundException($"No Product found with this Id: {cartItemDTO.ProductId}");
            var ifquantityisvalid = await _context.Stock.Where(s => s.ProductId == cartItemDTO.ProductId)
                                                        .AnyAsync(s => s.Quantity >= cartItemDTO.Quantity);
            if (!ifquantityisvalid)
                throw new Exception("Sorry, the quantity you insert is not valid!");

            var price = await _context.Product.SingleOrDefaultAsync(p => p.ProductId == cartItemDTO.ProductId);
            var totalprice = cartItemDTO.Quantity * price.Price;

            var cart = new CartItemModel
            {
                CartItemId = lastid + 1,
                CartItemGuid = Guid.NewGuid(),
                CartId = (int)cartItemDTO.CartId,
                ProductId = (int)cartItemDTO.ProductId,
                Quantity = (int)cartItemDTO.Quantity,
                TotalPrice = (decimal)totalprice,
            };
            await _context.CartItems.AddAsync(cart);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveCartItem(Guid guid)
        {
            var exist = await _context.CartItems.SingleOrDefaultAsync(c => c.CartItemGuid == guid);
            if (exist is null)
                throw new FileNotFoundException("No Cart found with this id");
            _context.CartItems.Remove(exist);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<CartItemModel>> GetCartItems()
        {
            var data = await _context.CartItems.Include(c => c.Product).ToListAsync();
            if (data.Count is 0)
                throw new Exception("No Data Found");
            return data;
        }

        public async Task<bool> UpdateCartItem(CartItemDTO cartItemDTO)
        {
            var data = await _context.CartItems.SingleOrDefaultAsync(c => c.CartItemGuid == cartItemDTO.CartItemGuid);

            if (data is null)
                throw new FileNotFoundException("No Cart found with this Guid");

            var ifquantityisvalid = await _context.Stock.Where(s => s.ProductId == cartItemDTO.ProductId)
                                                        .AnyAsync(s => s.Quantity >= cartItemDTO.Quantity);
            if (!ifquantityisvalid)
                throw new Exception("Sorry, the quantity you insert is not valid!");

            var price = await _context.Product.SingleOrDefaultAsync(p => p.ProductId == cartItemDTO.ProductId);
            if (cartItemDTO.Quantity > 0)
                data.TotalPrice = (decimal)cartItemDTO.Quantity * price.Price;

            data.ProductId = (int)cartItemDTO.ProductId > 0 ? (int)cartItemDTO.ProductId : data.ProductId;
            data.Quantity = (int)cartItemDTO.Quantity > 0 ? (int)cartItemDTO.Quantity : data.Quantity;
            data.CreatedAt = DateTime.UtcNow;
            data.UpdatedAt = DateTime.UtcNow;

            _context.CartItems.Update(data);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
