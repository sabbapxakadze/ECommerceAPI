using DomainLibrary.Models;

namespace AppLibrary.IService
{
    public interface IProductService
    {
        Task<Product?> AddNewProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
        Task<List<Product?>> GetAllProductAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<List<Product?>> GetProductsByCategoryAsync(int categoryId);
    }
}