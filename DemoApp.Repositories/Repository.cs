using System.Linq;
using System.Data;
using DemoApp.DataModel.Interfaces;
using System.Data.Entity;
using System;

namespace DemoApp.Repositories
{
	public class Repository<T> : IRepository<T> where T : class
	{
		#region Dependency
		private IDbContext _context;

		public Repository(IDbContext context)
		{
			_context = context;
		}

		#endregion Dependency

		protected virtual IQueryable<T> Query
		{
			get { return _context.Set<T>(); }
		}

		public System.Collections.Generic.IEnumerable<T> Find(System.Linq.Expressions.Expression<System.Func<T, bool>> filter = null, System.Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
		{
			IQueryable<T> query = Query;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			foreach (var includeProperty in includeProperties.Split
				(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}

			if (orderBy != null)
			{
				return orderBy(query).ToList();
			}
			else
			{
				return query.ToList();
			}
		}

		public void Insert(T entity)
		{
			_context.Set<T>().Add(entity);
		}

		public void Update(T entity)
		{
			_context.Set<T>().Attach(entity);
			_context.Entry<T>(entity).State = EntityState.Modified;
		}

		public void Delete(T entity)
		{
			if (_context.Entry<T>(entity).State == EntityState.Deleted)
				_context.Set<T>().Attach(entity);
			_context.Set<T>().Remove(entity);
		}
	}
}