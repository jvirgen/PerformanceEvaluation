using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PES.Models;
using PES.Services;

namespace PES.Controllers
{
    [AllowAnonymous]
    public class LoginUserController : Controller
    {
        public string UserEmail;
        EmployeeService _employeeService;

        public LoginUserController()
        {
            _employeeService = new EmployeeService();
            //using (PES.Controllers.PerformanceEvaluationController callsubtitle = new PerformanceEvaluationController())
            //    callsubtitle.Index();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login user)
        {
            var isAuthenticated = user.Authentication(user.UserEmail, user.Password);

            //Check if the Email and Password exist in Office 356
            if (isAuthenticated)
            {
                // Get user 
                Employee resource = _employeeService.GetByEmail(user.UserEmail);

                // Validate if resource not found
                if (resource != null)
                {
                    //Store the Resource profile in a variable session
                    Session["UserProfile"] = (int)resource.ProfileId;

                    //Store the Resource user name in a variable session
                    Session["UserName"] = resource.Email;

                    //Deside if the user is a Resouce
                    if ((ProfileUser)resource.ProfileId == ProfileUser.Resource)
                    {
                        //Return the Resource's view 
                        return RedirectToAction("ChoosePeriod", "PerformanceEvaluation");
                    }
                    //Check if the user is a Manager or Director 
                    else if ((ProfileUser)resource.ProfileId == ProfileUser.Manager)
                    {
                        //Return the Manager's view
                        return RedirectToAction("Index", "PerformanceEvaluation");
                    }
                    else if ((ProfileUser)resource.ProfileId == ProfileUser.Director)
                    {
                        //Return the Manager's view
                        return RedirectToAction("Index", "PerformanceEvaluation");
                    }
                    else
                    {
                        // Return to the login screen if no profile
                        // Message: You are not allowed
                        return RedirectToAction("Login", "LoginUser");
                    }
                }
                else 
                {
                    // resource not found
                    TempData["Error"] = "Resource not found";
                    return View("Login", user);
                }
            }
            else
            {
                // Return the login view if the user isn't valid 
                // Message: User or password are not valid
                TempData["Error"] = "User or password not correct. Please try again.";
                return View("Login", user);
            }
        }

        //Get session variables Profile
        //public string SessionProfile (string UserEmail)
        //{
        //    Session["UserProfile"] = PES.Services.EmployeeService.UserProfile(UserEmail);
        //    Session["UserName"] = UserName(UserEmail);
        //    string SessionProfile = (string)Session["UserProfile"];
        //    return SessionProfile;
        //}

        ////Get session variable Email
        //public string SessionEmail(Login user)
        //{
        //    Session["UserEmail"] = user.UserEmail;
        //    string SessionEmail = (string)Session["UserEmail"];
        //    return SessionEmail;
        //}

        //Get session variable Name
        //public string SessionName(User user)
        //{
        //    Session["UserName"] = user.UserName(user.UserEmail);
        //    string SessionName = (string)Session["UserName"];
        //    return SessionName;
        //}
    }
}