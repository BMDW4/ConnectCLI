using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WmdaConnect.Models.OtherTypes
{
    /// <summary>
    /// HLA Node with DNA 
    /// </summary>
    public class InfectiousDiseaseMarkers
    {
        /// <summary>
        /// Donor CMV antibody status D_ANTI_CMV Opt 1
        /// </summary>
        [MaxLength(1)]
        [JsonProperty("antiCmv")]
        public string DonorCmvAntibodyStatus { get; set; }

        /// <summary>
        /// Date of Donor CMV antibody test D_ANTI_CMV_DATE Opt 8
        /// </summary>
        [JsonProperty("antiCmvDate")]
        public DateTime? DonorCmvAntibodyTestDate { get; set; }

        /// <summary>
        /// Donor CMV NAT status D_CMV_NAT Opt 1
        /// </summary>
        [MaxLength(1)]
        [JsonProperty("cmvNat")]
        public string DonorCmvNatStatus { get; set; }

        /// <summary>
        /// Date of Donor CMV NAT test D_CMV_NAT_DATE Opt 8
        /// </summary>
        [JsonProperty("cmvNatDate")]
        public DateTime? DonorCmvNatTestDate { get; set; }

        /// <summary>
        /// Donor Hepatitis B status (hepatitis B surface antigen) D_HBS_AG Opt 1
        /// </summary>
        [MaxLength(1)]
        [JsonProperty("hbsAg")]
        public string DonorHepatitisBStatusSurfaceAntigen { get; set; }

        /// <summary>
        /// Donor Hepatitis B status (antibody to hepatitis B core antigen) D_ANTI_HBC Opt 1
        /// </summary>
        [MaxLength(1)]
        [JsonProperty("antiHbc")]
        public string DonorHepatitisBStatusAntibodyToCoreAntigen { get; set; }

        /// <summary>
        /// Donor Hepatitis B status (antibody to hepatitis B surface antigen) D_ANTI_HBS Opt 1
        /// </summary>
        [MaxLength(1)]
        [JsonProperty("antiHbs")]
        public string DonorHepatitisBStatusAntibodyToSurfaceAntigen { get; set; }

        /// <summary>
        /// Donor HBV NAT status D_HBV_NAT Opt 1
        /// </summary>
        [MaxLength(1)]
        [JsonProperty("hbvNat")]
        public string DonorHbvNatStatus { get; set; }

        /// <summary>
        /// Donor Hepatitis C status (antibody to hepatitis C virus) D_ANTI_HCV Opt 1
        /// </summary>
        [MaxLength(1)]
        [JsonProperty("antiHcv")]
        public string DonorHepatitisCStatusAntibodyToHepatitisCVirus { get; set; }

        /// <summary>
        /// Donor HCV NAT status D_HCV_NAT Opt 1
        /// </summary>
        [MaxLength(1)]
        [JsonProperty("hcvNat")]
        public string DonorHcvNatStatus { get; set; }

        /// <summary>
        /// Donor Hepatitis E status (antibody to hepatitis E virus) D_ANTI_HEV Opt 1
        /// </summary>
        [MaxLength(1)]
        [JsonProperty("antiHev")]
        public string DonorHepatitisEStatusAntibodyToHepatitisEVirus { get; set; }

        /// <summary>
        /// Donor HEV NAT status D_HEV_NAT Opt 1
        /// </summary>
        [MaxLength(1)]
        [JsonProperty("hevNat")]
        public string DonorHevNatStatus { get; set; }

        /// <summary>
        /// Donor HIV-1/2 antibody status D_ANTI_HIV_12 Opt 1
        /// </summary>
        [MaxLength(1)]
        [JsonProperty("antiHiv12")]
        public string DonorHiv12AntibodyStatus { get; set; }

        /// <summary>
        /// Donor HIV-1 NAT status D_HIV_1_NAT Opt 1
        /// </summary>
        [MaxLength(1)]
        [JsonProperty("hiv1Nat")]
        public string DonorHiv1NatStatus { get; set; }

        /// <summary>
        /// Donor HIV p24 antigen D_HIV_P24 Opt 1
        /// </summary>
        [MaxLength(1)]
        [JsonProperty("hivP24")]
        public string DonorHivP24Antigen { get; set; }

        /// <summary>
        /// Donor HTLV-I/II antibody status D_ANTI_HTLV Opt 1
        /// </summary>
        [MaxLength(1)]
        [JsonProperty("antiHTLV")]
        public string DonorHtlv12AntibodyStatus1 { get; set; }

        /// <summary>
        /// Donor Syphilis status (Treponema pallidum) D_SYPHILIS Opt 1
        /// </summary>
        [MaxLength(1)]
        [JsonProperty("syphilis")]
        public string DonorSyphilisStatus { get; set; }

        /// <summary>
        /// Donor West Nile Virus NAT status D_WNV_NAT Opt 1
        /// </summary>
        [MaxLength(1)]
        [JsonProperty("wnvNat")]
        public string DonorWestNileVirusNatStatus { get; set; }

        /// <summary>
        /// Donor Chagas antibody status D_ANTI_CHAGAS Opt 1
        /// </summary>
        [MaxLength(1)]
        [JsonProperty("antiChagas")]
        public string DonorChagasAntibodyStatus { get; set; }

        /// <summary>
        /// Donor Chagas NAT status D_CHAGAS_NAT Opt 1
        /// </summary>
        [MaxLength(1)]
        [JsonProperty("chagasNat")]
        public string DonorChagasNatStatus { get; set; }

        /// <summary>
        /// Donor EBV status D_EBV Opt 1
        /// </summary>
        [MaxLength(1)]
        [JsonProperty("ebv")]
        public string DonorEbvStatus { get; set; }

        /// <summary>
        /// Donor Toxoplasmosis status D_TOXO Opt 1
        /// </summary>
        [MaxLength(1)]
        [JsonProperty("toxo")]
        public string DonorToxoplasmosisStatus { get; set; }

        /// <summary>
        /// Donor ParvoB19 NAT status D_PB19_NAT Opt 1
        /// </summary>
        [MaxLength(1)]
        [JsonProperty("pb19Nat")]
        public string DonorParvoB19NatStatus { get; set; }

        /// <summary>
        /// Donor ALT status D_ALT Opt 3
        /// </summary>
        [MaxLength(3)]
        [JsonProperty("alt")]
        public string DonorAltStatus { get; set; }
    }


}
