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
        private readonly DbSet<T> _bags;
        private readonly DbContext _dbContext;
        private string fileName = "bags.txt";

        public event EventHandler<T>? BagAdded;
        public event EventHandler<T>? BagRemoved;
        public event EventHandler<T>? FileSavedAdded;
        public event EventHandler<T>? FileSavedRemoved;


        public SqlRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _bags = _dbContext.Set<T>();
        }

        public IEnumerable<T> GetAll() => _bags.ToList();

        public Dictionary<int, string> GetAll1()
        {
            if (File.Exists(fileName))
            {
                var linesFromFile = new Dictionary<int, string>(); ;
                using(var reader = File.OpenText(fileName))
                {
                    var line = reader.ReadLine();
                    int i = 1;
                    while(line!= null)
                    {
                        linesFromFile.Add(i,line);
                        line = reader.ReadLine();
                        i++;
                    }
                }
                return linesFromFile;
            }
            else
            {
                throw new Exception("File doesn't exist.");
            }
        }

        public T? GetById(int id) => _bags.Find(id);

        public string GetById1(int id)
        {
            if (File.Exists(fileName))
            {
                var bags = GetAll1();
                var bag = bags[id];
                return bag;
            }
            else
            {
                throw new Exception("File doesn't exist.");
            }
        }

        public void Add(T item) 
        {
            _bags.Add(item);
            using(var writer = File.AppendText(fileName))
            {
                writer.Write(item);
            }
            BagAdded?.Invoke(this, item);
            FileSavedAdded?.Invoke(this, item);

        }
        public void Remove1(T item) 
        {
            if (File.Exists(fileName))
            {
                var bags = GetAll1();
                bags.Remove(item.Id);
                File.Delete(fileName);
                using(var writer = File.AppendText(fileName))
                {
                    foreach(var bag in bags)
                    {
                        writer?.WriteLine(bag.Value);
                    }
                }
                BagRemoved?.Invoke(this, item);
                FileSavedRemoved?.Invoke(this, item);
            }
            else
            {
                throw new Exception("File doesn't exist.");
            }
        } 

        public void Remove(T item)
        {
            _bags?.Remove(item);
            BagRemoved?.Invoke(this, item);
            FileSavedRemoved?.Invoke(this, item);
        }

        public void Save() => _dbContext.SaveChanges();
    }
}
