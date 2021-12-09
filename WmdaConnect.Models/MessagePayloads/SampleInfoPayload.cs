using System.ComponentModel.DataAnnotations;
using WmdaConnect.Models.FieldDictionary;
using WmdaConnect.Models.IdTypes;

namespace WmdaConnect.Models.MessagePayloads
{
    public class SampleInfoPayload : IHasReferenceCode, IHasPatient, IHasRemark
    {
        [Required]
        public PatientId Patient { get; set; }

        [Required]
        public DonorSelection Donor { get; set; }

        /// <summary>
        /// Reference code REF_CODE Req 15
        /// </summary>
        [Required]
        [MaxLength(ReferenceCodeField.MaxLength)]
        public string ReferenceCode { get; set; }

        /// <summary>
        /// Information Type INFO_TYPE Req 3
        /// </summary>
        [Required]
        [MinLength(3)]
        [MaxLength(3)]
        public string InformationType { get; set; }

        /// <summary>
        /// Remark REMARK Opt 120
        /// </summary>
        public string Remark { get; set; }
    }
}