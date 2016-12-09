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
        public int EmployeeId { get; set; }

        /// <summary>
        /// Title of the request
        /// </summary>
        [Required(ErrorMessage = "Request title is required")]
        public string Title { get; set; }

        /// <summary>
        /// Number of vacation days
        /// </summary>
        public int NoVacDays { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        [Required(ErrorMessage = "Please submit a comment")]
        public string Comments { get; set; }

        /// <summary>
        /// Number of unpaid days
        /// </summary>
        public int? NoUnpaidDays { get; set; }

        public IEnumerable<NewVacationDates> SubRequest { get; set; }

        public int Freedays {get; set; }


    }
}