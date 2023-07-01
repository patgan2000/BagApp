using BagApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BagApp.Repositories
{
    public class SqlRepository<T> : IRepository<T>
        where T : class, IEntity, new()
    {
        private readonly DbSet<T> _dbSet;
        private readonly DbContext _dbContext;

        public event EventHandler<T>? ItemAdded;
        public event EventHandler<T>? ItemRemoved;
        public event EventHandler<T>? ItemsSavedToFile;

        public SqlRepository(DbContext DbContext)
        {
            _dbContext = DbContext;
            _dbSet = _dbContext.Set<T>();

        }

        public IEnumerable<T> GetAll() => _dbSet.ToList();

        public T? GetById(int id) => _dbSet.Find(id);

        public void Add(T item)
        {
            _dbSet?.Add(item);
            ItemAdded?.Invoke(this, item);
        }

        public void Remove(T item)
        {
            _dbSet?.Remove(item);
            ItemRemoved?.Invoke(this, item);
            ItemsSavedToFile?.Invoke(this, default);
        }

        public void Save() => _dbContext.SaveChanges();

        public IEnumerable<T> Read()
        {
            return _dbSet?.ToList();
        }
    }
}
