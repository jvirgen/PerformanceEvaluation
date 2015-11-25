using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;


namespace PES.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public string Index()
        {
            string EmployeeName = "";

            // Connect to the database
            OracleConnection Connection = new OracleConnection();
            Connection.ConnectionString = "data source=localhost;user id=system;password=4colima";
            Connection.Open();

            // Read data
            OracleCommand Comand = new OracleCommand("SELECT FIRST_NAME FROM PES.EMPLOYEE", Connection);
            OracleDataReader Read = Comand.ExecuteReader();
            
            // Store sata
            while (Read.Read())
            {
                EmployeeName = Convert.ToString(Read["FIRST_NAME"]);       
            }

            Connection.Close();
            return "Employee name: " + EmployeeName;
        }
    }
}