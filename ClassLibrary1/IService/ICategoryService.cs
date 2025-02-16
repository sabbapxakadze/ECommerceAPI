using DomainLibrary.Models;

namespace AppLibrary.IService
{
    public interface ICategoryService
    {
        Task<Category?> GetCategoryWithProductsAsync(int categoryId);
    }
}