using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DemoApp.DataModel.Interfaces
{
	public interface IRepository<T>
	{
		IEnumerable<T> Find(
			 Expression<Func<T, bool>> filter = null,
			 Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			 string includeProperties = "");
		void Insert(T obj);
		void Update(T obj);
		void Delete(T obj);
	}
}