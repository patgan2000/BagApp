using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareStorageApp.Entities;

namespace WareStorageApp.Repositories
{
    public class SqlRepository<T> : IRepository<T>
        where T : class, IEntity, new()
    {
        private readonly DbSet<T> _dbSet;
        private readonly DbContext _dbContext;

        public event EventHandler<T>? WareAdded;
        public event EventHandler<T>? WareRemoved;

        public SqlRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public IEnumerable<T> GetAll() => _dbSet.ToList();

        public T? GetById(int id) => _dbSet.Find(id);

        public void Add(T item) 
        {
            _dbSet.Add(item);
            WareAdded?.Invoke(this, item);

        }
        public void Remove(T item) 
        {
            _dbSet.Remove(item);
            WareRemoved?.Invoke(this, item);
        } 

        public void Save() => _dbContext.SaveChanges();
    }
}
