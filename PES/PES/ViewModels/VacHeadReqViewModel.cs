using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;


namespace PES.ViewModels
{
    public class VacHeadReqViewModel : VacationHeaderReq
    {
        /// <summary>
        /// Fist name of the employee
        /// </summary>
        public string first_name { get; set; }

        /// <summary>
        /// Last name of the employee
        /// </summary>
        public string last_name { get; set; } 

        /// <summary>
        /// Days requested 
        /// </summary>
        public int freedays { get; set; }

        /// <summary>
        /// Status of the Request
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// Start Date of the Vacation Request
        /// </summary>
        public DateTime start_date { get; set; }

        /// <summary>
        /// End Date of the Vacation Request
        /// </summary>
        public DateTime end_date { get; set; }

        /// <summary>
        /// Return Date of the Employee's Vacations
        /// </summary>
        public DateTime return_date { get; set; }

        /// <summary>
        /// Lead's Name of the employee in the period of the vacations
        /// </summary>
        public string lead_name { get; set; }

        /// <summary>
        /// Flag to know if the employee has a project
        /// </summary>
        public char have_project { get; set; }
    }
}