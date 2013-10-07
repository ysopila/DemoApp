using DemoApp.Business.Models;

namespace DemoApp.Business.Services
{
    public interface IUserService : IService<User>
    {
        User Get(string username);
    }
}
