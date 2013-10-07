using System;
using System.Linq;

namespace DemoApp.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        T Create(T aEntity);
        T Save(T aEntity);
        void Delete(T aEntity);

        ITransaction BeginTransaction();
    }
    public interface ITransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}