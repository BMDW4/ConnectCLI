using System.ComponentModel.DataAnnotations;

namespace WmdaConnect.Models.IdTypes
{
    public class PatientId
    {
        /// <summary>
        /// Patient identification P_ID Opt 17
        /// </summary>
        [Required]
        [MaxLength(17)]
        public string Id { get; set; }
    }
}