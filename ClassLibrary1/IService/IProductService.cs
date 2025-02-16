using DomainLibrary.Models;

namespace AppLibrary.IService
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsByCategoryAsync(int categoryId);
    }
}