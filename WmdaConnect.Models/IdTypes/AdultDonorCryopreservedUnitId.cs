using System.ComponentModel.DataAnnotations;

namespace WmdaConnect.Models.IdTypes
{
    public class AdultDonorCryopreservedUnitId
    {
        /// <summary>
        /// N/A in EMDIS
        /// </summary>
        [Required]
        [MaxLength(13)]
        public string Id { get; set; }


    }
}