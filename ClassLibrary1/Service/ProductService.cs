
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

        public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _productRepos.GetProductsByCategoryIdAsync(categoryId);
        }
    }
}