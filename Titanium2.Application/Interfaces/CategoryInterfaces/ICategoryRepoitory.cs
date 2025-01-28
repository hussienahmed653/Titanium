using Titanium2.Domain.Category;

namespace Titanium2.Application.Interfaces.CategoryInterfaces
{
    public interface ICategoryRepoitory
    {
        public Task<List<CategoryModel>> GetAllCategory();
        public Task<CategoryModel> GetCategoryByName(string categoryname);
        public Task<bool> AddCategory(CategoryDTO category);
        public Task<bool> UpdateCategory(int? id, string categoryname);
        public Task<bool> DeleteCategory(int id);
    }
}
