using DomainLibrary.IRepositories.Base;
using DomainLibrary.Models;

namespace DomainLibrary.IRepositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<List<Order>> GetOrdersByUserIdAsync(string userId);
    }
}