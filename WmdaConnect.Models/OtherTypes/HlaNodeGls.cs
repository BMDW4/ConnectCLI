using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WmdaConnect.Models.OtherTypes
{
    /// <summary>
    /// HLA Node with GLS 
    /// </summary>
    public class HlaNodeGls
    {
        /// <summary>
        /// allele list strings Opt 255
        /// </summary>
        [Required]
        public string Gls { get; set; }
    }


}
