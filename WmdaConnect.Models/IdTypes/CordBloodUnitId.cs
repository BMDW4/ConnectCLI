using System.ComponentModel.DataAnnotations;

namespace WmdaConnect.Models.IdTypes
{
    public class CordBloodUnitId
    {
        /// <summary>
        /// CBU Identification2 CB_ID Opt 17
        /// </summary>
        [Required]
        [MaxLength(17)]
        public string Id { get; set; }


    }
}