using DomainLibrary.IRepositories.Base;
using DomainLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLibrary.IRepositories
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
        public Task<Cart> GetCartByUserId(string id);
    }
}
