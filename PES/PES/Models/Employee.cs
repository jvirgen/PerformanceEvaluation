using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Models
{
    /// <summary>
    /// This class will store a Employee
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Employee Id
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Employee Client 
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// Position
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Employee Profile Id
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        /// Manager Id
        /// </summary>
        public int ManagerId { get; set;}

        /// <summary>
        /// Hire Date
        /// </summary>
        public DateTime HireDate { get; set; }

        /// <summary>
        /// Employee Ranking
        /// </summary>
        public int Ranking { get; set;}

        /// <summary>
        /// End Date 
        /// </summary>
        public DateTime EndDate { get; set; }

    }
}