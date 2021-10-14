using System;
using System.ComponentModel.DataAnnotations;
using WmdaConnect.Models.FieldDictionary;
using WmdaConnect.Models.IdTypes;

namespace WmdaConnect.Models.MessagePayloads
{
    public class NoResultPayload : IHasPatient, IHasDonor, IHasRemark
    {
        /// <inheritdoc/>
        [Required]
        public PatientId Patient { get; set; }

        /// <inheritdoc/>
        [Required]
        public DonorSelection Donor {  get; set; }

        /// <summary>
        /// Reference code REF_CODE Req 15
        /// </summary>
        [Required]
        [MaxLength(ReferenceCodeField.MaxLength)]
        public string ReferenceCode { get; set; }

        /// <summary>
        /// Type of request REQ_TYPE Req 3
        /// </summary>
        [Required]
        [MaxLength(3)]
        public string RequestType { get; set; }

        /// <summary>
        /// Reason REASON Req 3
        /// </summary>
        [Required]
        [MinLength(3)]
        [MaxLength(3)]
        public string Reason { get; set; }

        /// <summary>
        /// Remark REMARK Opt 120
        /// </summary>
        [MaxLength(RemarkField.MaxLength)]
        public string Remark { get; set; }
    }
}