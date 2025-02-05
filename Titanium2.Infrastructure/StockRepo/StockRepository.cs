using Microsoft.EntityFrameworkCore;
using Titanium2.Application.Interfaces.StockInterface;
using Titanium2.Domain.File;
using Titanium2.Domain.Product;
using Titanium2.Domain.Stock;
using Titanium2.Infrastructure.AppDbContext;

namespace Titanium2.Infrastructure.StockRepo
{
    public class StockRepository : ISockInterface
    {
        ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddProductInStock(Guid guid, int quantity)
        {
            var lastid = await _context.Stock.AnyAsync() ? await _context.Stock.MaxAsync(s => s.StockId) : 0;
            var productdata = await _context.Product.SingleOrDefaultAsync(p => p.ProductGuid == guid);
            if (productdata is null)
                throw new FileNotFoundException();
            var isexists = await _context.Stock.AnyAsync(s => s.ProductId == productdata.ProductId);
            if (isexists)
                throw new Exception("This product is already exists in stocks");
            var newstock = new StockModel
            {
                StockId = lastid + 1,
                ProductId = productdata.ProductId,
                Quantity = quantity
            };
            await _context.Stock.AddAsync(newstock);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveProductInStock(Guid guid)
        {
            var havedata = await _context.Stock.SingleOrDefaultAsync(s => s.StockGuid == guid);
            if(havedata is null)
                throw new FileNotFoundException("No Product found in stock!");
            _context.Stock.Remove(havedata);
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> UpdateProductInStock(Guid guid, int quantity)
        {
            var mydata = await _context.Stock.SingleOrDefaultAsync(s => s.StockGuid == guid);
            if(mydata is null)
                throw new FileNotFoundException();
            mydata.Quantity = quantity;
            _context.Stock.Update(mydata);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
