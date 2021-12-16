using System;
using System.ComponentModel.DataAnnotations;
using WmdaConnect.Models.FieldDictionary;
using WmdaConnect.Models.IdTypes;
using WmdaConnect.Models.Shared;

namespace WmdaConnect.Models.MessagePayloads
{
    public class ResultReminderPayload : IHasPatient, IHasDonor, IHasRemark, IHasExpirationDate
    {
        /// <inheritdoc/>
        public PatientId Patient { get; set; }

        /// <inheritdoc/>
        public DonorSelection Donor {  get; set; }

        /// <summary>
        /// Request date REQ_DATE Opt 8 yyyy-MM-dd [or yyyyMMdd]
        /// </summary>
        public DateTime? RequestDate { get; set; }

        /// <summary>
        /// EMDIS format date of Request Date REQ_DATE Req 8 yyyy-MM-dd [or yyyyMMdd]
        /// </summary>
        public string RequestDateEmdis => RequestDate?.ToEmdis();

        /// <summary>
        /// Reference code REF_CODE Req 15
        /// </summary>
        [Required]
        [MaxLength(ReferenceCodeField.MaxLength)]
        public string ReferenceCode { get; set; }

        /// <summary>
        /// Type of result reminded RES_TYPE Req 9
        /// </summary>
        [Required]
        [MaxLength(9)]
        public string ResultType { get; set; }

        /// <summary>
        /// Request date EXPI_DATE Opt 8 yyyy-MM-dd [or yyyyMMdd]
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// EMDIS format date of Expiration Date EXPI_DATE_DATE Req 8 yyyy-MM-dd [or yyyyMMdd]
        /// </summary>
        public string ExpirationDateEmdis => ExpirationDate?.ToEmdis();

        /// <summary>
        /// Remark REMARK Opt 120
        /// </summary>
        [MaxLength(RemarkField.MaxLength)]
        public string Remark { get; set; }
    }
}