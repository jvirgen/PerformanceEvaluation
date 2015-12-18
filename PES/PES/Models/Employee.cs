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
        public DateTime? EndDate { get; set; }
        //public string EndDate { get; set; }

        /// <summary>
        /// Employee project 
        /// </summary>
        public string Project { get; set; }

    }

    public class EmployeeManagerViewModel 
    {
        public Employee employee { get; set; }
        public Employee manager { get; set; }
    }

    public class EmployeeChoosePeriodViewModel 
    {
        public decimal totalEvaluation { get; set; }
        public decimal totalPerforformance { get; set; }
        public decimal totalCompetences { get; set; }
        public decimal totalEnglish { get; set; }
    }

    public enum ProfileUser
    {
        None = 0,
        Resource = 1,
        Manager = 2,
        Director = 3
    }
}