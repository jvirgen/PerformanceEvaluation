using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;

namespace PES.ViewModels
{
    public class TransferEmployeeViewModel
    {
        public List<Profile> ProfilesList { get; set; }
        public List<Employee> ManagerAList { get; set; }
        public Employee SelectedManagerA { get; set; }
        public List<Employee> ManagerBList { get; set; }
        public Employee SelectedManagerB { get; set; }
        public List<Employee> ManagerAEmplyeeList { get; set; }
        public List<Employee> ManagerBEmployeeList { get; set; }
    }
}