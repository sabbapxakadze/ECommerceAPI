using DomainLibrary.Models;

namespace AppLibrary.IService
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryWithProductsAsync(int categoryId);
        Task<Category?> AddNewCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
        Task<Category?> GetCategoryByIdAsync(int id);
    }
}