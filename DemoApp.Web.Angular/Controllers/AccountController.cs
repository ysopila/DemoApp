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
using System.Web.Security;

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
            var user = UserService.Get(model.Username);
            if (user != null && HashUtils.CompareHash(model.Password, user.Password, user.PasswordSalt))
            {
                user.AuthToken = Guid.NewGuid().ToString();
                UserService.Save(user);
                FormsAuthentication.SetAuthCookie(user.Username, false);
                HttpContext.User = new GenericPrincipal(new GenericIdentity(user.Username), null);
                return Json(new
                {
                    AuthHeader = string.Format("{0} {1}", "DemoApp", string.Format("{0}:{1}", user.Username, user.AuthToken)),
                    Success = true
                });
            }
            return Json(new { Success = false, ErrorMessage = "The Username or Password provided is incorrect." });
        }


        [HttpGet]
        public JsonResult IsAuthenticated()
        {
            var user = UserService.Get(HttpContext.User.Identity.Name);
            if (user == null)
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            return Json(new
            {
                AuthHeader = string.Format("{0} {1}", "DemoApp", string.Format("{0}:{1}", user.Username, user.AuthToken)),
                Success = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SignOut()
        {
            var user = UserService.Get(User.Identity.Name);
            if (user == null)
                return Json(new { Success = false, ErrorMessage = "An unknown error occurred." });
            user.AuthToken = null;
            UserService.Save(user);
            FormsAuthentication.SignOut();
            return Json(new { Success = true });
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult Register(UserModel model)
        {
            var user = UserService.Get(model.Username);
            if (user == null)
            {
                var salt = HashUtils.GenerateSalt();
                user = new User
                {
                    Username = model.Username,
                    Password = HashUtils.GetHash(model.Password, salt),
                    PasswordSalt = salt,
                    DateCreated = DateTime.Now,
                    AuthToken = Guid.NewGuid().ToString()
                };
                UserService.Add(user);

                return Json(new
                {
                    AuthHeader = string.Format("{0} {1}", "DemoApp", string.Format("{0}:{1}", user.Username, user.AuthToken)),
                    Success = true
                });
            }
            return Json(new
            {
                Success = false,
                ErrorMessage = "Username already exists. Please enter a different user name."
            });
        }

    }
}
