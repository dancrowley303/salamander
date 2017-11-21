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
        public string ProductCode { get; set; }

        [ProtoMember(2)]
        [JsonProperty(PropertyName = "timestamp")]
        public DateTime TimeStamp { get; set; }

        [ProtoMember(3)]
        [JsonProperty(PropertyName = "best_bid")]
        public decimal BestBid { get; set; }

        [ProtoMember(4)]
        [JsonProperty(PropertyName = "best_ask")]
        public decimal BestAsk { get; set; }

        [ProtoMember(5)]
        [JsonProperty(PropertyName = "best_bid_size")]
        public decimal BestBidSize { get; set; }

        [ProtoMember(6)]
        [JsonProperty(PropertyName = "best_ask_size")]
        public decimal BestAskSize { get; set; }

        [ProtoMember(7)]
        [JsonProperty(PropertyName = "total_bid_depth")]
        public decimal TotalBidDepth { get; set; }

        [ProtoMember(8)]
        [JsonProperty(PropertyName = "total_ask_depth")]
        public decimal TotalAskDepth { get; set; }

        [ProtoMember(9)]
        [JsonProperty(PropertyName = "ltp")]
        public decimal LastTradedPrice { get; set; }

        [ProtoMember(10)]
        [JsonProperty(PropertyName = "volume")]
        public decimal Volume { get; set; }

        [ProtoMember(11)]
        [JsonProperty(PropertyName = "volume_by_product")]
        public decimal VolumeByProduct { get; set; }
    }
}
