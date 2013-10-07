using DemoApp.Business.Models;
using DemoApp.Business.Services;
using DemoApp.Web.Angular.Models;
using DemoApp.Web.Angular.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace DemoApp.Web.Angular.Controllers
{
    public class AccountController : Controller
    {
        private IUserService UserService { get; set; }

        public AccountController(IUserService service)
        {
            UserService = service;
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult SignIn(UserModel model)
        {
            return Json(new { AuthHeader = string.Format("{0} {1}", "DemoApp", string.Format("{0}:{1}", "Admin", Guid.NewGuid())), Success = true });

            var user = UserService.Get(model.Username);
            if (user != null && HashUtils.CompareHash(model.Password, user.Password, user.PasswordSalt))
            {
                user.AuthToken = Guid.NewGuid().ToString();
                UserService.Save(user);
                return Json(new { AuthHeader = string.Format("{0} {1}", "DemoApp", string.Format("{0}:{1}", user.Username, user.AuthToken)), Success = true });
            }
            return Json(new { Success = false, Message = "" });
        }

        [HttpPost]
        public JsonResult SignOut()
        {
            return Json(new { Success = true });
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult Register(UserModel model)
        {
            var salt = HashUtils.GenerateSalt();
            var user = new User
            {
                Username = "Admin",
                Password = HashUtils.GetHash(model.Password, salt),
                PasswordSalt = salt,
                DateCreated = DateTime.Now
            };
            UserService.Add(user);

            return Json(new { Username = user.Username, Success = true });
        }

    }
}
