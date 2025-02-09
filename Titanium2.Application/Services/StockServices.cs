using Microsoft.EntityFrameworkCore;
using Titanium2.Application.Interfaces.ProductInterfaces;
using Titanium2.Application.Interfaces.StockInterface;
using Titanium2.Domain.Stock;

namespace Titanium2.Application.Services
{
    public class StockServices
    {
        ISockInterface _SockInterface;
        IproductInterface _productInterface;

        public StockServices(ISockInterface sockInterface, IproductInterface productInterface)
        {
            _SockInterface = sockInterface;
            _productInterface = productInterface;
        }
        public async Task<bool> AddInStock(Guid guid, int quantity)
        {
            var lastid = await _SockInterface.LastId();
            var productdata = await _productInterface.GetProductByGuid(guid);
            if (productdata is null)
                throw new FileNotFoundException();
            var isexists = await _SockInterface.IsThisProductExistInStock(productdata.ProductId);
            if (isexists)
                throw new Exception("This product is already exists in stocks");
            var newstock = new StockModel
            {
                StockId = lastid + 1,
                ProductId = productdata.ProductId,
                Quantity = quantity
            };
            return await _SockInterface.AddProductInStock(newstock);
        }

        public async Task<bool> UpdateProductInStock(Guid guid, int quantity)
        {
            var mydata = await _SockInterface.GetStockByGuid(guid);
            if (mydata is null)
                throw new FileNotFoundException();
            mydata.Quantity = quantity;
            return await _SockInterface.UpdateProductInStock(mydata);
        }

        public async Task<bool> RemoveProductInStock(Guid guid)
        {
            var havedata = await _SockInterface.GetStockByGuid(guid);
            if (havedata is null)
                throw new FileNotFoundException("No Product found in stock!");
            return await _SockInterface.RemoveProductInStock(havedata);
        }
    }
}
