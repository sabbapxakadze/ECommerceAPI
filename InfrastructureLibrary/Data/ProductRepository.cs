using DomainLibrary.IRepositories;
using DomainLibrary.Models;
using InfrastructureLibrary.Context;
using InfrastructureLibrary.Data.Base;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLibrary.Data
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Product>> GetAllProductAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _dbContext.Products
                .Where(p => p.CategoryId == categoryId).ToListAsync();
        }
    }
}