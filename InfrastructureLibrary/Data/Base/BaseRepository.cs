using Microsoft.EntityFrameworkCore;
using DomainLibrary.IRepositories.Base;
using InfrastructureLibrary.Context;

namespace InfrastructureLibrary.Data.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext _dbContext;
        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity)  
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync() 
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)  
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()  
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
