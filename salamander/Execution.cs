using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ProtoBuf;
using System;

namespace com.defrobo.salamander
{
    [ProtoContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Side
    {
        Buy,
        Sell
    }

    [ProtoContract]
    public struct Execution
    {
        [ProtoMember(1)]
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        [ProtoMember(2)]
        [JsonProperty(PropertyName = "side")]
        public Side Side { get; set; }

        [ProtoMember(3)]
        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }

        [ProtoMember(4)]
        [JsonProperty(PropertyName = "size")]
        public decimal Size { get; set;}

        [ProtoMember(5)]
        [JsonProperty(PropertyName = "exec_date")]
        public DateTime ExecutionDate { get; set; }

        [ProtoMember(6)]
        [JsonProperty(PropertyName = "buy_child_order_acceptance_id")]
        public string BuyChildOrderAcceptanceId { get; set; }

        [ProtoMember(7)]
        [JsonProperty(PropertyName = "sell_child_order_acceptance_id")]
        public string SellChildOrderAcceptanceId { get; set; }
    }
}
