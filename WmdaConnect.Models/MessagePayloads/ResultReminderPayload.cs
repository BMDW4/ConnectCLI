using System;
using System.ComponentModel.DataAnnotations;
using WmdaConnect.Models.FieldDictionary;
using WmdaConnect.Models.IdTypes;

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
        /// Remark REMARK Opt 120
        /// </summary>
        [MaxLength(RemarkField.MaxLength)]
        public string Remark { get; set; }
    }
}