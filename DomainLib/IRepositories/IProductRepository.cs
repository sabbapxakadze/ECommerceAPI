using DomainLibrary.Models;

namespace DomainLibrary.IRepositories
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId);
    }
}
