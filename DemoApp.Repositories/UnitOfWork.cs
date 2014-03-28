using DemoApp.Data;
using DemoApp.DataModel.Entities;
using DemoApp.DataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private IDbContext _context;
		private IRepository<User> _userRepository;
		private IRepository<Book> _bookRepository;
		private IRepository<Person> _personRepository;
		private IRepository<ContentObject> _contentRepository;

		private IDbContext Context
		{
			get { return _context ?? (_context = new DemoAppDataContext()); }
		}

		public void Save()
		{
			_context.SaveChanges();
		}
		
		public void Recycle()
		{
			if (_context != null)
			{
				_context.Dispose();
				_context = null;
			}
		}

		#region IDisposable
		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing && _context != null)
				{
					_context.Dispose();
				}
			}
			this.disposed = true;
		}

		void IDisposable.Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion

		public IRepository<User> UserRepository
		{
			get { return _userRepository ?? (_userRepository = new UserRepository(Context)); }
		}

		public IRepository<DataModel.Entities.Book> BookRepository
		{
			get { return _bookRepository ?? (_bookRepository = new BookRepository(Context)); }
		}

		public IRepository<DataModel.Entities.Person> PersonRepository
		{
			get { return _personRepository ?? (_personRepository = new PersonRepository(Context)); }
		}

		public IRepository<ContentObject> ContentRepository
		{
			get { return _contentRepository ?? (_contentRepository = new ContentRepository(Context)); }
		}
	}
}
