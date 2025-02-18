using DomainLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.IService
{
    public interface IOrderService
    {
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<List<Order>> GetOrdersByUserIdAsync(string userId);
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order> PlaceOrderAsync(string userId);
        Task UpdateOrderStatusAsync(int orderId, string status);
    }
}