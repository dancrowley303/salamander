using Newtonsoft.Json;
using System;

namespace com.defrobo.salamander
{
    public class OrderBookUpdateEventArgs : EventArgs
    {
        public string RawOrderBookSnapshotMessage { get; }
        public OrderBookUpdate OrderBookUpdate { get; }

        public OrderBookUpdateEventArgs(string rawOrderBookSnapshotMessage)
        {
            this.RawOrderBookSnapshotMessage = rawOrderBookSnapshotMessage;
            this.OrderBookUpdate = JsonConvert.DeserializeObject<OrderBookUpdate>(rawOrderBookSnapshotMessage);
        }
    }
}
