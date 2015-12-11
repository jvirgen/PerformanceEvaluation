using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PES.Models;
using PES.Services;

namespace PES.Controllers
{
    public class LoginUserController : Controller
    {
        public string UserEmail;
        EmployeeService Employee = new EmployeeService();
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login user)
        {
            //Check if the Email and Password exist in Office 356
            if (user.Authentication(user.UserEmail, user.Password))
            {
                // Get profile
                EmployeeService.ProfileUser userProfile = (PES.Services.EmployeeService.ProfileUser)int.Parse(Employee.UserProfile(user.UserEmail));

                //Store the Resource profile in a variable session
                Session["UserProfile"] = (int)userProfile;

                //Store the Resource name in a variable session
                Session["UserName"] = Employee.UserName();

                //Deside if the user is a Resouce
                if (userProfile == EmployeeService.ProfileUser.Resource)
                {
                    //Return the Resource's view 
                    return RedirectToAction("ChoosePeriod", "PerformanceEvaluation");
                }
                //Check if the user is a Manager or Director 
                else if (userProfile == EmployeeService.ProfileUser.Manager)
                {
                    //Return the Manager's view
                    return RedirectToAction("Index", "PerformanceEvaluation");
                }
                else if (userProfile == EmployeeService.ProfileUser.Director)
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
                // Return the login view if the user isn't valid 
                // Message: User or password are not valid
                return RedirectToAction("Login", "LoginUser");
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