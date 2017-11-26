using ProtoBuf;
using System.Collections.Generic;

namespace com.defrobo.salamander
{
    [ProtoContract]
    public class BackTestData
    {
        [ProtoMember(1)]
        public Dictionary<long, List<ExecutionEventArgs>> ExecutionEvents { get; set; }

        [ProtoMember(2)]
        public Dictionary<long, List<OrderBookUpdateEventArgs>> OrderBookUpdateEvents { get; set; }

        [ProtoMember(3)]
        public Dictionary<long, List<OrderBookSnapshotEventArgs>> OrderBookSnapshotEvents { get; set; }

        [ProtoMember(4)]
        public Dictionary<long, List<MarketTickEventArgs>> MarketTickEvents { get; set; }
    }
}
