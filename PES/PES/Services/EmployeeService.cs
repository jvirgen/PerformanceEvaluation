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
        public string Profile, Name;

        //Get ONE employee By Email
        public Employee GetByEmail(string Emial)
        {
            Employee employee = null;
            using (OracleConnection db = dbContext.GetDBConnection())
            {
                string Query = "SELECT FIRST_NAME," +
                                       "LAST_NAME," +
                                       "EMAIL," +
                                       "CUSTOMER," +
                                       "POSITION," +
                                       "ID_MANAGER," +
                                       "HIRE_DATE," +
                                       "RANKING," +
                                       "END_DATE)" +
                                       "FROM EMPLOYEE WHERE ID_EMPLOYEE = " + employee.Email;
                
                OracleCommand Comand = new OracleCommand(Query, db);
                OracleDataReader Read = Comand.ExecuteReader();
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
                    string endDate = Convert.ToString(Read["END_DATE"]);

                    if (!string.IsNullOrEmpty(endDate))
                    {
                        employee.EndDate = Convert.ToDateTime(endDate);
                    }
                    else
                    {
                        employee.EndDate = null;
                    }
                }
            }

            return employee;
        }

        //Get all employees
        public List<Employee> GetAll()
        {
            List<Employee> employees = null;
            Employee employee = null;
            using (OracleConnection db = dbContext.GetDBConnection())
            {
                string Query = "SELECT FIRST_NAME," +
                                       "LAST_NAME," +
                                       "EMAIL," +
                                       "CUSTOMER," +
                                       "POSITION," +
                                       "ID_MANAGER," +
                                       "HIRE_DATE," +
                                       "RANKING," +
                                       "END_DATE)" +
                                       "FROM EMPLOYEE";

                OracleCommand Comand = new OracleCommand(Query, db);
                OracleDataReader Read = Comand.ExecuteReader();
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
                    string endDate = Convert.ToString(Read["END_DATE"]);

                    if (!string.IsNullOrEmpty(endDate))
                    {
                        employee.EndDate = Convert.ToDateTime(endDate);
                    }
                    else
                    {
                        employee.EndDate = null;
                    }
                    employees.Add(employee);
                }
            }
            return employees;
        }

        // Insert a employee data in the DB
        public bool InsertEmployee(Employee employee)
        {
            bool status = false;

            // Connect to the DB 
                // insert
                try
                {
                    using (OracleConnection db = dbContext.GetDBConnection())
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
                }
                catch
                {
                    status = false;
                }
            
            return status;
        }


        //Get ID_Profile from the DB 
        public string GetUserProfile(string UserEmail)
        {
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    string QueryProfile = "SELECT ID_PROFILE FROM EMPLOYEE WHERE EMAIL=" + "'" + UserEmail + "'";
                    string QueryName = "SELECT FIRST_NAME FROM EMPLOYEE WHERE EMAIL=" + "'" + UserEmail + "'";
                    OracleCommand Comand = new OracleCommand(QueryProfile, db);
                    OracleDataReader Read = Comand.ExecuteReader();

                    while (Read.Read())
                    {
                        Profile = Convert.ToString(Read["ID_PROFILE"]);
                    }
                    Comand = new OracleCommand(QueryName, db);
                    Read = Comand.ExecuteReader();
                    while (Read.Read())
                    {
                        Name = Convert.ToString(Read["FIRST_NAME"]);
                    }
                }

                return Profile;
            }
            catch
            {
                return "0";
            }
        }

        //Get User Name to show in the Resource view 
        public string UserName()
        {
            return Name;
        }

        public enum ProfileUser
        {
            None = 0,
            Resource = 1,
            Manager = 2,
            Director = 3
        }
    }
}