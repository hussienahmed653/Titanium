using Titanium2.Domain.Stock;

namespace Titanium2.Application.Interfaces.StockInterface
{
    public interface ISockInterface
    {
        public Task<List<StockModel>> GetAllProductInStock();
        public Task<List<StockModel>> GetProductInStockByName(string name);
        public Task<bool> AddProductInStock(Guid guid, int quantity);
        public Task<bool> UpdateProductInStock(Guid guid, int quantity);
        public Task<bool> RemoveProductInStock(Guid guid);
    }
}
