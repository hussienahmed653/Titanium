using Titanium2.Domain.Category;

namespace Titanium2.Application.Interfaces.CategoryInterfaces
{
    public interface ICategoryInterface
    {
        public Task<List<CategoryModel>> GetAllCategory();
        public Task<CategoryModel> GetCategoryByName(string categoryname);
        public Task<bool> AddCategory(CategoryDTO category);
        public Task<bool> UpdateCategory(Guid guid, string categoryname);
        public Task<bool> DeleteCategory(Guid guid);
    }
}
