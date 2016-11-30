using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Models
{
    /// <summary>
    /// This class will store a vacation header request
    /// </summary>
    public class VacationHeaderReq
    {
        /// <summary>
        /// Vacation header request Id
        /// </summary>  
        public int vacationHeaderReqId { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>  
        public int employeeId { get; set; }

        /// <summary>
        /// Title of the request
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Number of vacation days
        /// </summary>
        public int noVacDays { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        public string comments { get; set; }

        /// <summary>
        /// Request status Id
        /// </summary>
        public int ReqStatusId { get; set; }

        /// <summary>
        /// Replay Comment
        /// </summary>
        public string replayComment { get; set; }

        /// <summary>
        /// Name of lead
        /// </summary>
        public string lead_name { get; set; }

        /// <summary>
        /// Flag to know if have a project
        /// </summary>
        public Char have_project { get; set; }

        /// <summary>
        /// Number of unpaid days
        /// </summary>
        public int noUnpaidDays { get; set; }

    }

    public class VacHeadReqViewModel : VacationHeaderReq
    {
        public string status { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public DateTime return_date { get; set; }
    }
}