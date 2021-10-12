using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WmdaConnect.Models.OtherTypes
{
    /// <summary>
    /// HLA Node with Ser and DNA 
    /// </summary>
    public class HlaNodeSerDna
    {
        public Ser ser { get; set; }

        public Dna dna { get; set; }
    }


}
