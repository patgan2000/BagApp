
using BagApp.Entities;

namespace BagApp.Repositories
{
    public class ListRepository<T> : IRepository<T>
        where T : class, IEntity, new()
    {
        private readonly List<T> _items = new();

        public event EventHandler<T>? ItemAdded;
        public event EventHandler<T>? ItemRemoved;

        public IEnumerable<T> GetAll() => _items.ToList();

        public T GetById(int id) => _items.Single(T => T.Id == id);

        public void Add(T item)
        {
            item.Id = _items.Count + 1;
            _items.Add(item);
        }

        public void Remove(T item)
        {
            _items.Remove(item);
        }

        public void Save()
        {
        }

        public void deleteAllFromFile()
        {
            throw new NotImplementedException();
        }

        public void getAllToFile()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Read()
        {
            throw new NotImplementedException();
        }
    }
}
