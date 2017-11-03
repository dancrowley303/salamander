using Newtonsoft.Json;
using System;

namespace com.defrobo.salamander
{
    public class MarketTick
    {
        [JsonProperty(PropertyName = "product_code")]
        public string ProductCode { get; }

        [JsonProperty(PropertyName = "timestamp")]
        public DateTime TimeStamp { get; }

        [JsonProperty(PropertyName = "best_bid")]
        public decimal BestBid { get; }

        [JsonProperty(PropertyName = "best_ask")]
        public decimal BestAsk { get; }

        [JsonProperty(PropertyName = "best_bid_size")]
        public decimal BestBidSize { get; }

        [JsonProperty(PropertyName = "best_ask_size")]
        public decimal BestAskSize { get; }

        [JsonProperty(PropertyName = "total_bid_depth")]
        public decimal TotalBidDepth { get; }

        [JsonProperty(PropertyName = "total_ask_depth")]
        public decimal TotalAskDepth { get; }

        [JsonProperty(PropertyName = "ltp")]
        public decimal LastTradedPrice { get; }

        [JsonProperty(PropertyName = "volume")]
        public decimal Volume { get; }

        [JsonProperty(PropertyName = "volume_by_product")]
        public decimal VolumeByProduct { get; }
    
        public MarketTick(string productCode, DateTime timeStamp, int tickId, decimal bestBid, decimal bestAsk,
        decimal bestBidSize, decimal bestAskSize, decimal totalBidDepth, decimal totalAskDepth,
        decimal lastTradedPrice, decimal volume, decimal volumeByProduct)
        {
            this.ProductCode = productCode;
            this.TimeStamp = timeStamp;
            this.BestBid = bestBid;
            this.BestAsk = bestAsk;
            this.BestBidSize = bestBidSize;
            this.BestAskSize = bestAskSize;
            this.TotalBidDepth = totalBidDepth;
            this.TotalAskDepth = totalAskDepth;
            this.LastTradedPrice = lastTradedPrice;
            this.Volume = volume;
            this.VolumeByProduct = volumeByProduct;
        }
    }
}
