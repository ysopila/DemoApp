using DemoApp.Business.Models;
using DemoApp.Business.Services;
using System.Net;
using System.Web.Http;

namespace DemoApp.Api.Controllers
{
    public class UserController : ApiController
    {
        private IUserService UserService { get; set; }

        public UserController(IUserService service)
        {
            UserService = service;
        }

        public User Get(int id)
        {
            return UserService.Get(id);
        }

        public User Post(User user)
        {
            return UserService.Add(user);
        }

        public User Put(int id, User user)
        {
            var value = UserService.Get(id);
            if (value == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return UserService.Save(user);
        }
    }
}
