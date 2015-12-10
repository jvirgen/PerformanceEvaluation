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
    }
}