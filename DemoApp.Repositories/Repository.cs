using System.Linq;
using System.Data.Objects;
using System.Data;

namespace DemoApp.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ObjectContext _context;

        public Repository(ObjectContext context)
        {
            _context = context;
        }

        public IQueryable<T> Get()
        {
            return _context.CreateObjectSet<T>();
        }

        public T Create(T entity)
        {
            _context.CreateObjectSet<T>().AddObject(entity);
            _context.SaveChanges();
            return entity;
        }

        public T Save(T entity)
        {
            _context.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            _context.DeleteObject(entity);
            _context.SaveChanges();
        }

        public ITransaction BeginTransaction()
        {
            if (_context.Connection.State != ConnectionState.Open)
            {
                _context.Connection.Open();
            }
            return new Transaction(_context.Connection.BeginTransaction());
        }
    }

    internal class Transaction : ITransaction
    {
        IDbTransaction _tran;

        public Transaction(IDbTransaction aTran)
        {
            _tran = aTran;
        }
        public void Commit()
        {
            _tran.Commit();
            _tran = null;
        }

        public void Rollback()
        {
            _tran.Rollback();
            _tran = null;
        }

        public void Dispose()
        {
            if (_tran != null)
                _tran.Rollback();
        }
    }
}