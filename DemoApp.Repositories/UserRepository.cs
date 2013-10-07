using DemoApp.Data.Entities;
using System.Data.Objects;

namespace DemoApp.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ObjectContext context)
            : base(context)
        {
        }
    }
}
