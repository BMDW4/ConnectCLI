using System;
using System.ComponentModel.DataAnnotations;
using WmdaConnect.Models.FieldDictionary;
using WmdaConnect.Models.IdTypes;

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
        /// Remark REMARK Opt 120
        /// </summary>
        [MaxLength(RemarkField.MaxLength)]
        public string Remark { get; set; }
    }
}