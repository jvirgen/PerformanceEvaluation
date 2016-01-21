using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;

namespace PES.ViewModels
{
    public class PerformanceFilesPartial
    {
        public IEnumerable<EmployeeManagerViewModel> listFiles { get; set; }
        public Employee currentUser { get; set; }
        public int? countRankUpdated { get; set; }

        public PerformanceFilesPartial(IEnumerable<EmployeeManagerViewModel> listFiles, Employee currentUser)
        {
            this.listFiles = listFiles;
            this.currentUser = currentUser;
            this.countRankUpdated = null;
        }
    }
}