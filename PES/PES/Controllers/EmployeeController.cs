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
            //// Connect to the database
            //OracleConnection Connection = new OracleConnection();
            //Connection.ConnectionString = "data source=localhost;user id=system;password=4colima";
            //Connection.Open();

            //// Read data
            //string Query = "SELECT ID_EMPLOYEE, " +
            //                      "FIRST_NAME," +
            //                      "LAST_NAME," +
            //                      "EMAIL," +
            //                      "CUSTOMER," +
            //                      "POSITION," +
            //                      "ID_PROFILE," +
            //                      "ID_MANAGER," +
            //                      "HIRE_DATE," +
            //                      "RANKING," +
            //                      "END_DATE FROM EMPLOYEE";


            //OracleCommand Comand = new OracleCommand(Query, Connection);
            //OracleDataReader Read = Comand.ExecuteReader();
            //List<Employee> employees = new List<Employee>();
            //Employee employee;
            //// Store sata
            //while (Read.Read())
            //{
            //    // Store data in employee object 
            //    employee = new Employee();
            //    employee.EmployeeId = Convert.ToInt32(Read["ID_EMPLOYEE"]);
            //    employee.FirstName = Convert.ToString(Read["FIRST_NAME"]);
            //    employee.LastName = Convert.ToString(Read["LAST_NAME"]);
            //    employee.Email = Convert.ToString(Read["EMAIL"]);
            //    employee.Customer = Convert.ToString(Read["CUSTOMER"]);
            //    employee.Position = Convert.ToString(Read["POSITION"]);
            //    employee.ProfileId = Convert.ToInt32(Read["ID_PROFILE"]);
            //    employee.ManagerId = Convert.ToInt32(Read["iD_MANAGER"]);
            //    employee.HireDate = Convert.ToDateTime(Read["HIRE_DATE"]);
            //    employee.Ranking = Convert.ToInt32(Read["RANKING"]);
            //    string endDate = Convert.ToString(Read["END_DATE"]);

            //    if (!string.IsNullOrEmpty(endDate))
            //    {
            //        employee.EndDate = Convert.ToDateTime(endDate);
            //    }
            //    else 
            //    {
            //        employee.EndDate = null;
            //    }
                
            //    // ---
                
      
            //    // Add employee to the list
            //    employees.Add(employee);
            //}

            //Connection.Close();

            Login Mail = new Login();
            EmployeeService Getemployee = new EmployeeService();
            Employee Employee = new Employee();

            Employee = Getemployee.GetByEmail(Mail.UserEmail);
            return View(Employee);
        }
    }
}