using AppLibrary.IService;
using DomainLibrary.IRepositories;
using DomainLibrary.Models;

namespace AppLibrary.Service
{

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _orderRepository.GetOrdersByUserIdAsync(userId);
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        public async Task<Order> PlaceOrderAsync(string userId)
        {
            var cart = await _cartRepository.GetCartByUserId(userId);
            if (cart == null || !cart.CartItems.Any())
            {
                throw new Exception("Cart is empty. Cannot place order.");
            }

            var order = new Order
            {
                UserId = userId,
                OrderItems = cart.CartItems.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    Price = ci.Price
                }).ToList(),
                FullAmount = cart.CartItems.Sum(ci => ci.Quantity * ci.Price),
                Status = "Pending"
            };

            await _orderRepository.CreateOrderAsync(order);

            cart.CartItems.Clear();
            _cartRepository.Update(cart);

            return order;
        }

        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            order.Status = status;
            await _orderRepository.UpdateOrderAsync(order);
        }
    }
}