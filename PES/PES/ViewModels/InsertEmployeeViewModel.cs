using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PES.ViewModels
{
    public class InsertEmployeeViewModel
    {
        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Customer { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid")]
        [EmailAddress(ErrorMessage = "Please input an email address")]
        public string Email { get; set; }
        public List<SelectListItem> ListProfiles { get; set; }
        [Display(Name = "Profile")]
        public int SelectedProfile { get; set; }

        [Display(Name = "Hire Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/y}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }

        public string Position { get; set; }
        public string Project { get; set; }
        public List<SelectListItem> ListManagers { get; set; }
        [Display(Name = "Manager")]
        public int SelectedManager { get; set; }
    }
}