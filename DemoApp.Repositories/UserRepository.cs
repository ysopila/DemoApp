

using DemoApp.DataModel.Entities;
using DemoApp.DataModel.Interfaces;
namespace DemoApp.Repositories
{
    public class UserRepository : Repository<User>
	{
		public UserRepository(IDbContext context) : base(context) { }
    }
}
