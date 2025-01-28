using Titanium2.Domain.Product;

namespace Titanium2.Domain.Category
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public ICollection<ProductModel> product { get; set; }
    }
}
