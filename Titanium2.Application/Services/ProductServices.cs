using Titanium2.Application.DTOs;
using Titanium2.Application.Interfaces.ProductInterfaces;
using Titanium2.Domain.Product;

namespace Titanium2.Application.Services
{
    public class ProductServices
    {
        IproductInterface _prosuctrepo;

        public ProductServices(IproductInterface prosuctrepo)
        {
            _prosuctrepo = prosuctrepo;
        }

        public async Task<List<ProductModel>> GetAllProduct()
        {
            return await _prosuctrepo.GetAllProducts();
        }

        public async Task<ProductModel> GetProductByName(string name)
        {
            return await _prosuctrepo.GetProductByName(name);
        }

        public async Task<bool> AddProduct(ProductDTO productDTO)
        {
            return await _prosuctrepo.AddProduct(productDTO);
        }

        public async Task<bool> UpdateProduct(ProductDTO productDTO)
        {
            return await _prosuctrepo.UpdateProduct(productDTO);
        }

        public async Task<bool> DeleteProduct(Guid guid)
        {
            return await _prosuctrepo.DeleteProduct(guid);
        }
    }
}
