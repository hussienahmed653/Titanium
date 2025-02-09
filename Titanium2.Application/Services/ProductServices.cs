using Microsoft.EntityFrameworkCore;
using Titanium2.Application.DTOs;
using Titanium2.Application.Interfaces.ProductInterfaces;
using Titanium2.Domain.Product;

namespace Titanium2.Application.Services
{
    public class ProductServices
    {
        IproductInterface _productrepo;

        public ProductServices(IproductInterface prosuctrepo)
        {
            _productrepo = prosuctrepo;
        }

        public async Task<List<ProductModel>> GetAllProduct()
        {

            return await _productrepo.GetAllProducts();
        }

        public async Task<ProductModel> GetProductByName(string name)
        {
            return await _productrepo.GetProductByName(name);
        }

        public async Task<bool> AddProduct(ProductDTO productDTO)
        {
            var lastid = await _productrepo.LastId();
            var newproduct = new ProductModel
            {
                ProductId = lastid + 1,
                ProductName = productDTO.ProductName,
                Description = productDTO.Description,
                Price = productDTO.Price,
                CategoryId = productDTO.CategoryId,
            };
            return await _productrepo.AddProduct(newproduct);
        }

        public async Task<bool> UpdateProduct(ProductDTO product)
        {
            var myproduct = await _productrepo.GetProductByGuid(product.ProductGuid);

            myproduct.ProductId = myproduct.ProductId;
            myproduct.ProductName = !string.IsNullOrEmpty(product.ProductName) ? product.ProductName : myproduct.ProductName;
            myproduct.Description = !string.IsNullOrEmpty(product.Description) ? product.Description : myproduct.Description;
            myproduct.Price = product.Price > 0 ? product.Price : myproduct.Price;
            myproduct.CategoryId = product.CategoryId > 0 ? product.CategoryId : myproduct.CategoryId;

            return await _productrepo.UpdateProduct(myproduct);
        }

        public async Task<bool> DeleteProduct(Guid guid)
        {
            var data = await _productrepo.GetProductByGuid(guid);
            if (data is null)
            {
                throw new ArgumentNullException($"No Product found with this guid: {guid}");
            }
            return await _productrepo.DeleteProduct(data);
        }
    }
}
