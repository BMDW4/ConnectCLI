using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WmdaConnect.Models.FieldDictionary;
using WmdaConnect.Models.IdTypes;

namespace WmdaConnect.Models.MessagePayloads
{
    public class CordBloodUnitReportResponsePayload : IHasPatient, IHasCordBloodUnitId, IHasReferenceCode, IHasAttachmentGuids
    {
        [Required] public PatientId Patient { get; set; }

        [Required] public CordBloodUnitId CordBloodUnit { get; set; }

        /// <summary>
        /// Reference code REF_CODE Req 15
        /// </summary>
        [Required]
        [MaxLength(ReferenceCodeField.MaxLength)]
        public string ReferenceCode { get; set; }


        /// <summary>
        /// Attachment guids
        /// </summary>
        public List<Guid> AttachmentGuids { get; set; }
    }
}
