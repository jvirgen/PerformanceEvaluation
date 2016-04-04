using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PES.ViewModels
{
    public class ChangeProfileViewModel : InsertEmployeeViewModel
    {
        [Display(Name = "Transfer all to ")]
        public int NewManager { get; set; }
    }
}