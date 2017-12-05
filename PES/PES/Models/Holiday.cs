using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PES.Models
{
    /// <summary>
    /// This clas will store holiday days
    /// </summary>
    public class Holiday
    { 
        /// <summary>
        /// Holiday Id
        /// </summary>
        public int HolidayId { get; set; }

        /// <summary>
        /// Holiday day
        /// </summary>
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Day { get; set; }


        /// <summary>
        ///Description 
        /// </summary>
        public string Description { get; set; }
    }
}