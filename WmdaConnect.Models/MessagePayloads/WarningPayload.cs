using System;
using System.ComponentModel.DataAnnotations;
using WmdaConnect.Models.FieldDictionary;
using WmdaConnect.Models.IdTypes;

namespace WmdaConnect.Models.MessagePayloads
{
    public class WarningPayload : IHasPatient, IHasDonor, IHasRemark
    {
        /// <summary>
        /// Code operation of original message MSG_CODE Req 10
        /// </summary>
        [Required]
        [MaxLength(MessageCodeField.MaxLength)]
        public string MessageCode { get; set; }

        /// <inheritdoc/>
        public PatientId Patient { get; set; }

        /// <inheritdoc/>
        public DonorSelection Donor {  get; set; }

        /// <summary>
        /// Reference code REF_CODE Opt 15
        /// </summary>
        [MaxLength(ReferenceCodeField.MaxLength)]
        public string ReferenceCode { get; set; }
        
        /// <summary>
        /// Origin of denial ORG_DEN Req 20
        /// </summary>
        [Required]
        [MaxLength(OriginOfDenialField.MaxLength)]
        public string OriginOfDenial { get; set; }

        /// <summary>
        /// Remark(Explanation for the warning) REMARK Req 120
        /// </summary>
        [Required]
        [MaxLength(RemarkField.MaxLength)]
        public string Remark { get; set; }
    }
}