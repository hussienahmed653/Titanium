using Titanium2.Application.Interfaces.StockInterface;
using Titanium2.Domain.Stock;

namespace Titanium2.Application.Services
{
    public class StockServices
    {
        ISockInterface _SockInterface;

        public StockServices(ISockInterface sockInterface)
        {
            _SockInterface = sockInterface;
        }
        public async Task<List<StockModel>> GetAllData()
        {
            return await _SockInterface.GetAllProductInStock();
        }
        public async Task<List<StockModel>> GetAllDataByName(string name)
        {
            return await _SockInterface.GetProductInStockByName(name);
        }

        public async Task<bool> AddInStock(Guid guid, int quantity)
        {
            return await _SockInterface.AddProductInStock(guid, quantity);
        }

        public async Task<bool> UpdateProductInStock(Guid guid, int quantity)
        {
            return await _SockInterface.UpdateProductInStock(guid, quantity);
        }

        public async Task<bool> RemoveProductInStock(Guid guid)
        {
            return await _SockInterface.RemoveProductInStock(guid);
        }
    }
}
