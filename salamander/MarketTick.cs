using Newtonsoft.Json;
using ProtoBuf;
using System;

namespace com.defrobo.salamander
{
    [ProtoContract]
    public struct MarketTick
    {
        [ProtoMember(1)]
        [JsonProperty(PropertyName = "product_code")]
        public string ProductCode;

        [ProtoMember(2)]
        [JsonProperty(PropertyName = "timestamp")]
        public DateTime TimeStamp;

        [ProtoMember(3)]
        [JsonProperty(PropertyName = "best_bid")]
        public decimal BestBid;

        [ProtoMember(4)]
        [JsonProperty(PropertyName = "best_ask")]
        public decimal BestAsk;

        [ProtoMember(5)]
        [JsonProperty(PropertyName = "best_bid_size")]
        public decimal BestBidSize;

        [ProtoMember(6)]
        [JsonProperty(PropertyName = "best_ask_size")]
        public decimal BestAskSize;

        [ProtoMember(7)]
        [JsonProperty(PropertyName = "total_bid_depth")]
        public decimal TotalBidDepth;

        [ProtoMember(8)]
        [JsonProperty(PropertyName = "total_ask_depth")]
        public decimal TotalAskDepth;

        [ProtoMember(9)]
        [JsonProperty(PropertyName = "ltp")]
        public decimal LastTradedPrice;

        [ProtoMember(10)]
        [JsonProperty(PropertyName = "volume")]
        public decimal Volume;

        [ProtoMember(11)]
        [JsonProperty(PropertyName = "volume_by_product")]
        public decimal VolumeByProduct;
    }
}
