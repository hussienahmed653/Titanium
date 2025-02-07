using Microsoft.EntityFrameworkCore;
using Titanium2.Application.DTOs;
using Titanium2.Application.Interfaces.CategoryInterfaces;
using Titanium2.Domain.Category;
using Titanium2.Infrastructure.AppDbContext;

namespace Titanium2.Infrastructure.CategoryRepo
{
    public class CategoryRepository : ICategoryInterface
    {
        ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCategory(CategoryModel category)
        {
            try
            { 
                await _context.Category.AddAsync(category);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<bool> DeleteCategory(CategoryModel category)
        {
            _context.Category.Remove(category);
            return _context.SaveChanges() > 0;
        }

        public async Task<List<CategoryModel>> GetAllCategory()
        {
            return await _context.Category
                .OrderBy(c => c.CategoryId)
                .ToListAsync();
        }

        public async Task<CategoryModel> GetCategoryByName(string categoryname)
        {
            return await _context.Category.SingleOrDefaultAsync(c => c.CategoryName == categoryname);
        }


        public async Task<bool> UpdateCategory(CategoryModel category)
        {
            _context.Category.Update(category);
            return _context.SaveChanges() > 0;
        }
        public async Task<int> LastId()
        {
            return await _context.Category.AnyAsync() ? await _context.Category.MaxAsync(c => c.CategoryId) : 0;
        }

        public async Task<bool> ThisCategoryNameIsExist(string name)
        {
            return await _context.Category.AnyAsync(c => c.CategoryName == name);
        }

        public async Task<CategoryModel> GetCategoryByGuid(Guid guid)
        {
            return await _context.Category.SingleOrDefaultAsync(c => c.CategoryGuid == guid);
        }
    }
}
