using System;
using System.ComponentModel.DataAnnotations;
using WmdaConnect.Models.FieldDictionary;
using WmdaConnect.Models.IdTypes;
using WmdaConnect.Models.OtherTypes;
using WmdaConnect.Models.Shared;

namespace WmdaConnect.Models.MessagePayloads
{
    public class SampleResponsePayload
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
        /// Major version of HLA Nomenclature HLA_NOM_VER Req 7
        /// </summary>
        [Required]
        [MaxLength(HlaNomVerField.MaxLength)]
        public string HlaNomVer { get; set; }


        public Hla Hla { get; set; }

        /// <summary>
        /// Result MLC, Graft vs. Host MLC_GVH Opt 1
        /// </summary>
        public string MlcGvh { get; set; }

        /// <summary>
        /// Result MLC, Host vs. Graft MLC_HVG Opt 1
        /// </summary>
        public string MlcHvg { get; set; }

        /// <summary>
        /// GvH reactivity GVH_REAC Opt 3
        /// </summary>
        public string GvhReac { get; set; }

        /// <summary>
        /// HvG reactivity HVG_REAC Opt 3
        /// </summary>
        public string HvgReac { get; set; }

        /// <summary>
        /// Donor blood group and rhesus D_ABO Opt 3, //PRESENT IN TYP_RES
        /// </summary>
        public string Abo { get; set; }

        /// <summary>
        /// Donor CMV status D_CMV Opt 1, //PRESENT IN TYP_RES
        /// </summary>
        public string Cmv { get; set; }

        /// <summary>
        /// Date of CMV test D_CMV_DATE Opt 8 yyyy-MM-dd [or yyyyMMdd], //PRESENT IN TYP_RES
        /// </summary>
        public DateTime? CmvDate { get; set; }

        /// <summary>
        /// EMDIS format of Date of CMV test D_CMV_DATE Opt 8, //PRESENT IN TYP_RES
        /// </summary>
        public string CmvDateEmdis => CmvDate?.ToEmdis();

        /// <summary>
        /// Donor Toxoplasmosis D_TOXO Opt 1
        /// </summary>
        public string Toxo { get; set; }

        /// <summary>
        /// Donor EBV status D_EBV Opt 1
        /// </summary>
        public string Ebv { get; set; }

        /// <summary>
        /// Donor HIV status D_HIV Opt 1
        /// </summary>
        public string Hiv { get; set; }

        /// <summary>
        /// Donor HIV p24 antigen D_HIV_P24 Opt 1
        /// </summary>
        public string HivP24 { get; set; }

        /// <summary>
        /// Donor Hepatitis B status D_HBS_AG Opt 1
        /// </summary>
        public string HbsAg { get; set; }

        /// <summary>
        /// Donor Hepatitis B status D_ANTI_HBS Opt 1
        /// </summary>
        public string AntiHbs { get; set; }

        /// <summary>
        /// Donor Hepatitis B status D_ANTI_HBC Opt 1
        /// </summary>
        public string AntiHbc { get; set; }

        /// <summary>
        /// Donor Hepatitis C status D_ANTI_HCV Opt 1
        /// </summary>
        public string AntiHcv { get; set; }

        /// <summary>
        /// Donor Lues status D_TPHA Opt 1
        /// </summary>
        public string Tpha { get; set; }

        /// <summary>
        /// Donor ALT status D_ALT Opt 3
        /// </summary>
        public string Alt { get; set; }

        /// <summary>
        /// Donor antibody to HTLV1.V2 D_ANTI_HTLV Opt 1
        /// </summary>
        public string AntiHtlv { get; set; }

        /// <summary>
        /// Donor still of interest DON_ACCPT Req 1
        /// </summary>
        [Required]
        [MaxLength(DonorAcceptField.MaxLength)]
        public string DonAccpt { get; set; }

    }
}