using Titanium2.Domain.Product;

namespace Titanium2.Domain.Stock
{
    public class StockModel
    {
        public int StockId { get; set; }
        public Guid StockGuid { get; set; } = Guid.NewGuid();
        public int ProductId { get; set; }
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }
    }
}
