using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Education_API.ViewModels.Categories;

namespace Education_API.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<CategoryViewModel> GetCategoryByNameAsync(string name);
        public Task<CategoryViewModel> GetCategoryByIdAsync(int id);
        public Task<List<CategoryViewModel>> ListAllCategoriesAsync();
        public Task<List<string>> ListAllCategoryNamesAsync();

        public Task AddCategoryAsync(PostCategoryViewModel model);
        public Task DeleteCategoryAsync(int id);
        public Task UpdateCategoryAsync(int id, PostCategoryViewModel model);
        public Task<bool> SaveAllAsync();
        
    }
}