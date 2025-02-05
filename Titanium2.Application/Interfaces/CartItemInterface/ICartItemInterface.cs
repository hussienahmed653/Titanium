using Titanium2.Application.DTOs;
using Titanium2.Domain.CartItem;

namespace Titanium2.Application.Interfaces.CartItemInterface
{
    public interface ICartItemInterface
    {
        public Task<List<CartItemModel>> GetCartItems();
        public Task<bool> AddCartItem(CartItemDTO cartItemDTO);
        public Task<bool> UpdateCartItem(CartItemDTO cartItemDTO);
        public Task<bool> RemoveCartItem(Guid guid);
    }
}
