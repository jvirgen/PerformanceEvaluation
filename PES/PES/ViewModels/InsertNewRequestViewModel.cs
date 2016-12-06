using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;

namespace PES.ViewModels
{
    public class InsertNewRequestViewModel : VacationHeaderReq
    {
        public List<VacationSubreq> subReqList { get; set; }
        public int freedays {get; set; }
        public string status { get; set; }
    }
}