using Newtonsoft.Json;

namespace WmdaConnect.Models.OtherTypes
{
    /// <summary>
    /// Supply one node from the following
    /// </summary>
    public class Hla
    {

        [JsonProperty("a")]
        public HlaNodeSerDna HlaNodeSerDnaA { get; set; }

        [JsonProperty("b")]
        public HlaNodeSerDna HlaNodeSerDnaB { get; set; }

        [JsonProperty("c")]
        public HlaNodeSerDna HlaNodeSerDnaC { get; set; }

        [JsonProperty("drb1")]
        public HlaNodeSerDna HlaNodeSerDnaDrb1 { get; set; }

        [JsonProperty("dqb1")]
        public HlaNodeSerDna HlaNodeSerDnaDqb1 { get; set; }

        [JsonProperty("dpb1")]
        public HlaNodeDna HlaNodeDnaDpb1 { get; set; }

        [JsonProperty("drb3")]
        public HlaNodeDna HlaNodeDnaDrb3 { get; set; }

        [JsonProperty("drb4")]
        public HlaNodeDna HlaNodeDnaDrb4 { get; set; }

        [JsonProperty("drb5")]
        public HlaNodeDna HlaNodeDnaDrb5 { get; set; }

        [JsonProperty("dpa1")]
        public HlaNodeDna HlaNodeDnaDpa1 { get; set; }

        [JsonProperty("dqa1")]
        public HlaNodeDna HlaNodeDnaDqa1 { get; set; }

        [JsonProperty("mica")]
        public HlaNodeGls HlaNodeGlsMica { get; set; }

        [JsonProperty("micb")]
        public HlaNodeGls HlaNodeGlsMicb { get; set; }
    }
}
