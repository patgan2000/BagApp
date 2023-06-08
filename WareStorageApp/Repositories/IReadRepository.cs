using WareStorageApp.Entities;

namespace WareStorageApp.Repositories
{
    public interface IReadRepository<out T> 
        where T : class, IEntity
    {
        IEnumerable<T> GetAll();

        T GetById(int id);
    }
}
