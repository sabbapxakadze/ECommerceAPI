using DomainLibrary.IRepositories.Base;
using DomainLibrary.Models;

namespace DomainLibrary.IRepositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        public Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId);
    }
}
