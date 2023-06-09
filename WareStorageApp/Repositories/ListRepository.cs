using WareStorageApp.Entities;

namespace WareStorageApp.Repositories
{
    public class ListRepository<T> : IRepository<T>, IReadRepository<T>
        where T : class, IEntity, new()
    {
        private readonly List<T> _items = new();

        public event EventHandler<T>? WareAdded;
        public event EventHandler<T>? WareRemoved;

        public IEnumerable<T> GetAll() => _items.ToList();

        public T? GetById(int id) => _items.Single(T => T.Id == id);

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
            Console.WriteLine("Data saved to file.");
        }

    }
}
