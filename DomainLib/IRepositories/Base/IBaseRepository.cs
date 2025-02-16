using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLibrary.IRepositories.Base
{
    public interface IBaseRepository<T> where T : class
    {
        IReadOnlyList<T> GetAll();
        T GetById(Guid id);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SaveChanges();
    }
}
