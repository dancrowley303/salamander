using System;

namespace com.defrobo.salamander
{
    public interface ITicker
    {
        event EventHandler<MarketTickEventArgs> Updated;
    }
}
