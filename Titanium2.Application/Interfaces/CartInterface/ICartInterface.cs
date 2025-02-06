using Titanium2.Application.DTOs;
using Titanium2.Domain.Cart;

namespace Titanium2.Application.Interfaces.CartInterface
{
    public interface ICartInterface
    {
        public Task<List<CartModel>> GetAllCarts();
        public Task<bool> AddToCart(CartModel cart);
        public Task<bool> RemoveFromCart(CartModel cart);
        // دول الي انا هستخدمم في اماكن تانيه في ال services
        public Task<int> LastId();
        public Task<bool> HasCart(int? cartid);
        public Task<bool> IfUserHasCart(int userid);
        public Task<CartModel> GetCartByGuid(Guid guid);
    }
}
