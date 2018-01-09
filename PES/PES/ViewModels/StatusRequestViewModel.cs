using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.ViewModels
{    
    public class StatusRequestViewModel   /* : VacHeadReqViewModel*/
    {
        public int HeaderRequestId { get; set; }

        public string currentStatusId { get; set; }

        /// <summary>
        /// Replay comment or reason of cancelation
        /// </summary>
        public string Reason { get; set; }

        public string EmployeeEmail { get; set; }

        public string ManagerEmail { get; set; }

        public string NoVacDays { get; set; }

        public VacHeadReqViewModel VacHedModel { get; set;  }
    }
}