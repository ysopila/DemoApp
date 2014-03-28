using DemoApp.Business.Models;

namespace DemoApp.Business.Services.Abstractions
{
    public interface IUserService : IService<User>
    {
        User Get(string username);
    }
}
