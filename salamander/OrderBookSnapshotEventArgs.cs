using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.defrobo.salamander
{
    public class OrderBookSnapshotEventArgs : EventArgs
    {
        public string RawOrderBookSnapshotMessage { get; }
        public OrderBookUpdate OrderBookUpdate { get; }

        public OrderBookSnapshotEventArgs(string rawOrderBookSnapshotMessage)
        {
            this.RawOrderBookSnapshotMessage = rawOrderBookSnapshotMessage;
            this.OrderBookUpdate = JsonConvert.DeserializeObject<OrderBookUpdate>(rawOrderBookSnapshotMessage);
        }
    }
}
