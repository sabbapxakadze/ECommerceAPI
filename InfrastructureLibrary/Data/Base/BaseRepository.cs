using DomainLibrary.IRepositories.Base;
using InfrastructureLibrary.Context;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLibrary.Data.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        /*
            This is where we communicate with database,
            and it stands for all the classes.              
         */
        protected readonly AppDbContext _dbContext;
        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public IReadOnlyList<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public T GetById(Guid id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}