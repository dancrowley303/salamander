using com.defrobo.salamander;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salamander.backtester
{
    [ProtoContract]
    public class BackTestData
    {
        [ProtoMember(1)]
        public Dictionary<long, List<Execution>> Executions { get; set; }

        [ProtoMember(2)]
        public Dictionary<long, List<OrderBookUpdate>> OrderBookUpdates { get; set; }

        [ProtoMember(3)]
        public Dictionary<long, List<OrderBookUpdate>> OrderBookSnapshots { get; set; }

        [ProtoMember(4)]
        public Dictionary<long, List<MarketTick>> MarketTicks { get; set; }
    }
}
