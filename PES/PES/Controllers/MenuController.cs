using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PES.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult latenessMenu()
        {
            return View();
        }




        public ActionResult Choose()
        {
            //Get the session to make validations
            int useProfile = (int)Session["UserProfile"];
            string employeeEmail = (string)Session["UserEmail"];
            int employeeID = (int)Session["UserId"];


            //Redirecting to PerformanceEvaluation according USERPROFILE (1.-Resourse, 2.-Manager, 3.-Director)
            if (useProfile == 1)
            {
                return RedirectToAction("Index", "Lateness", new { employeeEmail, employeeID });
            }
            else
                return RedirectToAction("latenessMenu", "Menu", new { employeeEmail = Session["UserEmail"], employeeID = Session["UserId"] });
        }
    }
}