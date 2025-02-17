using AppLibrary.IService;
using DomainLibrary.IRepositories;
using DomainLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.Service
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IProductRepository _productRepository;
        public CartService(ICartRepository cartRepository, UserManager<IdentityUser> userManager, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _userManager = userManager;
            _productRepository = productRepository;
        }

        public async Task AddToCartAsync(string userId, int productId, int quantity)
        {
            var cart = await _cartRepository.GetCartByUserId(userId);

            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                throw new Exception($"Product with given id:{productId} can not be found!");
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = quantity,
                    Price = product.Price,
                };

                cart.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }
            _cartRepository.Update(cart);
        }

        public async Task ClearCartAsync(string userId)
        {
            var cart = await _cartRepository.GetCartByUserId(userId);
            if (cart == null)
                throw new Exception("Cart not found");

            cart.CartItems.Clear();
            _cartRepository.Update(cart);
        }

        public async Task<Cart> GetCartAsync(string userId)
        {
            return await _cartRepository.GetCartByUserId(userId);
        }

        public async Task RemoveFromCartAsync(string userId, int productId)
        {
            var cart = await _cartRepository.GetCartByUserId(userId);
            if (cart == null) throw new Exception("Cart not found");

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (cartItem != null)
            {
                cart.CartItems.Remove(cartItem);
                _cartRepository.Update(cart);
            }
        }
    }
}