using Titanium2.Application.DTOs;
using Titanium2.Application.Interfaces.CartInterface;
using Titanium2.Application.Interfaces.CartItemInterface;
using Titanium2.Application.Interfaces.ProductInterfaces;
using Titanium2.Application.Interfaces.StockInterface;
using Titanium2.Domain.CartItem;

namespace Titanium2.Application.Services
{
    public class CartItemServices
    {
        ICartItemInterface _cartItemInterface;
        ICartInterface _cartInterface;
        IproductInterface _productInterface;
        ISockInterface _SockInterface;

        public CartItemServices(ICartItemInterface cartItemInterface,
            ICartInterface cartInterface,
            IproductInterface productInterface,
            ISockInterface sockInterface)
        {
            _cartItemInterface = cartItemInterface;
            _cartInterface = cartInterface;
            _productInterface = productInterface;
            _SockInterface = sockInterface;
        }

        public async Task<List<CartItemModel>> GetAllCarts()
        {
            return await _cartItemInterface.GetCartItems();
        }


        public async Task<bool> AddCart(CartItemDTO cartItemDTO)
        {
            var lastid = await _cartItemInterface.GetLastId();
            var hascart = await _cartInterface.HasCart(cartItemDTO.CartId);
            if (!hascart)
                throw new FileNotFoundException($"No Cart found with this Id: {cartItemDTO.CartId}");
            var hasproduct = await _productInterface.HasProduct(cartItemDTO.ProductId);
            if (!hasproduct)
                throw new FileNotFoundException($"No Product found with this Id: {cartItemDTO.ProductId}");
            var ifcartidandproductidisexist = await _cartItemInterface
                .IfCartIdAndProductIsAlreadyExist((int)cartItemDTO.CartId, (int)cartItemDTO.ProductId);
            if (ifcartidandproductidisexist)
                throw new Exception("This CartId and ProductId is already exists!");
            var ifquantityisvalid = await _SockInterface.IfQuantityIsValid(cartItemDTO.ProductId, cartItemDTO.Quantity);
            if (!ifquantityisvalid)
                throw new Exception("Sorry, the quantity you insert is not valid!");



            var cart = new CartItemModel
            {
                CartItemId = lastid + 1,
                CartItemGuid = Guid.NewGuid(),
                CartId = (int)cartItemDTO.CartId,
                ProductId = (int)cartItemDTO.ProductId,
                Quantity = (int)cartItemDTO.Quantity,
            };
            return await _cartItemInterface.AddCartItem(cart);
        }
        
        public async Task<bool> UpdateCart(Guid? guid, int? quantity)
        {
            var data = await _cartItemInterface.GetCartItemByGuid((Guid)guid);

            if (data is null)
                throw new FileNotFoundException("No Cart found with this Guid");

            var ifquantityisvalid = await _SockInterface.IfQuantityIsValid(data.ProductId, quantity);
            if (!ifquantityisvalid)
                throw new Exception("Sorry, the quantity you insert is not valid!");

            //data.ProductId = (int)productid > 0 ? (int)productid : data.ProductId;
            data.Quantity = (int)quantity > 0 ? (int)quantity : data.Quantity;
            data.CreatedAt = DateTime.UtcNow;
            data.UpdatedAt = DateTime.UtcNow;
            return await _cartItemInterface.UpdateCartItem(data);
        }
        public async Task<bool> RemoveCart(Guid guid)
        {
            var exist = await _cartItemInterface.GetCartItemByGuid(guid);
            if (exist is null)
                throw new FileNotFoundException("No Cart found with this id");
            return await _cartItemInterface.RemoveCartItem(exist);
        }
    }
}
