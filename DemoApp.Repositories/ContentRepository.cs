

using DemoApp.DataModel.Entities;
using DemoApp.DataModel.Interfaces;
namespace DemoApp.Repositories
{
    public class ContentRepository : Repository<ContentObject>
	{
		public ContentRepository(IDbContext context) : base(context) { }
    }
}
