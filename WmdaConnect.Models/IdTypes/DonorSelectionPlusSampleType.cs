using Newtonsoft.Json;

namespace WmdaConnect.Models.IdTypes
{
    /// <summary>
    /// Supply one node from the following
    /// </summary>
    public class DonorSelectionPlusSampleType
    {
        [JsonProperty("scd")]
        public DonorGrid StemCellDonor { get; set; }

        [JsonProperty("adcu")]
        public AdultDonorCryopreservedUnitId Adcu { get; set; }

        [JsonProperty("cbu")]
        public CordBloodUnitIdPlusSampleType CordBloodUnit { get; set; }
    }
}