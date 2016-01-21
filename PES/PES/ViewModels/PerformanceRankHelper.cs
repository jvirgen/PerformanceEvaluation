using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.ViewModels
{
    [Serializable]
    public class PerformanceRankHelper
    {
        public int performanceId { get; set; }
        public double rankValue { get; set; }
    }
}