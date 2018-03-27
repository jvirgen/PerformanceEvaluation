using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;
using System.Web.DynamicData;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PES.ViewModels
{
    /// <summary>
    /// Class to sotre all Data of a New Request
    /// </summary>
    public class InsertNewRequestViewModel
    {
        /// <summary>
        /// Employee Id
        /// </summary>  
        public int EmployeeId { get; set; }

        public string EmployeeEmail { get; set; }

        public string ManagerEmail { get; set; }
        /// <summary>
        /// Title of the request
        /// </summary>
        [Required(ErrorMessage = "Request Title is required")]
        public string Title { get; set; }

        /// <summary>
        /// Number of days requested 
        /// </summary>
        public int daysReq { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        [Required(ErrorMessage = "Please submit a comment")]
        public string Comments { get; set; }

        /// <summary>
        /// Number of unpaid days
        /// </summary>
        public int? NoUnpaidDays { get; set; }

        /// <summary>
        /// List of New vacation date Viewmodel 
        /// </summary>
        public List<NewVacationDates> SubRequest { get; set; }

        /// <summary>
        /// Number of vacation days that employee have 
        /// </summary>
        public IEnumerable<SelectListItem> ListEmployee { get; set; }
        //public List<Employee> ListEmployee { get; set; }

        

        public int Freedays {get; set; }

        //public string myFile { get; set;  }

        public VacHeadReqViewModel VacHeaderRequest { get; set;  }

        //[DataType(DataType.Upload)]
        //HttpPostedFileBase myFile { get; set; }
    }
 
}