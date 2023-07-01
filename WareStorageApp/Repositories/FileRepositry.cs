using BagApp.Entities;
using System.Text.Json;

namespace BagApp.Repositories
{
    public class FileRepositry<T> : IRepository<T>
        where T : class, IEntity, new()
    {
        private string fileName = "Bags.json";

        public event EventHandler<T>? ItemAdded;

        public event EventHandler<T>? ItemRemoved;

        public event EventHandler<T>? ItemsSaveToFile;

        public readonly List<T> _items = new();

        public FileRepositry()
        {
        }

        public void Add(T item)
        {
            item.Id = _items.Count + 1;
            _items.Add(item);
            ItemAdded?.Invoke(this, item);
        }


        public void Remove(T item)
        {
            _items.Remove(item);
            ItemRemoved?.Invoke(this, item);
        }

        public IEnumerable<T> GetAll()
        {
            return _items.ToList();
        }

        public T? GetById(int id)
        {
            if (_items.Exists(x => x.Id == id))
            {
                return _items.Single(item => item.Id == id);
            }
            else { return null; }
        }

        public void Save()
        {
            var objectSerialized = JsonSerializer.Serialize<IEnumerable<T>>(_items);
            File.WriteAllText(fileName, objectSerialized);
        }

        public IEnumerable<T> Read()
        {
            if (File.Exists(fileName))
            {
                var objectsSerialized = File.ReadAllText(fileName);
                var deserializedObject = JsonSerializer.Deserialize<IEnumerable<T>>(objectsSerialized);
                if (deserializedObject != null)
                {
                    foreach (var item in deserializedObject)
                    {
                        _items.Add(item);
                    }
                }
            }
            return _items;
        }
    }
}
