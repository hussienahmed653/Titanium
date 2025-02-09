using Microsoft.EntityFrameworkCore;
using Titanium2.Application.Interfaces.StockInterface;
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

        public async Task<bool> AddProductInStock(StockModel stock)
        {
            await _context.Stock.AddAsync(stock);
            return await _context.SaveChangesAsync() > 0;
        }



        public async Task<bool> RemoveProductInStock(StockModel stock)
        {
            _context.Stock.Remove(stock);
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> UpdateProductInStock(StockModel stock)
        {
            _context.Stock.Update(stock);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<int> LastId()
        {
            return await _context.Stock.AnyAsync() ? await _context.Stock.MaxAsync(s => s.StockId) : 0; 
        }
        public async Task<bool> IfQuantityIsValid(int? productid, int? quantity)
        {
            return await _context.Stock.Where(s => s.ProductId == productid)
                                       .AnyAsync(s => s.Quantity >= quantity);
        }
        public async Task<bool> IsThisProductExistInStock(int productid)
        {
            return await _context.Stock.AnyAsync(s => s.ProductId == productid); ;
        }
        public async Task<StockModel> GetStockByGuid(Guid guid)
        {
            return await _context.Stock.SingleOrDefaultAsync(s => s.StockGuid == guid);
        }
    }
}
