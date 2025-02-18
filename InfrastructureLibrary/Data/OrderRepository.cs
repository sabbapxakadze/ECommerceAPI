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

        public async Task CreateOrderAsync(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _dbContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _dbContext.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .ToListAsync();
        }
        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _dbContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}