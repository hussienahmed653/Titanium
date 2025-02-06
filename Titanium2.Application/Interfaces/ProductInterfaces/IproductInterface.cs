using Titanium2.Application.DTOs;
using Titanium2.Domain.Product;

namespace Titanium2.Application.Interfaces.ProductInterfaces
{
    public interface IproductInterface
    {
        public Task<List<ProductModel>> GetAllProducts();
        public Task<ProductModel> GetProductByName(string name);
        public Task<bool> AddProduct(ProductDTO product);
        public Task<bool> UpdateProduct(ProductDTO product);
        public Task<bool> DeleteProduct(Guid guid);
        // دول الي انا هستخدمم في اماكن تانيه في ال services
        public Task<bool> HasProduct(int? productid);
    }
}
