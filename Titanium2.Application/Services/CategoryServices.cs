using Microsoft.EntityFrameworkCore;
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
            return await _category.GetAllCategory();
        }

        public async Task<CategoryModel> GetCategoryByName(string name)
        {
            return await _category.GetCategoryByName(name);
        }

        public async Task<bool> AddCategory(CategoryDTO categoryDTO)
        {
            var lastid = await _category.LastId();
            var hascategory = await _category.ThisCategoryNameIsExist(categoryDTO.Categoryname);
            if (hascategory)
                throw new FileNotFoundException("This Category Name Is already exist!");
            var addcategory = new CategoryModel
            {
                CategoryId = lastid + 1,
                CategoryName = categoryDTO.Categoryname,
            };
            return await _category.AddCategory(addcategory);
        }

        public async Task<bool> UpdateCategory(Guid guid, string categoryname)
        {
            var datacategory = await _category.GetCategoryByGuid(guid);
            if (datacategory == null)
                throw new FileNotFoundException("No Data Was Found With This Guid");

            datacategory.CategoryName = categoryname;
            return await _category.UpdateCategory(datacategory);
        }

        public async Task<bool> DeleteCategory(Guid guid)
        {
            var datacategory = await _category.GetCategoryByGuid(guid);
            if (datacategory == null)
                throw new FileNotFoundException("No Data Was Found With This Id");
            return await _category.DeleteCategory(datacategory);
        }
    }
}
