using BagApp.Data.Entities;

namespace BagApp.Data.Repositories
{
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
        where T : class, IEntity
    {
        event EventHandler<T>? BagAdded;
        event EventHandler<T>? BagRemoved;
    }
}
