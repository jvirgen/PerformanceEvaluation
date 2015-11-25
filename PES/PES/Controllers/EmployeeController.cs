using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using PES.Models;

namespace PES.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            string EmployeeName = "";

            // Connect to the database
            OracleConnection Connection = new OracleConnection();
            Connection.ConnectionString = "data source=localhost;user id=system;password=4colima";
            Connection.Open();

            // Read data
            string query = "SELECT ID_EMPLOYEE, " +
                                  "FIRST_NAME," +


            OracleCommand Comand = new OracleCommand("SELECT * FROM PES.EMPLOYEE", Connection);
            OracleDataReader Read = Comand.ExecuteReader();

            List<Employee> employees = new List<Employee>();
            Employee employee;
            // Store sata
            while (Read.Read())
            {
                // Store data in employee object 
                employee = new Employee();
                employee.EmployeeId = Convert.ToInt32(Read["ID_EMPLOYEE"]);
                employee.FirstName = Convert.ToString(Read["FIRST_NAME"]);
                employee.LastName = Convert.ToString(Read["LAST_NAME"]);
                employee.Email = Convert.ToString(Read["EMAIL"]);
                employee.Customer = Convert.ToString(Read["CUSTOMER"]);
                employee.Position = Convert.ToString(Read["POSITION"]);
                employee.ProfileId = Convert.ToInt32(Read["ID_PROFILE"]);
                employee.ManagerId = Convert.ToInt32(Read["iD_MANAGER"]);
                employee.HireDate = Convert.ToDateTime(Read["HIRE_DATE"]);
                employee.Ranking = Convert.ToInt32(Read["RANKING"]);
                employee.EndDate = null;
                // ---
                
      
                // Add employee to the list
                employees.Add(employee);
            }

            Connection.Close();

            return View(employees);
        }
    }
}