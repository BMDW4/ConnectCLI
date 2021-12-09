using System.ComponentModel.DataAnnotations;

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
