using System;
using System.ComponentModel.DataAnnotations;
using WmdaConnect.Models.FieldDictionary;
using WmdaConnect.Models.IdTypes;
using WmdaConnect.Models.Shared;

namespace WmdaConnect.Models.MessagePayloads
{
    public class InfectiousDiseaseMarkerRequestPayload : IHasPatient, IHasDonor, IHasRequestDate, IHasReferenceCode, IHasMarker, IHasInstitutionPaying, IHasAcknowledgementId, IHasRemark
    {
        public PatientId Patient { get; set; }
        public DonorSelection Donor { get; set; }

        /// <summary>
        /// Request date REQ_DATE Req 8 yyyy-MM-dd [or yyyyMMdd]
        /// </summary>
        [Required]
        [Range(typeof(DateTime), "02-Jan-0001", "31-Dec-9999", ErrorMessage = nameof(RequestDate) + " expects yyyy-MM-dd [or yyyyMMdd]")]
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
        /// Infectious disease markers as requested (IDM_REQ) / Infectious disease markers as performed SMP_REQ) MARKER Req 19
        /// </summary>
        [Required]
        [MinLength(MarkerField.MinLength)]
        [MaxLength(MarkerField.MaxLength)]
        public string Marker { get; set; }

        /// <summary>
        /// Institution paying INST_PAY Req 10
        /// </summary>
        [Required]
        [MaxLength(InstitutionPayingField.MaxLength)]
        public string InstitutionPaying { get; set; }

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
