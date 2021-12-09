using System.ComponentModel.DataAnnotations;

namespace WmdaConnect.Models.OtherTypes
{
    /// <summary>
    /// DNA
    /// </summary>
    public class Dna
    {
        /// <summary>
        /// 1st allele Opt 20
        /// </summary>
        [Required]
        public string field1 { get; set; }

        /// <summary>
        /// 2nd allele Opt 20
        /// </summary>
        public string field2 { get; set; }
    }
}
