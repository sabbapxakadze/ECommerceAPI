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

        public async Task<Category?> GetCategoryWithProductsAsync(int categoryId)
        {
            return await _categoryRepos.GetCategoryWithProductsAsync(categoryId);
        }
    }
}