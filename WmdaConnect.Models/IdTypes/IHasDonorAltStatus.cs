using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WmdaConnect.Models.IdTypes;

public interface IHasDonorAltStatus
{
    /// <summary>
    /// Donor ALT status D_ALT Opt 3
    /// </summary>
    [Range(0,999)]
    [JsonProperty("alt")]
    public int? DonorAltStatus { get; set; }
}