
using AppLibrary.IService;
using DomainLibrary.IRepositories;
using DomainLibrary.Models;

namespace AppLibrary.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepos;
        public ProductService(IProductRepository productRepos)
        {
            _productRepos = productRepos;
        }

        public async Task<Product?> AddNewProductAsync(Product product)
        {
            return await _productRepos.AddAsync(product);
        }

        public async Task DeleteProductAsync(Product product)
        {
            _productRepos.Delete(product);
        }

        public async Task<List<Product>> GetAllProductAsync()
        {
            return await _productRepos.GetAllProductAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _productRepos.GetByIdAsync(id);
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _productRepos.GetProductsByCategoryIdAsync(categoryId);
        }

        public async Task UpdateProductAsync(Product product)
        {
            _productRepos.Update(product);
        }
    }
}