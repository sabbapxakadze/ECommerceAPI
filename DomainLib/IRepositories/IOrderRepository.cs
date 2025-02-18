using DomainLibrary.IRepositories.Base;
using DomainLibrary.Models;

namespace DomainLibrary.IRepositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<List<Order>> GetOrdersByUserIdAsync(string userId);
        Task<List<Order>> GetAllOrdersAsync();
        Task CreateOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
    }
}