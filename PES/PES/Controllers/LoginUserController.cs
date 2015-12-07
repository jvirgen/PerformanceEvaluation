using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PES.Controllers
{
    public class LoginUserController : Controller
    {

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.User user)
        {
            if (user.Authentication(user.UserName, user.Password))
            {
                return RedirectToAction("Index", "PerformanceEvaluation");
            }
            else
            {
                ModelState.AddModelError("", "User or Passwore is incorrect");
            }
           return RedirectToAction("Login", "LoginUser");
        }
    }
}