using Microsoft.EntityFrameworkCore;
using Titanium2.Application;
using Titanium2.Application.Interfaces.IImageInterface;
using Titanium2.Application.Interfaces.ProductInterfaces;
using Titanium2.Domain.File;
using Titanium2.Domain.Product;
using Titanium2.Infrastructure.AppDbContext;

namespace Titanium2.Infrastructure.ProductRepo
{
    public class ProductRepository : IproductRepoitory
    {
        ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddProduct(ProductDTO product)
        {
            var lastid = await _context.Product.AnyAsync() ? await _context.Product.MaxAsync(p => p.ProductId) : 0;
            var newproduct = new ProductModel
            {
                ProductId = lastid + 1,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
            };
            await _context.Product.AddAsync(newproduct);
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> DeleteProduct(Guid guid)
        {
            var data = await _context.Product.SingleOrDefaultAsync(p => p.ProductGuid == guid);
            if (data is null)
            {
                throw new ArgumentNullException($"No Product found with this guid: {guid}");
            }
            _context.Product.Remove(data);
            return _context.SaveChanges() > 0;

        }

        public async Task<List<ProductModel>> GetAllProducts()
        {
            var products = await _context.Product
        .Select(p => new ProductModel
        {
            ProductId = p.ProductId,
            ProductGuid = p.ProductGuid,
            ProductName = p.ProductName,
            Description = p.Description,
            Price = p.Price,
            CategoryId = p.CategoryId
        })
        .ToListAsync(); // ✅ جلب المنتجات أولاً بدون الملفات

            foreach (var product in products)
            {
                product.FilePath = await _context.Files
                    .Where(f => f.FolderGuid == product.ProductGuid)
                    .Select(f => new FileModel
                    {
                        FilePath = f.FilePath,
                        Extention = f.Extention,
                        Size = f.Size
                    })
                    .ToListAsync();
            }
            return products;
        }

        public async Task<ProductModel> GetProductByName(string name)
        {
            var product = await _context.Product
                .Where(p => p.ProductName == name)
                .Select(p => new ProductModel
                {
                    ProductId= p.ProductId,
                    ProductGuid = p.ProductGuid,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    FilePath = _context.Files
                    .Where(f => f.FolderGuid == p.ProductGuid)
                    .Select (f => new FileModel
                    {
                        FilePath = f.FilePath,
                        Extention = f.Extention,
                        Size = f.Size
                    })
                    .ToList()
                })
                .FirstOrDefaultAsync();
            return product;
        }

        public async Task<bool> UpdateProduct(ProductDTO product)
        {
            var myproduct = await _context.Product.SingleOrDefaultAsync(p => p.ProductGuid == product.ProductGuid);
            var updateproduct = new ProductModel
            {
                ProductId = myproduct.ProductId,
                ProductName = product.ProductName ?? myproduct.ProductName,
                Description = product.Description ?? myproduct.Description,
                Price = product.Price != default ? product.Price : myproduct.Price,
                CategoryId = product.CategoryId != default ? product.CategoryId : myproduct.CategoryId,
            };
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
