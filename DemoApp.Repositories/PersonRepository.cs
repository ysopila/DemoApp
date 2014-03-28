

using DemoApp.DataModel.Entities;
using DemoApp.DataModel.Interfaces;
namespace DemoApp.Repositories
{
    public class PersonRepository : Repository<Person>
	{
		public PersonRepository(IDbContext context) : base(context) { }
    }
}
