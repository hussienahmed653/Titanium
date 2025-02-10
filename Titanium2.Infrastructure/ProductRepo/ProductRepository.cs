using Microsoft.EntityFrameworkCore;
using Titanium2.Application.Interfaces.ProductInterfaces;
using Titanium2.Domain.File;
using Titanium2.Domain.Product;
using Titanium2.Domain.Stock;
using Titanium2.Infrastructure.AppDbContext;

namespace Titanium2.Infrastructure.ProductRepo
{
    public class ProductRepository : IproductInterface
    {
        ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddProduct(ProductModel product)
        {
            await _context.Product.AddAsync(product);
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> DeleteProduct(ProductModel data)
        {
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
                    CategoryId = p.CategoryId,
                    Stock = _context.Stock.Where(s => s.ProductId == p.ProductId)
                    .Select(s => new StockModel
                    {
                            StockId = s.StockId,
                            StockGuid = s.StockGuid,
                            ProductId = s.ProductId,
                            Quantity = s.Quantity,
                    })
                    .SingleOrDefault(),
                    FilePath = _context.Files
                    .Where(f => f.FolderGuid == p.ProductGuid)
                    .Select(f => new FileModel
                    {
                        FileId = f.FileId,
                        FileGuid = f.FileGuid,
                        FilePath = f.FilePath,
                        Extention = f.Extention,
                        Size = f.Size
                    })
                    .ToList()
                })
                .ToListAsync();
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
                    Stock = _context.Stock.Where(s => s.ProductId == p.ProductId)
                    .Select(s => new StockModel
                    {
                        StockId = s.StockId,
                        StockGuid = s.StockGuid,
                        ProductId = s.ProductId,
                        Quantity = s.Quantity,
                    })
                    .SingleOrDefault(),
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
        public async Task<bool> UpdateProduct(ProductModel product)
        {
            _context.Product.Update(product);
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<bool> HasProduct(int? productid)
        {
            return await _context.Product.AnyAsync(p => p.ProductId == productid);
        }
        public async Task<ProductModel> GetProductByGuid(Guid guid)
        {
            return await _context.Product
                .Include(p => p.Category)
                .SingleOrDefaultAsync(p => p.ProductGuid == guid);
        }
        public async Task<int> LastId()
        {
            return await _context.Product.AnyAsync() ? await _context.Product.MaxAsync(p => p.ProductId) : 0;
        }

        public async Task<ProductModel> GetProductById(int productid)
        {
            return await _context.Product.SingleOrDefaultAsync(p => p.ProductId == productid);
        }
    }
}
