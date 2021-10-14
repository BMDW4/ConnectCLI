using System.ComponentModel.DataAnnotations;

namespace WmdaConnect.Models.IdTypes
{
    public class CordBloodUnitIdPlusSampleType
    {
        /// <summary>
        /// CBU Identification2 CB_ID Opt 17
        /// </summary>
        [Required]
        [MaxLength(17)]
        public string Id { get; set; }

        /// <summary>
        /// Type of sample CB_SAMPLE_TYPE Opt 2
        /// </summary>
        public string SampleType { get; set; }

    }
}