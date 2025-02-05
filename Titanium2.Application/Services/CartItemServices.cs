using Titanium2.Application.DTOs;
using Titanium2.Application.Interfaces.CartItemInterface;
using Titanium2.Domain.CartItem;

namespace Titanium2.Application.Services
{
    public class CartItemServices
    {
        ICartItemInterface _cartItemInterface;

        public CartItemServices(ICartItemInterface cartItemInterface)
        {
            _cartItemInterface = cartItemInterface;
        }

        public async Task<List<CartItemModel>> GetAllCarts()
        {
            return await _cartItemInterface.GetCartItems();
        }
        public async Task<bool> AddCart(CartItemDTO cartItemDTO)
        {
            return await _cartItemInterface.AddCartItem(cartItemDTO);
        }
        public async Task<bool> UpdateCart(CartItemDTO cartItemDTO)
        {
            return await _cartItemInterface.UpdateCartItem(cartItemDTO);
        }
        public async Task<bool> RemoveCart(Guid guid)
        {
            return await _cartItemInterface.RemoveCartItem(guid);
        }
    }
}
