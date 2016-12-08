using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;
using System.ComponentModel.DataAnnotations;

namespace PES.ViewModels
{
    public class InsertNewRequestViewModel
    {
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
        /// Number of unpaid days
        /// </summary>
        public int? noUnpaidDays { get; set; }

        public IEnumerable<NewVacationDates> subRequest { get; set; }

        public int freedays {get; set; }


    }
}