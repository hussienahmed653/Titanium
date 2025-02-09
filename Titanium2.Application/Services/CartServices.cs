using Titanium2.Application.DTOs;
using Titanium2.Application.Interfaces.CartInterface;
using Titanium2.Domain.Cart;
using Titanium2.Domain.UserRepo;

namespace Titanium2.Application.Services
{
    public class CartServices
    {
        ICartInterface _cartInterface;
        IUserRepo _userRepo;

        public CartServices(ICartInterface cartInterface,
            IUserRepo userRepo)
        {
            _cartInterface = cartInterface;
            _userRepo = userRepo;
        }

        public async Task<List<CartModel>> GetCarts()
        {
            return await _cartInterface.GetAllCarts();
        }
        public async Task<bool> AddCarts(CartDTO cartDTO)
        {
            var lastid = await _cartInterface.LastId();
            var userexist = await _userRepo.UserExist(cartDTO.UserId);
            var ifcartexist = await _cartInterface.IfUserHasCart(cartDTO.UserId);
            if (ifcartexist)
                throw new Exception("This user is already have cart");
            if (!userexist)
                throw new FileNotFoundException("No user found with this id");
            var cart = new CartModel
            {
                CartId = lastid + 1,
                CartGuid = Guid.NewGuid(),
                UserId = cartDTO.UserId,
            };
            return await _cartInterface.AddToCart(cart);
        }
        public async Task<bool> RemoveCart(Guid guid)
        {
            var data = await _cartInterface.GetCartByGuid(guid);
            if (data is null)
                throw new FileNotFoundException("No Data found");
            return await _cartInterface.RemoveFromCart(data);
        }
    }
}
