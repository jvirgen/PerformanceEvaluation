using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.DBContext;
using PES.Models;
using Oracle.ManagedDataAccess.Client;

namespace PES.Services
{
    public class EmployeeService
    {
        private PESDBContext dbContext;

        public Employee GetById(int id)
        {
            Employee employee = null;
            using (OracleConnection db = dbContext.GetDBConnection())
            {

            }

            return employee;
        }

        public List<Employee> GetAll()
        {
            List<Employee> employees = null;
            using (OracleConnection db = dbContext.GetDBConnection())
            {

            }

            return employees;
        }

        public bool InsertEmployee(Employee employee)
        {
            bool status = false;

            // Connect to the DB 
            using (OracleConnection db = dbContext.GetDBConnection())
            {
                // insert
                try
                {
                    string InsertQuery = "INSERT INTO EMPLOYEE (FIRST_NAME," +
                                                               "LAST_NAME," +
                                                               "EMAIL," +
                                                               "CUSTOMER," +
                                                               "POSITION," +
                                                               "ID_MANAGER," +
                                                               "HIRE_DATE," +
                                                               "RANKING," +
                                                               "END_DATE)" +
                                     " VALUES (" + employee.FirstName + "," +
                                               employee.LastName + "," +
                                               employee.Email + "," +
                                               employee.Customer + "," +
                                               employee.Position + "," +
                                               employee.ManagerId + "," +
                                               employee.HireDate + "," +
                                               employee.Ranking + "," +
                                               employee.EndDate + ")";

                    OracleCommand Comand = new OracleCommand(InsertQuery, db);
                    Comand.ExecuteNonQuery();
                    
                    status = true;
                }
                catch
                {
                    status = false;
                }
            }
            return status;
        }
    }
}