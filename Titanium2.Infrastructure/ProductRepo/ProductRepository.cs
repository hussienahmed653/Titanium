using Microsoft.EntityFrameworkCore;
using Titanium2.Application;
using Titanium2.Application.Interfaces.IImageInterface;
using Titanium2.Application.Interfaces.ProductInterfaces;
using Titanium2.Domain.Product;
using Titanium2.Infrastructure.AppDbContext;

namespace Titanium2.Infrastructure.ProductRepo
{
    public class ProductRepository : IproductRepoitory
    {
        ApplicationDbContext _context;
        IImageService _imageService;

        public ProductRepository(ApplicationDbContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        public async Task<bool> AddProduct(ProductDTO product)
        {
            var imagepath = await _imageService.UplodeImage(product.ImagePath, product.CategoryId);
            var lastid = await _context.Product.AnyAsync() ? await _context.Product.MaxAsync(p => p.ProductId) : 0;
            var newproduct = new ProductModel
            {
                ProductId = lastid + 1,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                ImagePath = imagepath,
                CategoryId = product.CategoryId,
            };
            await _context.Product.AddAsync(newproduct);
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> DeleteProduct(int productid)
        {
            var data = await _context.Product.FindAsync(productid);
            if (data is null)
            {
                throw new ArgumentNullException($"No Product found with this id: {productid}");
            }
            _context.Product.Remove(data);
            return _context.SaveChanges() > 0;

        }

        public async Task<List<ProductModel>> GetAllProducts()
        {
            return await _context.Product.ToListAsync();
        }

        public async Task<ProductModel> GetProductByName(string name)
        {
            return await _context.Product.FirstOrDefaultAsync(p => p.ProductName == name);
        }

        public async Task<bool> UpdateProduct(ProductDTO product)
        {
            string image = string.Empty;
            var myproduct = await _context.Product.SingleOrDefaultAsync(p => p.ProductId == product.ProductId);
            if (product.ImagePath != null)
            {
                image = await _imageService.UplodeImage(product.ImagePath, myproduct.CategoryId);
            }
            else
            {
                image = myproduct.ImagePath;
            }
            var updateproduct = new ProductModel
            {
                ProductId = myproduct.ProductId,
                ProductName = product.ProductName ?? myproduct.ProductName,
                Description = product.Description ?? myproduct.Description,
                Price = product.Price != default ? product.Price : myproduct.Price,
                ImagePath = image,
                CategoryId = product.CategoryId != default ? product.CategoryId : myproduct.CategoryId,
            };
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
