using Titanium2.Application.DTOs;
using Titanium2.Application.Interfaces.CategoryInterfaces;
using Titanium2.Domain.Category;

namespace Titanium2.Application.Services
{
    public class CategoryServices
    {
        ICategoryInterface _category;

        public CategoryServices(ICategoryInterface category)
        {
            _category = category;
        }

        public async Task<List<CategoryModel>> GetAllCategories()
        {
            var categories = await _category.GetAllCategory();
            return categories;
        }

        public async Task<CategoryModel> GetCategoryByName(string name)
        {
            var category = await _category.GetCategoryByName(name);
            return category;
        }

        public async Task<bool> AddCategory(CategoryDTO categoryDTO)
        {
            return await _category.AddCategory(categoryDTO);
        }

        public async Task<bool> UpdateCategory(Guid guid, string categoryname)
        {
            return await _category.UpdateCategory(guid, categoryname);
        }

        public async Task<bool> DeleteCategory(Guid guid)
        {
            return await _category.DeleteCategory(guid);
        }
    }
}
