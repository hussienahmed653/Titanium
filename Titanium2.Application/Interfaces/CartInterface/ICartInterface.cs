using Titanium2.Application.DTOs;
using Titanium2.Domain.Cart;

namespace Titanium2.Application.Interfaces.CartInterface
{
    public interface ICartInterface
    {
        public Task<List<CartModel>> GetAllCarts();
        public Task<bool> AddToCart(CartDTO cartDTO);
        public Task<bool> RemoveFromCart(Guid guid);
    }
}
