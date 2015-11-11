using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Models
{
    public class Employee
    {
        /// <summary>
        /// Employee Id
        /// </summary>
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Customer { get; set; }

        public string Position { get; set; }

        public int ProfileId { get; set; }

        public int ManagerId { get; set;}

        public DateTime HireDate { get; set; }

        public int Ranking { get; set;}

        public DateTime EndDate { get; set; }

    }
}