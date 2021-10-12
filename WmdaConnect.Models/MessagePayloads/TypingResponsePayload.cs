using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WmdaConnect.Models.FieldDictionary;
using WmdaConnect.Models.IdTypes;
using WmdaConnect.Models.OtherTypes;
using WmdaConnect.Models.Shared;

namespace WmdaConnect.Models.MessagePayloads
{
    public class TypingResponsePayload : IHasPatient, IHasReferenceCode, IHasRemark
    {
        [Required]
        public PatientId Patient { get; set; }

        /// <summary>
        /// Reference code REF_CODE Req 15
        /// </summary>
        [Required]
        [MaxLength(ReferenceCodeField.MaxLength)]
        public string ReferenceCode { get; set; }

        /// <summary>
        /// Major version of HLA Nomenclature HLA_NOM_VER Req 7
        /// </summary>
        [Required]
        [DefaultValue("3")]
        public string HlaNomVer { get; set; }

        public DonorSelectionPlusSampleType Donor { get; set; }

        /// <summary>
        /// Donor date of birth D_BIRTH_DATE Opt 8 yyyy-MM-dd [or yyyyMMdd], //MISSING FROM SMP_RES
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// EMDIS format of Donor date of birth D_BIRTH_DATE Opt 8, //MISSING FROM SMP_RES
        /// </summary>
        public string DateOfBirthEmdis => DateOfBirth?.ToEmdis();

        /// <summary>
        /// Donor Sex D_SEX Opt 1", //MISSING FROM SMP_RES
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// Donor CMV status D_CMV Opt 1
        /// </summary>
        public string Cmv { get; set; }

        /// <summary>
        /// Date of CMV test D_CMV_DATE Opt 8 yyyy-MM-dd [or yyyyMMdd]
        /// </summary>
        public DateTime? CmvDate { get; set; }

        /// <summary>
        /// EMDIS format of Date of CMV test D_CMV_DATE Opt 8
        /// </summary>
        public string CmvDateEmdis => CmvDate?.ToEmdis();


        public Hla Hla { get; set; }

        /// <summary>
        /// Remark REMARK Opt 120
        /// </summary>
        public string Remark { get; set; }
    }
}