using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WmdaConnect.Models.OtherTypes
{
    /// <summary>
    /// HLA Node with DNA 
    /// </summary>
    public class HlaNodeDna
    {

        [Required]
        public Dna dna { get; set; }
    }


}
