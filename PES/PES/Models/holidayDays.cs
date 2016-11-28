using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Models
{
    /// <summary>
    /// This clas will store holiday days
    /// </summary>
    public class HolidayDays
    { 
        /// <summary>
        /// Holiday Id
        /// </summary>
        public int holidayId { get; set; }

        /// <summary>
        /// Holiday day
        /// </summary>
        public DateTime holidayDay { get; set; }
    }
}