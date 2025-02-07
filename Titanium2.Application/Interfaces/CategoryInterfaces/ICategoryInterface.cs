using Titanium2.Application.DTOs;
using Titanium2.Domain.Category;

namespace Titanium2.Application.Interfaces.CategoryInterfaces
{
    public interface ICategoryInterface
    {
        public Task<List<CategoryModel>> GetAllCategory();
        public Task<CategoryModel> GetCategoryByName(string categoryname);
        public Task<bool> AddCategory(CategoryModel category);
        public Task<bool> UpdateCategory(CategoryModel category);
        public Task<bool> DeleteCategory(CategoryModel category);

        public Task<int> LastId();
        public Task<bool> ThisCategoryNameIsExist(string name);
        public Task<CategoryModel> GetCategoryByGuid(Guid guid);
    }
}
