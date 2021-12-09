using System.ComponentModel.DataAnnotations;

namespace WmdaConnect.Models.OtherTypes
{

    public class Prod
    {


        /// <summary>
        /// Product required Req 10
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string Product { get; set; }

        /// <summary>
        /// Product quantity per tube Opt 4
        /// </summary>
        [Required]
        public int Quantity { get; set; }

        /// <summary>
        /// Number of tubes for the product Opt 2
        /// </summary>
        [Required]
        public int NumberOfTubes { get; set; }
    }

}
