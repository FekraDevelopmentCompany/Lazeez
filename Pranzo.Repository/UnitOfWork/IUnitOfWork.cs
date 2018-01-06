using Lazeez.Repository.Repository;
using System;

namespace Lazeez.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        S Service<S>() where S : class;
        //IRepository<T> Repository<T>() where T : class;
        int Save();
    }
}
