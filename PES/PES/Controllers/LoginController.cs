using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Exchange.WebServices.Data;

namespace PES.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult LoginUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginUser(Models.User user)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Idex", "PerformanceEvaluation");
            }
            else
            {
                ModelState.AddModelError("", "Passwor or user invalid");
            }
            return RedirectToAction("LoginUser", "Login");           
        }
    }
}