using System;
using System.ComponentModel.DataAnnotations;
using WmdaConnect.Models.FieldDictionary;
using WmdaConnect.Models.IdTypes;
using WmdaConnect.Models.Shared;

namespace WmdaConnect.Models.MessagePayloads
{
    public class SampleArrivalPayload : IHasReferenceCode, IHasPatient, IHasRemark
    {
        [Required]
        public PatientId Patient { get; set; }

        [Required]
        public DonorSelectionPlusSampleType Donor { get; set; }

        /// <summary>
        /// Reference code REF_CODE Req 15
        /// </summary>
        [Required]
        [MaxLength(ReferenceCodeField.MaxLength)]
        public string ReferenceCode { get; set; }

        /// <summary>
        /// Proposed date of sample arrival ARRV_DATE Req 8
        /// </summary>
        [Required]
        [Range(typeof(DateTime), "02-Jan-0001", "31-Dec-9999", ErrorMessage = nameof(ArrivalDate) + " expects yyyy-MM-dd [or yyyyMMdd]")]
        public DateTime ArrivalDate { get; set; }

        /// <summary>
        /// EMDIS format date of sample arrival ARRV_DATE Req 8 yyyy-MM-dd [or yyyyMMdd]
        /// </summary>
        public string ArrivalDateEmdis => ArrivalDate.ToEmdis();


        /// <summary>
        /// Donor blood collection date COLL_DATE Opt 8
        /// </summary>
        public DateTime? CollectionDate { get; set; }

        /// <summary>
        /// EMDIS format date of Donor blood collection date COLL_DATE Opt 8 yyyy-MM-dd [or yyyyMMdd]
        /// </summary>
        public string CollectionDateEmdis => CollectionDate?.ToEmdis();


        /// <summary>
        /// Acknowledgement ID ACK_ID Opt 17
        /// </summary>
        public string AcknowledgementId { get; set; }

        /// <summary>
        /// Verbatim verification typing (VT) sample label ID D_LABEL_ID Req 19
        /// </summary>
        [Required]
        [MaxLength(LabelIdField.MaxLength)]
        public string LabelId { get; set; }

        /// <summary>
        /// Remark REMARK Opt 120
        /// </summary>
        public string Remark { get; set; }
    }
}