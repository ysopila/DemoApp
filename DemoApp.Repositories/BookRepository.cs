
using System.Linq;
using System.Data.Entity;
using DemoApp.DataModel.Entities;
using DemoApp.DataModel.Interfaces;
namespace DemoApp.Repositories
{
    public class BookRepository : Repository<Book>
	{
		public BookRepository(IDbContext context) : base(context) { }

		protected override IQueryable<Book> Query
		{
			get
			{
				return base.Query.Include(x => x.Author);
			}
		}
    }
}
