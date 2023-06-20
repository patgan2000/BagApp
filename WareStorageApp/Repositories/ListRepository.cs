using WareStorageApp.Entities;

namespace WareStorageApp.Repositories
{
    public class ListRepository<T> : IRepository<T>, IReadRepository<T>
        where T : class, IEntity, new()
    {
        private readonly List<T> _items = new();

        public event EventHandler<T>? BagAdded;
        public event EventHandler<T>? BagRemoved;
        public event EventHandler<T>? FileSaved;
        public event EventHandler<T>? FileRemoved;

        public IEnumerable<T> GetAll() => _items.ToList();

        public T? GetById(int id) 
        { 
            if(_items.Exists(x => x.Id == id))
            {
                return _items.Single(T => T.Id == id);
            }
            else
            {
                return null;
            }
        } 

        public void Add(T item)
        {
            item.Id = _items.Count + 1;
            _items.Add(item);
            BagAdded?.Invoke(this, item);
            FileSaved?.Invoke(this, item);
        }

        public void Remove(T item)
        {
            _items.Remove(item);
            BagRemoved?.Invoke(this, item);
            FileSaved?.Invoke(this, item);
        }

        public void Save()
        {
            Console.WriteLine("Data saved to file.");
        }

    }
}
