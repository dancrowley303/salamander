using Newtonsoft.Json;
using System.Collections.Generic;

namespace com.defrobo.salamander
{
    public class Order
    {
        public decimal Price { get; }
        public decimal Size { get; }

        public Order(decimal price, decimal size)
        {
            this.Price = price;
            this.Size = size;
        }
    }

    public class OrderBookUpdate
    {
        public OrderBookUpdate(decimal midPrice, List<Order> bids, List<Order> asks)
        {
            this.MidPrice = midPrice;
            this.Bids = bids;
            this.Asks = asks;
        }

        [JsonProperty(PropertyName = "mid_price")]
        public decimal MidPrice { get; }

        public List<Order> Bids { get; }

        public List<Order> Asks { get; }
    }
}
