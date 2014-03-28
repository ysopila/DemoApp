using DemoApp.DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.DataModel.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		void Save();
		void Recycle();

		IRepository<User> UserRepository { get; }
		IRepository<Book> BookRepository { get; }
		IRepository<Person> PersonRepository { get; }
		IRepository<ContentObject> ContentRepository { get; }
	}
}
