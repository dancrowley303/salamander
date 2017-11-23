using Newtonsoft.Json;
using ProtoBuf;
using System;

namespace com.defrobo.salamander
{
    [ProtoContract]
    public class OrderBookUpdateEventArgs : EventArgs
    {
        [ProtoMember(1)]
        public OrderBookUpdate OrderBookUpdate { get; set; }

        public OrderBookUpdateEventArgs(string rawOrderBookUpdateMessage)
        {
            this.OrderBookUpdate = JsonConvert.DeserializeObject<OrderBookUpdate>(rawOrderBookUpdateMessage);
        }

        public OrderBookUpdateEventArgs(OrderBookUpdate orderBookUpdate)
        {
            this.OrderBookUpdate = orderBookUpdate;
        }

        public OrderBookUpdateEventArgs()
        {

        }
    }
}
