using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using WmdaConnect.Models.FieldDictionary;
using WmdaConnect.Models.IdTypes;

namespace WmdaConnect.Models.MessagePayloads
{
    public class CordBloodUnitReportRequestPayload : IHasPatient, IHasCordBloodUnitId, IHasReferenceCode,
        IHasEmailAddress, IHasFaxNumber, IHasAcknowledgementId
    {
        [Required] public PatientId Patient { get; set; }

        [JsonProperty("cbu")]
        [Required] public CordBloodUnitId CordBloodUnit { get; set; }

        /// <summary>
        /// Reference code REF_CODE Req 15
        /// </summary>
        [Required]
        [MaxLength(ReferenceCodeField.MaxLength)]
        public string ReferenceCode { get; set; }

        /// <summary>
        /// Email address EMAIL Req 60
        /// </summary>
        [Required]
        [MaxLength(EmailAddressField.MaxLength)]
        [EmailAddress]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Fax Number FAX Req 20
        /// </summary>
        [Required]
        [MaxLength(FaxNumberField.MaxLength)]
        public string FaxNumber { get; set; }

        /// <summary>
        /// Preferred sending method SEND_PREF Opt 1
        /// </summary>
        [MaxLength(PreferredSendingMethodField.MaxLength)]
        public string PreferredSendingMethod { get; set; }

        /// <summary>
        /// Acknowledgement ID ACK_ID Opt 17
        /// </summary>
        [Required]
        [MaxLength(AcknowledgementIdField.MaxLength)]
        public string AcknowledgementId { get; set; }

    }
}
