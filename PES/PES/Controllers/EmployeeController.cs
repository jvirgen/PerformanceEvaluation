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

        // GET: InsertEmployee
        public ActionResult InsertEmployee()
        {
            return View();
        }

        // POST: InsertEmployee
        [HttpPost]
        public ActionResult InsertEmployee(Employee employee)
        {
            EmployeeService Insert = new EmployeeService();
            employee.Customer = "No Customer";
            employee.Position = "Not specified";
            employee.Project = null;

            Insert.InsertEmployee(employee);
            return View("Index");
        }
    }
}