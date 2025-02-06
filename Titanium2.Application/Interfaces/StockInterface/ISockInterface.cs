using Titanium2.Domain.Stock;

namespace Titanium2.Application.Interfaces.StockInterface
{
    public interface ISockInterface
    {
        public Task<bool> AddProductInStock(Guid guid, int quantity);
        public Task<bool> UpdateProductInStock(Guid guid, int quantity);
        public Task<bool> RemoveProductInStock(Guid guid);
        // دول الي انا هستخدمم في اماكن تانيه في ال services
        public Task<bool> IfQuantityIsValid(int? productid, int? quantity);
    }
}
