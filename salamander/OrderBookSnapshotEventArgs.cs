using Newtonsoft.Json;
using ProtoBuf;
using System;

namespace com.defrobo.salamander
{
    [ProtoContract]
    public class OrderBookSnapshotEventArgs : EventArgs
    {
        [ProtoMember(1)]
        public OrderBookUpdate OrderBookSnapshot { get; set; }

        public OrderBookSnapshotEventArgs(string rawOrderBookSnapshotMessage)
        {
            this.OrderBookSnapshot = JsonConvert.DeserializeObject<OrderBookUpdate>(rawOrderBookSnapshotMessage);
        }

        public OrderBookSnapshotEventArgs(OrderBookUpdate orderBookUpdate)
        {
            this.OrderBookSnapshot = orderBookUpdate;
        }

        public OrderBookSnapshotEventArgs()
        {

        }
    }
}
