using Microsoft.EntityFrameworkCore;
using Titanium2.Application;
using Titanium2.Application.Interfaces.CategoryInterfaces;
using Titanium2.Domain.Category;
using Titanium2.Infrastructure.AppDbContext;

namespace Titanium2.Infrastructure.CategoryRepo
{
    public class CategoryRepository : ICategoryRepoitory
    {
        ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCategory(CategoryDTO category)
        {
            try
            {
                var lastid = await _context.Category.AnyAsync() ? await _context.Category.MaxAsync(c => c.CategoryId) : 0;
                var hascategory = await _context.Category.AnyAsync(c => c.CategoryName == category.Categoryname);
                if (hascategory)
                {
                    Console.WriteLine("This Category Name Is NotValid!");
                    return false;
                }
                var addcategory = new CategoryModel
                {
                    CategoryId = lastid + 1,
                    CategoryName = category.Categoryname
                };
                await _context.Category.AddAsync(addcategory);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteCategory(Guid guid)
        {
            var datacategory = await _context.Category.FindAsync(guid);
            if (datacategory == null)
            {
                Console.WriteLine("No Data Was Found With This Id");
                return false;
            }
            _context.Category.Remove(datacategory);
            return _context.SaveChanges() > 0;
        }

        public async Task<List<CategoryModel>> GetAllCategory()
        {
            return await _context.Category.ToListAsync();
        }

        public async Task<CategoryModel> GetCategoryByName(string categoryname)
        {
            return await _context.Category.SingleOrDefaultAsync(c => c.CategoryName == categoryname);
        }

        public async Task<bool> UpdateCategory(Guid guid, string categoryname)
        {
            var datacategory = await _context.Category.SingleOrDefaultAsync(c => c.CategoryGuid == guid);
            if (datacategory == null)
            {
                Console.WriteLine("No Data Was Found With This Id");
                return false;
            }
            datacategory.CategoryName = categoryname;
            _context.Category.Update(datacategory);
            return _context.SaveChanges() > 0;
        }
    }
}
