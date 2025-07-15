using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Movies.Domain.Interfaces.Base;

namespace Movies.Infrastructure.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _db;
        protected readonly DbSet<T> _dbSet;

        public Repository(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();    
        }

        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public void UpdateAsync(T entity) => _dbSet.Update(entity);

        public void DeleteAsync(T entity) => _dbSet.Remove(entity);
    }
}