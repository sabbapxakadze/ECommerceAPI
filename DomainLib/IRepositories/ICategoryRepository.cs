using DomainLibrary.IRepositories.Base;
using DomainLibrary.Models;

namespace DomainLibrary.IRepositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        public Task<Category?> GetCategoryWithProductsAsync(int categoryId);
    }
}