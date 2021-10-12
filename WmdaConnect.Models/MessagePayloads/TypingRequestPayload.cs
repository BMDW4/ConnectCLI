using System;
using System.ComponentModel.DataAnnotations;
using WmdaConnect.Models.FieldDictionary;
using WmdaConnect.Models.IdTypes;
using WmdaConnect.Models.Shared;

namespace WmdaConnect.Models.MessagePayloads
{
    public class TypingRequestPayload : IHasRequestDate, IHasRemark
    {
        [Required]
        public PatientId Patient { get; set; }
        
        [Required]
        public DonorSelection Donor { get; set; }

        /// <summary>
        /// Request date REQ_DATE Req 8 yyyy-MM-dd [or yyyyMMdd]
        /// </summary>
        [Required]
        [Range(typeof(DateTime), "02-Jan-0001", "31-Dec-9999", ErrorMessage = "Required, yyyy-MM-dd [or yyyyMMdd]")]
        public DateTime RequestDate { get; set; }

        /// <summary>
        /// EMDIS format of Request date REQ_DATE Req 8
        /// </summary>
        public string RequestDateEmdis => RequestDate.ToEmdis();

        /// <summary>
        /// Reference code REF_CODE Req 15
        /// </summary>
        [Required]
        [MaxLength(ReferenceCodeField.MaxLength)]
        public string RefCode { get; set; }

        /// <summary>
        /// Resolution required (see below) RESOLUT Req 11
        /// </summary>
        [Required]
        [MinLength(11)]
        [MaxLength(11)]
        public string ResolutionRequired { get; set; }

        /// <summary>
        /// Institution paying INST_PAY Req 10
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string InstitutionPaying { get; set; }

        /// <summary>
        /// Urgent request URGENT Opt 1
        /// </summary>
        public bool Urgent { get; set; }

        /// <summary>
        /// Acknowledgement ID ACK_ID Opt 17
        /// </summary>
        public string AcknowledgementId { get; set; }

        /// <summary>
        /// Remark REMARK Opt 120
        /// </summary>
        public string Remark { get; set; }
    }
}