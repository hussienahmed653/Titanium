using Titanium2.Application.DTOs;
using Titanium2.Application.Interfaces.CartInterface;
using Titanium2.Domain.Cart;

namespace Titanium2.Application.Services
{
    public class CartServices
    {
        ICartInterface _cartInterface;

        public CartServices(ICartInterface cartInterface)
        {
            _cartInterface = cartInterface;
        }

        public async Task<List<CartModel>> GetCarts()
        {
            return await _cartInterface.GetAllCarts();
        }
        public async Task<bool> AddCarts(CartDTO cartDTO)
        {
            return await _cartInterface.AddToCart(cartDTO);
        }
        public async Task<bool> RemoveCart(Guid guid)
        {
            return await _cartInterface.RemoveFromCart(guid);
        }
    }
}
