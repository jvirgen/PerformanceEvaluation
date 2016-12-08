using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;


namespace PES.ViewModels
{
    public class VacHeadReqViewModel : VacationHeaderReq
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int freedays { get; set; }
        public string status { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public DateTime return_date { get; set; }
        public string lead_name { get; set; }
        public char have_project { get; set; }
    }
}