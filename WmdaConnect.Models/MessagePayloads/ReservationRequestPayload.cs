using System;
using System.ComponentModel.DataAnnotations;
using WmdaConnect.Models.FieldDictionary;
using WmdaConnect.Models.IdTypes;
using WmdaConnect.Models.Shared;

namespace WmdaConnect.Models.MessagePayloads
{
    public class ReservationRequestPayload : IHasPatient, IHasDonor, IHasRequestDate, IHasReferenceCode, IHasAcknowledgementId, IHasRemark, IHasExpirationDate
    {
        [Required]
        public PatientId Patient { get; set; }
        
        public DonorSelection Donor { get; set; }
        
        /// <summary>
        /// Request date REQ_DATE Req 8 yyyy-MM-dd [or yyyyMMdd]
        /// </summary>
        [Required]
        [Range(typeof(DateTime), "02-Jan-0001", "31-Dec-9999", ErrorMessage = "Required, yyyy-MM-dd [or yyyyMMdd]")]
        public DateTime RequestDate { get; set; }

        /// <summary>
        /// EMDIS format date of Request Date REQ_DATE Req 8 yyyy-MM-dd [or yyyyMMdd]
        /// </summary>
        public string RequestDateEmdis => RequestDate.ToEmdis();

        /// <summary>
        /// Reference code REF_CODE Req 15
        /// </summary>
        [Required]
        [MaxLength(ReferenceCodeField.MaxLength)]
        public string ReferenceCode { get; set; }

        /// <summary>
        /// Request date EXPI_DATE Opt 8 yyyy-MM-dd [or yyyyMMdd]
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// EMDIS format date of Expiration Date EXPI_DATE Req 8 yyyy-MM-dd [or yyyyMMdd]
        /// </summary>
        public string ExpirationDateEmdis => ExpirationDate?.ToEmdis();

        /// <summary>
        /// Acknowledgement ID ACK_ID Opt 17
        /// </summary>
        [MaxLength(AcknowledgementIdField.MaxLength)]
        public string AcknowledgementId { get; set; }

        /// <summary>
        /// Remark REMARK Opt 120
        /// </summary>
        [MaxLength(RemarkField.MaxLength)]
        public string Remark { get; set; }

    }
}
