using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using WmdaConnect.Models.FieldDictionary;
using WmdaConnect.Models.IdTypes;
using WmdaConnect.Models.OtherTypes;

namespace WmdaConnect.Models.MessagePayloads 
{
    public class InfectiousDiseaseMarkerResultPayload : IHasPatient, IHasDonor, IHasReferenceCode, IHasMarker, IHasRemark
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
        /// Infectious disease markers as requested (IDM_REQ) / Infectious disease markers as performed SMP_REQ) MARKER Req 19
        /// </summary>
        [MinLength(MarkerField.MinLength)]
        [MaxLength(MarkerField.MaxLength)]
        public string Marker { get; set; }

        /// <summary>
        /// Date of sample extraction D_EXTR_DATE Opt 8
        /// </summary>
        public DateTime? SampleExtractionDate { get; set; }

        /// <summary>
        /// Donor blood group D_ABO Opt 2
        /// </summary>
        [MaxLength(2)]
        public string DonorBloodGroup { get; set; }

        /// <summary>
        /// Donor rhesus3 D_RHESUS Opt 1
        /// </summary>
        [MaxLength(1)]
        public string DonorRhesus { get; set; }

        /// <summary>
        /// Donor CCR5 status D_CCR5 Opt 2
        /// </summary>
        [MaxLength(2)]
        public string DonorCcr5Status { get; set; }

        /// <summary>
        /// Donor weight D_WEIGHT Opt 3
        /// </summary>
        [MaxLength(3)]
        public string DonorWeight { get; set; }

        /// <summary>
        /// Donor height D_HEIGHT Opt 3
        /// </summary>
        [MaxLength(3)]
        public string DonorHeight { get; set; }

        /// <summary>
        /// Number of transfusions D_NMBR_TRANS Opt 1
        /// </summary>
        [MaxLength(1)]
        public string NumberOfTransfusions { get; set; }

        /// <summary>
        /// Number of pregnancies D_NMBR_PREG Opt 1
        /// </summary>
        [MaxLength(1)]
        public string NumberOfPregnancies { get; set; }

        [JsonProperty("idm")]
        public InfectiousDiseaseMarkers InfectiousDiseaseMarkers { get; set; }

        /// <summary>
        /// Remark REMARK Opt 120
        /// </summary>
        [MaxLength(RemarkField.MaxLength)]
        public string Remark { get; set; }
    }
}
