using AppLibrary.IService;
using DomainLibrary.IRepositories;
using DomainLibrary.Models;

namespace AppLibrary.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepos;

        public CategoryService(ICategoryRepository categoryRepos)
        {
            _categoryRepos = categoryRepos;
        }

        public async Task<Category> AddNewCategoryAsync(Category category)
        {
            return await _categoryRepos.AddAsync(category);
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            _categoryRepos.Delete(category);
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepos.GetAllCategoriesAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepos.GetByIdAsync(id);
        }

        public async Task<Category?> GetCategoryWithProductsAsync(int categoryId)
        {
            return await _categoryRepos.GetCategoryWithProductsAsync(categoryId);
        }
    }
}