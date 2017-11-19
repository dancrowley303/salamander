using Newtonsoft.Json;
using System;

namespace com.defrobo.salamander
{
    public class OrderBookUpdateEventArgs : EventArgs
    {
        public string RawOrderBookUpdateMessage { get; }
        public OrderBookUpdate OrderBookUpdate { get; }

        public OrderBookUpdateEventArgs(string rawOrderBookUpdateMessage)
        {
            this.RawOrderBookUpdateMessage = rawOrderBookUpdateMessage;
            this.OrderBookUpdate = JsonConvert.DeserializeObject<OrderBookUpdate>(rawOrderBookUpdateMessage);
        }
    }
}
