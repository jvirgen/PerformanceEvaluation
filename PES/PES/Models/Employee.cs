using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PES.Models
{
    /// <summary>
    /// This class will store a Employee
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Employee Id
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        /// 
        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        /// 
        [Required]
        
        public string Email { get; set; }

        /// <summary>
        /// Employee Client 
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// Position
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Employee Profile Id
        /// </summary>
        /// 
        [Required(ErrorMessage = "Porfile is requiered")]
        [Display(Name = "Porfile")]
        public int ProfileId { get; set; }

        /// <summary>
        /// Manager Id
        /// </summary>
        /// 
        [Required(ErrorMessage = "Manager is required")]
        [Display(Name = "Manager")]
        public int ManagerId { get; set;}

        /// <summary>
        /// Hire Date
        /// </summary>
        //[DataType(DataType.Date)]
        //[Display(Name = "Hire Date")]
        //public DateTime HireDate { get; set; }

        ///// <summary>
        ///// Employee Ranking
        ///// </summary>
        //public int Ranking { get; set; }

        /// <summary>
        /// End Date 
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }
        //public string EndDate { get; set; }

        /// <summary>
        /// Employee project 
        /// </summary>
        public string Project { get; set; }

        ///<summary>
        ///Employee Location
        /// </summary>
        public int LocationId { get; set; }

        public int Freedays { get; set; }
        

    }

    public class EmployeeManagerViewModel 
    {
        public Employee employee { get; set; }
        public Employee manager { get; set; }
        public PEs lastPe { get; set; }
        public Location location { get; set; }
        //public double totalScore { get; set; }
        //public double? rank { get; set; }
        //public double englishScore { get; set; }
    }

    public class EmployeeChoosePeriodViewModel 
    {
        public int employeeid { get; set; }
        public int pesid { get; set; }
        public DateTime period { get; set; }
        public double totalEvaluation { get; set; }
        public double totalPerforformance { get; set; }
        public double totalCompetences { get; set; }
        public double totalEnglish { get; set; }
        public int evaluationYear { get; set; }
        public Period periodName { get; set; }
    }

    public enum ProfileUser
    {
        None = 0,
        Resource = 1,
        Manager = 2,
        Director = 3
    }
}