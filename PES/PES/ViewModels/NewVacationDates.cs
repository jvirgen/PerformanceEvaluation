using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.ViewModels
{
    public class NewVacationDates
    {
        public DateTime? startDate { get; set; }

        /// <summary>
        /// End date
        /// </summary>
        public DateTime? endDate { get; set; }

        /// <summary>
        /// Return date
        /// </summary>
        public DateTime? returnDate { get; set; }

        /// <summary>
        /// Name of lead
        /// </summary>
        public string lead_name { get; set; }

        /// <summary>
        /// Flag to know if have a project
        /// </summary>
        public char? have_project { get; set; }

        public string date { get; set; }

    }
}