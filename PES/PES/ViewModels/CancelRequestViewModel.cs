using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.ViewModels
{    
    public class CancelRequestViewModel   /* : VacHeadReqViewModel*/
    {
        public int HeaderRequestId { get; set; }

        /// <summary>
        /// Replay comment or reason of cancelation
        /// </summary>
        public string ReasonCancellation { get; set; }

        public string EmployeeEmail { get; set; }

        public string ManagerEmail { get; set; }
    }
}