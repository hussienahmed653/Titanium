using Titanium2.Domain.Stock;

namespace Titanium2.Application.Interfaces.StockInterface
{
    public interface ISockInterface
    {
        public Task<bool> AddProductInStock(StockModel stock);
        public Task<bool> UpdateProductInStock(StockModel stock);
        public Task<bool> RemoveProductInStock(StockModel stock);
        // دول الي انا هستخدمم في اماكن تانيه في ال services
        public Task<bool> IfQuantityIsValid(int? productid, int? quantity);
        public Task<int> LastId();
        public Task<bool> IsThisProductExistInStock(int productid);
        public Task<StockModel> GetStockByGuid(Guid guid);
    }
}
