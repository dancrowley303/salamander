using Newtonsoft.Json;
using ProtoBuf;
using System.Collections.Generic;

namespace com.defrobo.salamander
{
    [ProtoContract]
    public struct Order
    {
        [ProtoMember(1)]
        public decimal Price { get; }
        [ProtoMember(2)]
        public decimal Size { get; }
    }

    [ProtoContract]
    public class OrderBookUpdate
    {
        [ProtoMember(1)]
        [JsonProperty(PropertyName = "mid_price")]
        public decimal MidPrice { get; }

        [ProtoMember(2)]
        public List<Order> Bids { get; }

        [ProtoMember(3)]
        public List<Order> Asks { get; }
    }
}
