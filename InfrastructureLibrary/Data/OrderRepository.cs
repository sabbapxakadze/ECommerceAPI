using DomainLibrary.IRepositories;
using DomainLibrary.Models;
using InfrastructureLibrary.Context;
using InfrastructureLibrary.Data.Base;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLibrary.Data
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext) { }
        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _dbContext.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .ToListAsync();
        }
    }
}