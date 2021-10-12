using Newtonsoft.Json;

namespace WmdaConnect.Models.IdTypes
{
    internal interface IHasCordBloodUnitId
    {
        [JsonProperty("cbu")]
        public CordBloodUnitId CordBloodUnit { get; set; }
    }
}
