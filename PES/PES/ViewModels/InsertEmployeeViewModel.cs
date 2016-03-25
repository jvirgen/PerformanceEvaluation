using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;
using System.Web.Mvc;

namespace PES.ViewModels
{
    public class InsertEmployeeViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Customer { get; set; }
        public string Email { get; set; }
        public List<SelectListItem> ListProfiles { get; set; }
        public int SelectedProfile { get; set; }
        public DateTime HireDate { get; set; }
        public string Position { get; set; }
        public string Project { get; set; }
        public List<SelectListItem> ListManagers { get; set; }
        public int SelectedManager { get; set; }
    }
}