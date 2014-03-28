using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DemoApp.Repository.Test
{
	public class FakeDbSet<T> : DbSet<T>, IDbSet<T>
	where T : class
	{
		HashSet<T> _data;
		IQueryable _query;

		public FakeDbSet()
		{
			_data = new HashSet<T>();
			_query = _data.AsQueryable();
		}

		public override T Find(params object[] keyValues)
		{
			throw new NotImplementedException("Derive from FakeDbSet<T> and override Find");
		}

		public override T Add(T item)
		{
			_data.Add(item);
			return item;
		}

		public override T Remove(T item)
		{
			_data.Remove(item);
			return item;
		}

		public override T Attach(T item)
		{
			return item;
		}

		public void Detach(T item)
		{

		}

		Type IQueryable.ElementType
		{
			get { return _query.ElementType; }
		}

		System.Linq.Expressions.Expression IQueryable.Expression
		{
			get { return _query.Expression; }
		}

		IQueryProvider IQueryable.Provider
		{
			get { return _query.Provider; }
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _data.GetEnumerator();
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return _data.GetEnumerator();
		}
	}

}
