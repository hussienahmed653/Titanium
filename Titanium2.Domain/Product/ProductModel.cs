using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Titanium2.Domain.CartItem;
using Titanium2.Domain.Category;
using Titanium2.Domain.File;
using Titanium2.Domain.Stock;

namespace Titanium2.Domain.Product
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public Guid ProductGuid { get; set; } = Guid.NewGuid();
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        [NotMapped]
        public List<FileModel>? FilePath { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
        public StockModel Stock { get; set; }
        public CartItemModel CartItem { get; set; }
    }
}
