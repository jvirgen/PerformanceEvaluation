using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Models
{
    /// <summary>
    /// This class will store a Performance Evaluation
    /// </summary>
    public class PEs
    {
        /// <summary>
        /// Performance Evaluation Id
        /// </summary>
        public int PEId { get; set; }

        /// <summary>
        /// Performance Evaluation period
        /// </summary>
        public DateTime EvaluationPeriod { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Evaluator Id
        /// </summary>
        public int EvaluatorId { get; set; }

        /// <summary>
        /// PE Status Id
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// Employee total score
        /// </summary>
        public double Total { get; set; }

        /// <summary>
        /// English Score
        /// </summary>
        public double EnglishScore { get; set; }

        /// <summary>
        /// English Score
        /// </summary>
        public double PerformanceScore { get; set; }

        /// <summary>
        /// English Score
        /// </summary>
        public double CompeteneceScore { get; set; }
        
    }

    public class UploadFileViewModel 
    {
        public Employee CurrentUser { get; set; }
        public List<Employee> ListEmployees { get; set; }
        public int SelectedEmployee { get; set; }
    }
}