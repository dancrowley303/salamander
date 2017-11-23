using Newtonsoft.Json;
using ProtoBuf;
using System;

namespace com.defrobo.salamander
{
    [ProtoContract]
    public class MarketTickEventArgs : EventArgs
    {
        [ProtoMember(1)]
        public MarketTick Tick { get; set; }

        public MarketTickEventArgs(string rawTickMessage)
        {
            this.Tick = JsonConvert.DeserializeObject<MarketTick>(rawTickMessage);
        }

        public MarketTickEventArgs(MarketTick tick)
        {
            this.Tick = tick;
        }

        public MarketTickEventArgs()
        {

        }
    }
}
