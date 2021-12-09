using System.ComponentModel.DataAnnotations;

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
