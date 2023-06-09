﻿using WareStorageApp.Entities;

namespace WareStorageApp.Repositories
{
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
        where T : class, IEntity
    {
        event EventHandler<T>? WareAdded;
        event EventHandler<T>? WareRemoved;
    }
}