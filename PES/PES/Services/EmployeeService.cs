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

        public EmployeeService()
        {
            dbContext = new PESDBContext();
        }

        //Get ONE employee By Email
        public Employee GetByEmail(string email)
        {
            Employee employee = null;
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();

                    string Query = "SELECT ID_EMPLOYEE," +
                                           "FIRST_NAME," +
                                           "LAST_NAME," +
                                           "EMAIL," +
                                           "CUSTOMER," +
                                           "POSITION," +
                                           "ID_PROFILE," +
                                           "ID_MANAGER," +
                                           "HIRE_DATE," +
                                           "RANKING," +
                                           "END_DATE " +
                                           "FROM EMPLOYEE WHERE EMAIL = '" + email + "'";

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

                    db.Close();
                }
            }
            catch
            {
                employee = null;
            }
            return employee;
        }

        //Get all employees
        public List<Employee> GetAll()
        {
            List<Employee> employees = null;
            Employee employee = null;
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string Query = "SELECT ID_EMLPOYEE" +
                                           "FIRST_NAME," +
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
                    db.Close();
                }

            }
            catch
            {
                employees = null;
            }
            return employees;
        }

        // Insert a employee data in the DB
        public bool InsertEmployee(Employee employee)
        {
            bool status = false;

            // Connect to the DB 
            using (OracleConnection db = dbContext.GetDBConnection())
            {
                db.Open();
                // insert
                try
                {
                    string InsertQuery = "INSERT INTO EMPLOYEE (FIRST_NAME," +
                                                               "LAST_NAME," +
                                                               "EMAIL," +
                                                               "CUSTOMER," +
                                                               "POSITION," +
                                                               "ID_PROFILE,"+
                                                               "ID_MANAGER," +
                                                               "HIRE_DATE," +
                                                               "RANKING," +
                                                               "END_DATE)" +
                                     " VALUES ('" + employee.FirstName + "', '" +
                                               employee.LastName + "', '" +
                                               employee.Email + "', '" +
                                               employee.Customer + "', '" +
                                               employee.Position + "', '" +
                                               employee.ProfileId+ "', '" +
                                               employee.ManagerId + "', '" +
                                               employee.HireDate.ToShortDateString() + "', '" +
                                               employee.Ranking + "', '" +
                                               (employee.EndDate.HasValue ? employee.EndDate.Value.ToShortDateString() : null) + "')";

                    OracleCommand Comand = new OracleCommand(InsertQuery, db);
                    Comand.ExecuteNonQuery();

                    status = true;
                }
                catch
                {
                    status = false;
                }
                db.Close();
            }
            return status;
        }


        //Get ID_Profile from the DB 
        public string GetUserProfile(string userEmail)
        {
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string QueryProfile = "SELECT ID_PROFILE FROM EMPLOYEE WHERE EMAIL=" + "'" + userEmail + "'";
                    string QueryName = "SELECT FIRST_NAME FROM EMPLOYEE WHERE EMAIL=" + "'" + userEmail + "'";
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
                    db.Close();
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

        //Get Users Name By Manager Id
        public List<Employee> GetEmployeeByManager(int ManageerId)
        {
            List<Employee> Employees = null;
            Employee employee = null;
            try
            {
                using(OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string GetEmployees = "SELECT ID_EMPLOYEE"+ 
                                       "FIRST_NAME," +
                                       "LAST_NAME," +
                                       "EMAIL," +
                                       "CUSTOMER," +
                                       "POSITION," +
                                       "ID_MANAGER," +
                                       "HIRE_DATE," +
                                       "RANKING," +
                                       "END_DATE)" +
                                       "FROM EMPLOYEE WHERE ID_MANAGER = "+ ManageerId;
                    OracleCommand Command = new OracleCommand(GetEmployees, db);
                    Command.ExecuteNonQuery();
                    OracleDataReader Reader = Command.ExecuteReader();

                    while (Reader.Read())
                    {
                        employee = new Employee();
                        employee.EmployeeId = Convert.ToInt32(Reader["ID_EMPLOYEE"]);
                        employee.FirstName = Convert.ToString(Reader["FIRST_NAME"]);
                        employee.LastName = Convert.ToString(Reader["LAST_NAME"]);
                        employee.Email = Convert.ToString(Reader["EMAIL"]);
                        employee.Customer = Convert.ToString(Reader["CUSTOMER"]);
                        employee.Position = Convert.ToString(Reader["POSITION"]);
                        employee.ManagerId = Convert.ToInt32(Reader["ID_MANAGER"]);
                        employee.HireDate = Convert.ToDateTime(Reader["HIRE_DATE"]);
                        employee.Ranking = Convert.ToInt32(Reader["RANKING"]);
                        string endDate = Convert.ToString(Reader["END_DATE"]);

                        if (!string.IsNullOrEmpty(endDate))
                        {
                            employee.EndDate = Convert.ToDateTime(endDate);
                        }
                        else
                        {
                            employee.EndDate = null;
                        }
                        Employees.Add(employee);
                    }
                    db.Close();
                }

            }
            catch
            {
                Employees = null;
            }
            return Employees;
        }
        
    }
}