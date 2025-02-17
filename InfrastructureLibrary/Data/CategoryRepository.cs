using DomainLibrary.IRepositories;
using DomainLibrary.Models;
using InfrastructureLibrary.Context;
using InfrastructureLibrary.Data.Base;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLibrary.Data
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryWithProductsAsync(int categoryId)
        {
            return await _dbContext.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == categoryId);
        }
    }
}
