using Newtonsoft.Json;
using System;

namespace com.defrobo.salamander
{
    public class MarketTickEventArgs : EventArgs
    {
        public string RawTickMessage { get; }
        public MarketTick Tick { get;  }

        public MarketTickEventArgs(string rawTickMessage)
        {
            this.RawTickMessage = rawTickMessage;
            this.Tick = JsonConvert.DeserializeObject<MarketTick>(rawTickMessage);
        }
    }
}
