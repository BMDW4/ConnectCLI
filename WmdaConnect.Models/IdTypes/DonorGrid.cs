using System.ComponentModel.DataAnnotations;

namespace WmdaConnect.Models.IdTypes
{
    public class DonorGrid
    {
        /// <summary>
        /// Global registration identifier for donor D_GRID Opt 19
        /// </summary>
        [Required]
        [MinLength(19)]
        [MaxLength(19)]
        public string Grid { get; set; }
    }
}