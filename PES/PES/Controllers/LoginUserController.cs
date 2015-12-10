using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PES.Models;

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
        public ActionResult Login(User user)
        {
            //Check if the Email and Password exist in Office 356
            if (user.Authentication(user.UserEmail, user.Password))
            {
                // Get profile
                PES.Models.User.ProfileUser userProfile = (PES.Models.User.ProfileUser)int.Parse((SessionProfile(user)));

                //Deside if the user is a Resouce
                if (userProfile == PES.Models.User.ProfileUser.Resource)
                {
                    //Returns a resourse's view
                    return RedirectToAction("ChoosePeriod", "PerformanceEvaluation");
                }
                //Check if the user is a Manager or Director 
                else if (userProfile == PES.Models.User.ProfileUser.Manager || userProfile == PES.Models.User.ProfileUser.Director)
                {
                    //Returns a Manager's view
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
        public string SessionProfile (User user)
        {
            Session["UserProfile"] = user.UserProfile(user.UserEmail);
            Session["UserName"] = user.UserName(user.UserEmail);
            string SessionProfile = (string)Session["UserProfile"];
            return SessionProfile;
        }

        //Get session variable Email
        public string SessionEmail(User user)
        {
            Session["UserEmail"] = user.UserEmail;
            string SessionEmail = (string)Session["UserEmail"];
            return SessionEmail;
        }

        //Get session variable Name
        //public string SessionName(User user)
        //{
        //    Session["UserName"] = user.UserName(user.UserEmail);
        //    string SessionName = (string)Session["UserName"];
        //    return SessionName;
        //}
    }
}