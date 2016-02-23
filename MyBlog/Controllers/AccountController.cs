using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Users;
using MyBlog.Models.Users;

namespace MyBlog.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountManager _accountManager;

        public AccountController(IAccountManager manager)
        {
            _accountManager = manager;
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _accountManager.GetUserAuth(new UserAuth
                {
                    Name = model.Name,
                    Password = model.Password
                });

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);

                    return RedirectToAction("UsersManagement", "Users");
                }

                ModelState.AddModelError("", "invalid username or password");
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var userModel = new UserAuth {Name = model.Name, Password = model.Password};
                var user = _accountManager.FindUserAuth(model.Name);

                if (user == null)
                {
                    _accountManager.AddUserAuth(userModel);

                    user = _accountManager.GetUserAuth(userModel);

                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);

                        return RedirectToAction("UsersManagement", "Users");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "user with such login and password exsist");
                }
            }

            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("UsersManagement", "Users");
        }
	}
}