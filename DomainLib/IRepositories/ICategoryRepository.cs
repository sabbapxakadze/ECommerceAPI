using DomainLibrary.Models;

namespace DomainLibrary.IRepositories
{
    public interface ICategoryRepository
    {
        public Task<Category?> GetCategoryWithProductsAsync(int categoryId);
    }
}