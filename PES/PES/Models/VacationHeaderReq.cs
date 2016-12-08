using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Request title is required")]
        public string title { get; set; }

        /// <summary>
        /// Number of vacation days
        /// </summary>
        public int noVacDays { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        [Required(ErrorMessage = "Please submit a comment")]
        public string comments { get; set; }

        /// <summary>
        /// Request status Id
        /// </summary>
        public int ReqStatusId { get; set; }

        /// <summary>
        /// Replay Comment
        /// </summary>
        [Required(ErrorMessage = "A cancel reason is required")]
        public string replayComment { get; set; }

        /// <summary>
        /// Number of unpaid days
        /// </summary>
        public int? noUnpaidDays { get; set; }

    }
    
}