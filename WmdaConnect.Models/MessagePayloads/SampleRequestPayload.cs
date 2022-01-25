using System;
using System.ComponentModel.DataAnnotations;
using WmdaConnect.Models.FieldDictionary;
using WmdaConnect.Models.IdTypes;
using WmdaConnect.Models.OtherTypes;
using WmdaConnect.Models.Shared;

namespace WmdaConnect.Models.MessagePayloads
{
    public class SampleRequestPayload: IHasRequestDate, IHasRemark
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
        /// Request date REQ_DATE Req 8
        /// </summary>
        [Required]
        [Range(typeof(DateTime), "02-Jan-0001", "31-Dec-9999", ErrorMessage = nameof(RequestDate) + " expects yyyy-MM-dd [or yyyyMMdd]")]
        public DateTime RequestDate { get; set; }

        /// <summary>
        /// EMDIS format of Request date REQ_DATE Req 8 yyyy-MM-dd [or yyyyMMdd]
        /// </summary>
        public string RequestDateEmdis => RequestDate.ToEmdis();

        [Required]
        public Prod Prod1 { get; set; }

        public Prod Prod2 { get; set; }


        public Prod Prod3 { get; set; }

        public Prod Prod4 { get; set; }

        /// <summary>
        /// Earliest date of sample reception REC_DATE1 Req 8 yyyy-MM-dd [or yyyyMMdd]
        /// </summary>
        [Required]
        [Range(typeof(DateTime), "02-Jan-0001", "31-Dec-9999", ErrorMessage = nameof(ReceptionDate1) + " expects yyyy-MM-dd [or yyyyMMdd]")]
        public DateTime ReceptionDate1 { get; set; }

        /// <summary>
        /// EMDIS format of Earliest date of sample reception REC_DATE1 Req 8 
        /// </summary>
        public string ReceptionDate1Emdis => ReceptionDate1.ToEmdis();

        /// <summary>
        /// Latest date of sample reception REC_DATE2 Opt 8 yyyy-MM-dd [or yyyyMMdd]
        /// </summary>
        public DateTime? ReceptionDate2 { get; set; }

        /// <summary>
        /// EMDIS format of Latest date of sample reception REC_DATE2 Opt 8
        /// </summary>
        public string ReceptionDate2Emdis => ReceptionDate2?.ToEmdis();

        /// <summary>
        /// Weekdays acceptable for reception ACC_DAYS Opt 7
        /// </summary>
        public string AcceptableReceptionDaysOfWeek { get; set; }

        /// <summary>
        /// Institution the sample has to be sent to INST_SMP_SENT Req 10
        /// </summary>
        [Required]
        [MaxLength(14)]
        public string InstitutionToSendSampleTo { get; set; }

        /// <summary>
        /// Institution paying INST_PAY Req 10
        /// </summary>
        [Required]
        [MaxLength(InstitutionPayingField.MaxLength)]
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