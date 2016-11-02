using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PES.ViewModels
{
    [Serializable]
    public class PerformanceRankHelper
    {
        public int performanceId { get; set; }
        [Range(0.00, 1.00, ErrorMessage = "Rank value should be between 0 and 1")]
        public double rankValue { get; set; }
    }
}