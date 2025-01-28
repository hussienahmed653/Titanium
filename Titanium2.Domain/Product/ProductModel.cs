using Titanium2.Domain.Category;

namespace Titanium2.Domain.Product
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
    }
}
