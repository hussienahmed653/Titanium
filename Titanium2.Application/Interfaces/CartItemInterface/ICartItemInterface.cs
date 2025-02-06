using Titanium2.Application.DTOs;
using Titanium2.Domain.CartItem;

namespace Titanium2.Application.Interfaces.CartItemInterface
{
    public interface ICartItemInterface
    {
        public Task<List<CartItemModel>> GetCartItems();
        public Task<bool> AddCartItem(CartItemModel cartItem);
        public Task<bool> UpdateCartItem(CartItemModel cartItem);
        public Task<bool> RemoveCartItem(CartItemModel cartItem);
        // دول الي انا هستخدمم في اماكن تانيه في ال services
        public Task<int> GetLastId();
        public Task<CartItemModel> GetCartItemByGuid(Guid guid);
    }
}
