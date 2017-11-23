using Newtonsoft.Json;
using ProtoBuf;

namespace com.defrobo.salamander
{
    [ProtoContract]
    public struct Order
    {
        [ProtoMember(1)]
        public decimal Price;
        [ProtoMember(2)]
        public decimal Size;
    }

    [ProtoContract]
    public struct OrderBookUpdate
    {
        [ProtoMember(1)]
        [JsonProperty(PropertyName = "mid_price")]
        public decimal MidPrice;

        [ProtoMember(2)]
        public Order[] Bids;

        [ProtoMember(3)]
        public Order[] Asks;
    }
}
