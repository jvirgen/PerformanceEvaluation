using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using PES.Models;
using PES.Services;

namespace PES.Controllers
{
    [AllowAnonymous]
    public class EmployeeController : Controller
    {
        

        // GET: Employee
        public ActionResult Index()
        {   

            Login Mail = new Login();
            EmployeeService Getemployee = new EmployeeService();
            Employee Employee = new Employee();

            Employee = Getemployee.GetByEmail(Mail.UserEmail);
            return View(Employee);
        }

        [HttpGet]
        public ActionResult InsertEmployee()
        {
            // Current session
            // Get current user  
            Employee currentUser = new Employee();
            EmployeeService EmployeeService = new EmployeeService();
            currentUser = EmployeeService.GetByEmail((string)Session["UserEmail"]);


            if (currentUser.ProfileId == (int)ProfileUser.Resource)
            {
                // user is resource not allowed, return to home  
                // send error 
                return RedirectToAction("ViewEmployees");
            }
            ViewBag.currentUserProfileId = currentUser.ProfileId;


            // Get profiles 

            // Get managers 

            // Set data 

            // Return model
            Employee model = new Employee();

            return View(model);
        }

        [HttpPost]
        public ActionResult InsertEmployee(Employee employee)
        {
            EmployeeService Insert = new EmployeeService();
            employee.Customer = "No Customer";
            employee.Position = "No spicified";

            Insert.InsertEmployee(employee);
            return View(ViewEmployees());
        }

        public ActionResult ViewEmployees()
        {
            EmployeeService ViewEmployees = new EmployeeService();
            return View(ViewEmployees.GetAll());
        }
    }
}