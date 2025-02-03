using Microsoft.EntityFrameworkCore;
using System;
using System.Xml.Linq;
using Titanium2.Application;
using Titanium2.Application.Interfaces.ImageInterface;
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

        public async Task<bool> UpdateProduct(ProductDTO product)
        {
            var myproduct = await _context.Product.AsNoTracking()
                .SingleOrDefaultAsync(p => p.ProductGuid == product.ProductGuid);

            myproduct.ProductId = myproduct.ProductId;
            myproduct.ProductName = !string.IsNullOrEmpty(product.ProductName) ? product.ProductName : myproduct.ProductName;
            myproduct.Description = !string.IsNullOrEmpty(product.Description) ? product.Description : myproduct.Description;
            myproduct.Price = product.Price > 0 ? product.Price : myproduct.Price;
            myproduct.CategoryId = product.CategoryId > 0 ? product.CategoryId : myproduct.CategoryId;
            _context.Product.Update(myproduct);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
