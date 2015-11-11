using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Models
{
    /// <summary>
    /// This class will store a title
    /// </summary>
    public class Title
    {
        /// <summary>
        /// Title Id
        /// </summary>  
        public int TitleId { get; set; }

        /// <summary>
        /// Title name
        /// </summary>
        public string Title { get; set; }
    }
}