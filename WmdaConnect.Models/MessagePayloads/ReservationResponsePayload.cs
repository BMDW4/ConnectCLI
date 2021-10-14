using System;
using System.ComponentModel.DataAnnotations;
using WmdaConnect.Models.FieldDictionary;
using WmdaConnect.Models.IdTypes;

namespace WmdaConnect.Models.MessagePayloads
{
    public class ReservationResultPayload : IHasPatient, IHasDonor, IHasReferenceCode, IHasRemark, IHasExpirationDate, IHasConfirmed
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
        /// Date expiration of reservation EXPI_DATE Opt 8 yyyy-MM-dd [or yyyyMMdd]
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Confirmation of reservation CONFIRM Req 1
        /// </summary>
        [Required]
        public bool Confirmed { get; set; }

        /// <summary>
        /// Remark REMARK Opt 120
        /// </summary>
        [MaxLength(RemarkField.MaxLength)]
        public string Remark { get; set; }
    }
}