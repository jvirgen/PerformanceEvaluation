using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PES.ViewModels
{
    public class ChangeProfileViewModel
    {
        //public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public List<SelectListItem> ListProfiles { get; set; }
        public int SelectedProfile { get; set; }

        public List<SelectListItem> ListManagers { get; set; }
        public int SelectedManager { get; set; }
    }
}