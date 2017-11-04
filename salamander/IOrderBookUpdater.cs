using System;

namespace com.defrobo.salamander
{
    public interface IOrderBookUpdater
    {
        event EventHandler<OrderBookUpdateEventArgs> Snapshot;
        event EventHandler<OrderBookUpdateEventArgs> Updated;
    }
}
