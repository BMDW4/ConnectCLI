using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WmdaConnect.Models.OtherTypes
{
    /// <summary>
    /// Serological typing
    /// </summary>
    public class Ser
    {
        /// <summary>
        /// 1st antigen Opt 5
        /// </summary>
        [Required]
        public string field1 { get; set; }

        /// <summary>
        /// 2nd antigen Opt 5
        /// </summary>
        public string field2 { get; set; }
    }
}
