using System;
using System.ComponentModel.DataAnnotations;
using WmdaConnect.Models.FieldDictionary;
using WmdaConnect.Models.IdTypes;
using WmdaConnect.Models.Shared;

namespace WmdaConnect.Models.MessagePayloads
{
    public class MessageAcknowledgementPayload : IHasAcknowledgementId, IHasRemark
    {
        /// <summary>
        /// Acknowledgement ID ACK_ID Req 17
        /// </summary>
        [Required]
        [MaxLength(17)]
        public string AcknowledgementId { get; set; }

        /// <summary>
        /// Date of acknowledgment ACK_DATE Opt 8 yyyy-MM-dd [or yyyyMMdd]
        /// </summary>
        public DateTime? AcknowledgementDate { get; set; }

        /// <summary>
        /// EMDIS format date of Acknowledgement Date ACK_DATE Req 8 yyyy-MM-dd [or yyyyMMdd]
        /// </summary>
        public string AcknowledgementDateEmdis => AcknowledgementDate?.ToEmdis();

        /// <summary>
        /// Remark REMARK Opt 120
        /// </summary>
        [MaxLength(RemarkField.MaxLength)]
        public string Remark { get; set; }
    }
}