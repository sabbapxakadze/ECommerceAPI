using DomainLibrary.IRepositories;
using DomainLibrary.Models;
using InfrastructureLibrary.Context;
using InfrastructureLibrary.Data.Base;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLibrary.Data
{
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Cart> GetCartByUserId(string id)
        {
            var cart = await _dbContext.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.UserId == id);

            if (cart == null)
            {
                cart = new Cart { UserId = id };
                _dbContext.Carts.Add(cart);
                await _dbContext.SaveChangesAsync();
            }
            return cart;
        }
    }
}