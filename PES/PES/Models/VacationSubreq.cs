using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Models
{
    /// <summary>
    /// This class will store the dates of the vacation request
    /// </summary>
    public class VacationSubreq
    {
        /// <summary>
        /// Subrequest Id
        /// </summary>  
        public int subreqId { get; set; }

        /// <summary>
        /// Vacation header request Id
        /// </summary>
        public int vacationHeaderReqId { get; set; }

        /// <summary>
        /// Start date
        /// </summary>
        public DateTime startDate { get; set; }

        /// <summary>
        /// End date
        /// </summary>
        public DateTime endDate { get; set; }

        /// <summary>
        /// Return date
        /// </summary>
        public DateTime returnDate { get; set; }

        /// <summary>
        /// Name of lead
        /// </summary>
        public string lead_name { get; set; }

        /// <summary>
        /// Flag to know if have a project
        /// </summary>
        public string have_project { get; set; }
    }
}