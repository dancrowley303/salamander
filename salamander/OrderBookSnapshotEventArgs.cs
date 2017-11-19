using Newtonsoft.Json;
using System;

namespace com.defrobo.salamander
{
    public class OrderBookSnapshotEventArgs : EventArgs
    {
        public string RawOrderBookSnapshotMessage { get; }
        public OrderBookUpdate OrderBookSnapshot { get; }

        public OrderBookSnapshotEventArgs(string rawOrderBookSnapshotMessage)
        {
            this.RawOrderBookSnapshotMessage = rawOrderBookSnapshotMessage;
            this.OrderBookSnapshot = JsonConvert.DeserializeObject<OrderBookUpdate>(rawOrderBookSnapshotMessage);
        }
    }
}
