using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PES.Models;

namespace PES.ViewModels
{
    public class SendRequestViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int EmployeedID { get; set; }

        public string Title { get; set; }

        public int daysReq { get; set; }

        public int VacationDays { get; set; }

        public  IEnumerable<Status> Status { get; set; }

        public string Comments { get; set; }

        public List<SubrequestInfoVM> SubRequests { get; set; }

        public VacHeadReqViewModel VacHeaderRequest { get; set; }

       public HttpPostedFileBase MyFile { get; set; }
     
        public int TypeRequest { get; set; }

        public enum RequestType
        {
            Normal = 0,
            IsUnpaid = 1,
            UnEmergency = 2,
            Paternity = 3,
            Funeral = 4
        }


        public enum MyEnum
        {
            [Display(Name = "First Value - desc..")]
            FirstValue,
            [Display(Name = "Second Value - desc...")]
            SecondValue
        }


    }
}